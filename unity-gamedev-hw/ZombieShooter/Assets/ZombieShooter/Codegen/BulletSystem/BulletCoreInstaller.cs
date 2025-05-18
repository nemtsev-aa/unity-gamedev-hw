using System;
using UnityEngine;
using Atomic.Elements;
using Atomic.Entities;
using AtomicFramework.CollisionMechanics;
using AtomicFramework.MoveCompanent;

namespace AtomicFramework.BulletSystem {

    public sealed class BulletCoreInstaller : SceneEntityInstallerBase {
        [SerializeField] private Transform _root;

        private BulletConfig _config;

        [field: SerializeField] public OneDirectionMoveInstaller OneDirectionMoveInstaller { get; private set; }
        [field: SerializeField] public CollisionInstaller CollisionInstaller { get; private set; }

        public override void Install(IEntity entity) {

            entity.Name = _root.gameObject.name;
            entity.AddRoot(_root.transform);
            entity.AddTargetChanged(new BaseEvent<Transform>());
            entity.AddIsDestroy(new BaseEvent<IEntity>());
            entity.AddIsDeath(new ReactiveVariable<bool>(false));

            if (_config == null)
                throw new ArgumentException($"BulletCoreInstaller: BulletConfig is empty!");

            entity.AddDamage(_config.Attack.Damage);
            entity.AddRange(_config.Attack.Range);

            SetConfigValuesFromInstallers();

            OneDirectionMoveInstaller.Install(entity);
            CollisionInstaller.Install(entity);

            entity.AddBehaviour<BulletLifeCycle>();
        }

        public void SetConfig(BulletConfig config) {
            _config = config;

            SetConfigValuesFromInstallers();
        }

        private void SetConfigValuesFromInstallers() {
            OneDirectionMoveInstaller.SetConfig(_config.Move);
            CollisionInstaller.SetConfig(_config.Collision);
        }
    }
}
