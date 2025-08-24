using System;

namespace EventBusService {

    public interface IEventBus : IDisposable {
        void Subscribe<T>(Action<T> handler) where T : IEvent;
        void Unsubscribe<T>(Action<T> handler) where T : IEvent;
        void Publish<T>(T @event) where T : IEvent;
    }
}