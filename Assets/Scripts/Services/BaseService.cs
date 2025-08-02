using UnityEngine;

namespace Services
{
    /// <summary>
    /// Base class for services in framework
    /// </summary>  
    public abstract class BaseService : MonoBehaviour
    {
        private bool IsInitialized { get; set; }
        private bool IsSubscribed { get; set; }

        public void PreInitialize() => PreInitializeInternal();

        public void Initialize()
        {
            if (IsInitialized) return;
            InitializeInternal();
            IsInitialized = true;
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            if (IsSubscribed) return;
            SubscribeToEventsInternal();
            IsSubscribed = true;
        }

        private void UnsubscribeToEvents()
        {
            if (!IsSubscribed) return;
            UnsubscribeToEventsInternal();
            IsSubscribed = false;
        }

        public void Tick(float deltaTime)
        {
            if (!IsInitialized) return;
            TickInternal(deltaTime);
        }
        
        public void Dispose()
        {
            DisposeInternal();
            IsInitialized = false;
            UnsubscribeToEvents();
        }
        
        protected abstract void PreInitializeInternal();
        protected abstract void InitializeInternal();
        protected abstract void SubscribeToEventsInternal();
        protected abstract void UnsubscribeToEventsInternal();
        protected abstract void TickInternal(float deltaTime);
        protected abstract void DisposeInternal();
    }
}
