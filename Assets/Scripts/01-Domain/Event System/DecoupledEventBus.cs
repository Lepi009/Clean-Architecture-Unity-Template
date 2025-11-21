using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Domain.EventBus {

    /// <summary> an event bus that decouples in time and space </summary>
    public class DecoupledEventBus : IEventBus, IUpdatable {
        private const double MAX_UPDATE_PROCESSING_TIME = 0.005;
        private readonly ConcurrentDictionary<Type, List<EventSubscription>> _subscribers = new();
        private readonly ConcurrentQueue<EventHandlingTask> _eventDispatchQueue = new();
        private readonly ConcurrentDictionary<Type, object> _eventTypeLocks = new();
        private readonly ITimeProvider _timeProvider;

        public DecoupledEventBus(ITimeProvider timeProvider) {
            _timeProvider = timeProvider;
        }

        /// <summary> Subscribe to an event type with optional priority and filter.
        /// Returns a SubscriptionToken you can use to unsubscribe. </summary>
        SubscriptionToken IEventBus.Subscribe<T>(Action<T> handler, int priority, Predicate<T> filter) {
            var type = typeof(T);

            var subscription = new EventSubscription(
                new SubscriptionToken(type),
                e => handler((T)e),
                priority,
                filter != null ? (e => filter((T)e)) : null
            );
            var token = subscription.Token;

            var list = _subscribers.GetOrAdd(type, _ => new List<EventSubscription>());

            lock(_eventTypeLocks.GetOrAdd(type, _ => new object())) {
                // Binary insert by priority (descending)
                int index = list.BinarySearch(subscription);
                if(index < 0) index = ~index;
                list.Insert(index, subscription);
            }

            return token;
        }

        /// <summary>Unsubscribe using the token returned by Subscribe.</summary>
        void IEventBus.Unsubscribe(SubscriptionToken token) {
            if(!_subscribers.TryGetValue(token.EventType, out var subscriptions)) return;

            lock(_eventTypeLocks.GetOrAdd(token.EventType, _ => new object())) {
                for(int i = subscriptions.Count - 1; i >= 0; i--) {
                    if(subscriptions[i].Token.Id == token.Id) {
                        subscriptions.RemoveAt(i);
                    }
                }
            }
        }


        /// <summary> Publish an event to all subscribers.</summary>
        public void Publish<T>(T gameEvent) where T : IGameEvent {
            var type = gameEvent.GetType();
            if(!_subscribers.TryGetValue(type, out var list)) return;

            lock(_eventTypeLocks.GetOrAdd(type, _ => new object())) {
                foreach(var sub in list) {
                    if(sub.Filter == null || sub.Filter(gameEvent)) {
                        _eventDispatchQueue.Enqueue(new EventHandlingTask(gameEvent, sub.Handler));
                    }
                }
            }
        }

        /// <summary>Called every frame to check for new handlers to resolve</summary>
        public void Update(float deltaTime) {
            if(_eventDispatchQueue.IsEmpty) return;

            double dispatchTime = _timeProvider.Now;
            while(_timeProvider.Now - dispatchTime < MAX_UPDATE_PROCESSING_TIME && !_eventDispatchQueue.IsEmpty) {
                if(!_eventDispatchQueue.TryDequeue(out var dispatch))
                    break;

                try { dispatch.Callback(dispatch.GameEvent); }
                catch(Exception e) { ServiceLocator.Logger.LogException(e); }
            }
        }

        private sealed record EventSubscription(SubscriptionToken Token, Action<IGameEvent> Handler, int Priority, Predicate<IGameEvent> Filter = null) : IComparable<EventSubscription> {
            public int CompareTo(EventSubscription other)
                => other is null ? -1 : other.Priority.CompareTo(Priority);
        }

        private readonly record struct EventHandlingTask(IGameEvent GameEvent, Action<IGameEvent> Callback);
    }
}