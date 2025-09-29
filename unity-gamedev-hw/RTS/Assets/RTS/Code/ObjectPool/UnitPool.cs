using System;
using UnityEngine;
using UnitPoolSystem;
using Client.Components;
using GameplayCompanents;
using System.Collections.Generic;
using Client.Components.Common;

namespace GameCycleSystem {

    public sealed class UnitPool {
        private readonly UnitFactory _factory;
        private readonly ContainersPresenter _presenter;

        private readonly Queue<UnitEntity> _unitPool = new();

        private Transform _worldContainer;
        private Transform _unitContainer;

        public UnitPool(UnitFactory factory,
                        ContainersPresenter presenter) {

            _factory = factory;
            _presenter = presenter;
            _worldContainer = _presenter.WorldContainer;
        }

        public void Create(UnitTypes unitType, int size) {

            if (size <= 0)
                throw new ArgumentNullException($"UnitPool size less than zero!");

            _unitContainer = _presenter.GetContainerByUnitType(unitType);
            _unitContainer.gameObject.SetActive(false);

            for (var i = 0; i < size; i++) {
                var unit = _factory.Get(_unitContainer, unitType);
                unit.gameObject.name = $"{unitType} [{i}]";

                _unitPool.Enqueue(unit);
            }
        }

        public void Clear() {
            if (_unitPool.Count <= 0)
                return;

            foreach (var iUnit in _unitPool) {
                UnityEngine.Object.Destroy(iUnit.gameObject);
            }

            _unitPool.Clear();
        }

        public bool TryGetUnitFromPool(UnitTypes unitType, out UnitEntity unit) {
            if (_unitPool.TryDequeue(out unit) == false)
                return false;

            unit.transform.SetParent(_worldContainer);
            unit.transform.position = _worldContainer.position;

            return true;
        }

        public void ReturnUnitToPool(UnitEntity unit) {
            unit.transform.SetParent(_unitContainer);
            _unitPool.Enqueue(unit);
        }
    }
}