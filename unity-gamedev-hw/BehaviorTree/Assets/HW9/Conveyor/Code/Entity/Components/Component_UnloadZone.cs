using Elementary;
using Game.GameEngine.GameResources;
using System;
using UnityEngine;

namespace Conveyors.Entity.Components {
    public sealed class Component_UnloadZone : IComponent_UnloadZone {
        public event Action<int> OnAmountChanged {
            add { _storage.OnValueChanged += value; }
            remove { _storage.OnValueChanged -= value; }
        }

        #region PublicVariables

        public int MaxAmount {
            get { return _storage.MaxValue; }
        }

        public int CurrentAmount {
            get { return _storage.Current; }
        }

        public bool IsFull {
            get { return _storage.IsLimit; }
        }

        public bool IsEmpty {
            get { return _storage.Current <= 0; }
        }

        public ResourceType ResourceType {
            get { return _resourceType; }
        }

        public Vector3 ParticlePosition {
            get { return _particlePoint.position; }
        }

        #endregion


        private readonly IVariableLimited<int> _storage;
        private readonly ResourceType _resourceType;
        private readonly Transform _particlePoint;

        public Component_UnloadZone(IVariableLimited<int> storage, ResourceType resourceType, Transform particlePoint) {
            _storage = storage;
            _resourceType = resourceType;
            _particlePoint = particlePoint;
        }

        public void SetupAmount(int currentAmount) {
            _storage.Current = currentAmount;
        }

        public int ExtractAll() {
            var resources = _storage.Current;
            _storage.Current = 0;
            return resources;
        }
    }
}