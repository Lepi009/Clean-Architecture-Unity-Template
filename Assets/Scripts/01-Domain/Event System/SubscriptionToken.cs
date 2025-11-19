using System;

namespace Domain.EventBus {
    internal readonly struct SubscriptionToken {
        internal readonly Type EventType;
        internal readonly Guid Id;

        internal SubscriptionToken(Type eventType) {
            EventType = eventType;
            Id = Guid.NewGuid();
        }
    }
}