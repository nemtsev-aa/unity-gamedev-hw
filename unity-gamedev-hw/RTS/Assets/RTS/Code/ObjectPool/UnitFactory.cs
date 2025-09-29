using Client;
using Client.Components;
using Client.Components.Common;
using Leopotam.EcsLite.Entities;
using System;
using UnityEngine;
using Zenject;

namespace UnitPoolSystem {

    public sealed class UnitFactory {
        private readonly DiContainer _container;
        private readonly UnitPrefabPresenter _presenter;

        public UnitFactory(DiContainer container, UnitPrefabPresenter presenter) {
            _container = container;
            _presenter = presenter;
        }

        public UnitEntity Get(Transform parent, UnitTypes type) {
 
            if (_presenter.TryGetUnitEntityByType(type, out UnitEntity prefab) == true) {
                UnitEntity newUnit = _container.InstantiatePrefabForComponent<UnitEntity>(prefab);
                //var newUnit = _entityManager.Create(prefab, Vector3.zero, Quaternion.identity);
                newUnit.transform.SetParent(parent);

                return newUnit;
            }

            throw new ArgumentNullException($"UnitType {type} not found!");
        }
    }
}