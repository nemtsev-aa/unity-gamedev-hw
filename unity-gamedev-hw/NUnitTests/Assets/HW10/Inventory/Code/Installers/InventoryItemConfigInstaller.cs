using Zenject;
using System;
using UnityEngine;
using InventorySystem.Core;

namespace InventorySystem.Installers {

    [Serializable]
    public sealed class InventoryItemConfigInstaller {
        [SerializeField] private InventoryItemConfigProvider _itemConfigProvider;

        public void Bind(DiContainer container) {

            _itemConfigProvider.Init();

            container.BindInstance(_itemConfigProvider)
                     .AsSingle()
                     .NonLazy();
        }
    }
}
