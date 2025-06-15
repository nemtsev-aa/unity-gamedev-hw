using System;
using Elementary;
using Game.GameEngine.GameResources;

namespace Conveyors.Entity.Components {

    public sealed class Component_LoadZone : IComponent_LoadZone {

        #region PubicVariables

        public event Action<int> OnAmountChanged {
            add { _storage.OnValueChanged += value; }
            remove { _storage.OnValueChanged -= value; }
        }

        public int MaxAmount => _storage.MaxValue; 
        public int CurrentAmount => _storage.Current; 
        public int AvailableAmount => _storage.MaxValue - _storage.Current; 
        public bool IsFull => _storage.IsLimit; 
        public bool IsEmpty => _storage.Current <= 0;
        public ResourceType ResourceType => _resourceType; 
        
        #endregion

        private readonly IVariableLimited<int> _storage;
        private readonly ResourceType _resourceType;

        public Component_LoadZone(IVariableLimited<int> storage, ResourceType resourceType) {
           _storage = storage;
           _resourceType = resourceType;
        }

        public void SetupAmount(int currentAmount) {
            _storage.Current = currentAmount;
        }

        public void PutAmount(int range) {
            _storage.Current += range;
        }
    }
}