using System;
using System.Linq;
using System.Collections.Generic;

namespace EventBusService {

    public sealed class EventBus : IEventBus {
        private readonly Dictionary<Type, List<Action<IEvent>>> _subscribers = new();

        public void Subscribe<T>(Action<T> handler) where T : IEvent {
            Type eventType = typeof(T);

            if (_subscribers.ContainsKey(eventType) == false)
                _subscribers[eventType] = new List<Action<IEvent>>();

            _subscribers[eventType].Add(@event => handler((T)@event));
        }

        public void Unsubscribe<T>(Action<T> handler) where T : IEvent {
            Type eventType = typeof(T);

            if (_subscribers.ContainsKey(eventType) == true)
                _subscribers[eventType].RemoveAll(h => h.Target == handler.Target && h.Method == handler.Method);
        }

        public void Publish<T>(T @event) where T : IEvent {
            Type eventType = typeof(T);

            if (_subscribers.ContainsKey(eventType) == true) {

                foreach (var handler in _subscribers[eventType].ToList()) {
                    handler(@event);
                }
            }
        }

        public void Dispose() {
            _subscribers.Clear();
        }
    }
}