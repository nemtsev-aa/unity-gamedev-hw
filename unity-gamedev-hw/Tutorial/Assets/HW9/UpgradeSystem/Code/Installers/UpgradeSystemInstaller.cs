using Zenject;
using UnityEngine;

namespace UpgradesSystem.Installers {

    public sealed class UpgradeSystemInstaller : MonoInstaller {
        [SerializeField] private CoreInstaller _core;
        [SerializeField] private UIInstaller _ui;

        public override void InstallBindings() {
            _core.Install(Container);
            _ui.Install(Container);
        }
    }
}
