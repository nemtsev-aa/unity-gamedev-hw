using System;
using Zenject;
using UnityEngine;
using InventorySystem.UI;

namespace InventorySystem.Installers {

    [Serializable]
    public sealed class InventoryUIInstaller {
        [SerializeField] private InventoryItemInfoPopup _itemInfoPopup;
        [SerializeField] private InventoryPopup _inventoryPopup;

        public void Bind(DiContainer container) {

            container.BindInstance(_itemInfoPopup)
                     .AsSingle();

            container.BindInstance(_inventoryPopup)
                     .AsSingle();
        }
    }
}
