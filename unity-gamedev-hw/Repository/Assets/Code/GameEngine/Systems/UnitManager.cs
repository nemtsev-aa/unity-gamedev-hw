using System;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameEngine {
    //Нельзя менять!
    [Serializable]
    public sealed class UnitManager {
        [SerializeField]
        private Transform _container;

        [ShowInInspector, ReadOnly]
        private HashSet<Unit> _sceneUnits = new();

        public UnitManager() {
        }

        public UnitManager(Transform container) {
            _container = container;
        }

        public Transform Container => _container;

        public void SetupUnits(IEnumerable<Unit> units) {
            _sceneUnits = new HashSet<Unit>(units);
        }

        public void SetContainer(Transform container) {
            _container = container;
        }

        [Button]
        public Unit SpawnUnit(Unit prefab, Vector3 position, Quaternion rotation) {
            var unit = Object.Instantiate(prefab, position, rotation, _container);
            _sceneUnits.Add(unit);

            return unit;
        }

        [Button]
        public void DestroyUnit(Unit unit) {
            
            if (_sceneUnits.Remove(unit)) 
                Object.Destroy(unit.gameObject);
            
        }

        public IEnumerable<Unit> GetAllUnits() {
            return _sceneUnits;
        }
    }
}