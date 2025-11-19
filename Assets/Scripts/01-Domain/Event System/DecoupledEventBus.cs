using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Domain.EventBus {

    /// <summary> an event bus that decouples in time and space </summary>
    public class DecoupledEventBus : IEventBus, IUpdatable {
        private const double MaxLoopTimeSec = 0.005;
        private readonly ConcurrentDictionary<Type, List<Subscription>> _subscribers = new();
        private readonly ConcurrentQueue<HandleAction> _handlers = new();
        private readonly ConcurrentDictionary<Type, object> _locks = new();
        private readonly ITimeProvider _timeProvider;

        public DecoupledEventBus(ITimeProvider timeProvider) {
            _timeProvider = timeProvider;
        }

        /// <summary> Subscribe to an event type with optional priority and filter.
        /// Returns a SubscriptionToken you can use to unsubscribe. </summary>
        SubscriptionToken IEventBus.Subscribe<T>(Action<T> handler, int priority, Predicate<T> filter) {
            var type = typeof(T);

            var sub = new Subscription(
                new SubscriptionToken(type),
                e => handler((T)e),
                priority,
                filter != null ? (e => filter((T)e)) : null
            );
            var token = sub.Token;

            var list = _subscribers.GetOrAdd(type, _ => new List<Subscription>());

            lock(_locks.GetOrAdd(type, _ => new object())) {
                // Binary insert by priority (descending)
                int index = list.BinarySearch(sub);
                if(index < 0) index = ~index;
                list.Insert(index, sub);
            }

            return token;
        }

        /// <summary>Unsubscribe using the token returned by Subscribe.</summary>
        void IEventBus.Unsubscribe(SubscriptionToken token) {
            if(!_subscribers.TryGetValue(token.EventType, out var list)) return;

            lock(_locks.GetOrAdd(token.EventType, _ => new object())) {
                for(int i = list.Count - 1; i >= 0; i--) {
                    if(list[i].Token.Id == token.Id) {
                        list.RemoveAt(i);
                    }
                }
            }
        }


        /// <summary> Publish an event to all subscribers.</summary>
        public void Publish<T>(T gameEvent) where T : IGameEvent {
            var type = gameEvent.GetType();
            if(!_subscribers.TryGetValue(type, out var list)) return;

            lock(_locks.GetOrAdd(type, _ => new object())) {
                foreach(var sub in list) {
                    if(sub.Filter == null || sub.Filter(gameEvent)) {
                        _handlers.Enqueue(new HandleAction(gameEvent, sub.Handler));
                    }
                }
            }
        }

        /// <summary>Called every frame to check for new handlers to resolve</summary>
        public void Update(float deltaTime) {
            if(_handlers.IsEmpty) return;

            double startTime = _timeProvider.Now;
            while(_timeProvider.Now - startTime < MaxLoopTimeSec && !_handlers.IsEmpty) {
                if(!_handlers.TryDequeue(out var handler))
                    break;

                try {
                    handler.Handler(handler.Event);
                }
                catch(Exception e) {
                    ServiceLocator.Logger.LogException(e);
                }
            }
        }

        private sealed record Subscription(SubscriptionToken Token, Action<IGameEvent> Handler, int Priority, Predicate<IGameEvent> Filter = null) : IComparable<Subscription> {
            public int CompareTo(Subscription other)
                => other is null ? -1 : other.Priority.CompareTo(Priority);
        }

        private readonly struct HandleAction {
            public readonly IGameEvent Event;
            public readonly Action<IGameEvent> Handler;
            public HandleAction(IGameEvent gameEvent, Action<IGameEvent> handler) {
                Event = gameEvent;
                Handler = handler;
            }
        }
    }
}