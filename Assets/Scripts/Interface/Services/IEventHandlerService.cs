using System;
using EventProcessing;

namespace Interface.Services
{
    public interface IEventHandlerService
    {
        public void RaiseEvent<T>(T payload) where T : IEvent;

        public void AddEventListener<T>(Action<T> action) where T : IEvent;

        public void RemoveEventListener<T>(Action<T> action) where T : IEvent;

        public void RemoveAllEventListeners<T>() where T : IEvent;

        public void RemoveAll();
    }
}