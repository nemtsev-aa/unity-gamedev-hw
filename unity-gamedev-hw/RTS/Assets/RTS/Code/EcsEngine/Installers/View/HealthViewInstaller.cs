using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Installer {

    internal sealed class HealthViewInstaller : EntityInstaller {
        [SerializeField] private HealthBar _healthBar;

        protected override void Install(Entity entity) {
            _healthBar.Init(entity);
        }

        protected override void Dispose(Entity entity) {

        }
    }
}

