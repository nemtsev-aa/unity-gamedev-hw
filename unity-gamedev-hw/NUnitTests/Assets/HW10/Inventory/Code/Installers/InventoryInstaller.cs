using UnityEngine;
using Zenject;


namespace InventorySystem.Installers {

    public sealed class InventoryInstaller : MonoInstaller {
        [SerializeField, HideInInspector] private InventoryCoreInstaller _core;
        [SerializeField] private InventoryItemConfigInstaller _configs;
        [SerializeField] private InventoryUIInstaller _ui;

        public override void InstallBindings() {
            _core.Bind(Container);
            _configs.Bind(Container);
            _ui.Bind(Container);
        }
    }
}
