using System;

namespace Domain.EventBus {
    public interface IEventBus {
        public void Publish<T>(T gameEvent) where T : IGameEvent;
        internal SubscriptionToken Subscribe<T>(Action<T> handler, int priority = 0, Predicate<T> filter = null) where T : IGameEvent;
        internal void Unsubscribe(SubscriptionToken token);
    }
}