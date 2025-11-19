using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.EventBus {
    public sealed class SubscriptionGroup : IDisposable {
        private readonly List<SubscriptionToken> _tokens = new();
        private IEventBus _bus;

        public SubscriptionGroup() {
            _bus = ServiceLocator.EventBus;
        }

        public void Add<T>(Action<T> handler, int priority = 0, Predicate<T> filter = null)
            where T : IGameEvent {
            var token = _bus.Subscribe(handler, priority, filter);
            _tokens.Add(token);
        }

        // Remove a single subscription by event type
        public void Remove<T>() where T : IGameEvent {
            var type = typeof(T);

            // Remove only tokens matching this event
            var tokensToRemove = _tokens.Where(t => t.EventType == type).ToArray();
            for(int i = 0; i < tokensToRemove.Length; i++) {
                Remove(tokensToRemove[i]);
            }
        }

        // Remove a single subscription by token
        private void Remove(SubscriptionToken token) {
            if(_tokens.Remove(token))
                _bus.Unsubscribe(token);
        }

        // Remove all
        public void Dispose() {
            foreach(var t in _tokens)
                _bus.Unsubscribe(t);

            _tokens.Clear();
        }
    }

}