using UnityEngine;
using Atomic.Entities;
using AtomicFramework.CoreInstallers;
using AtomicFramework.EnemyPointerSystem;
using AtomicFramework.View.SFX;
using AtomicFramework.View.VFX;
using AtomicFramework.View.Visual;

namespace ZombieShooter.SceneObjects {

    public sealed class Enemy : Unit {
        private EnemyConfig _config;
        private EnemyCoreInsteller _core;
        private VisualInstaller _visual;
        private VFXSystemInstaller _vfx;
        private SFXSystemInstaller _audio;

        [field: SerializeField] public EnemyPointer EnemyPointer { get; private set; }

        public void Init(EnemyConfig config, Transform target) {
            _config = config;

            _core = Entity.GetComponent<EnemyCoreInsteller>();
            _visual = Entity.GetComponent<VisualInstaller>();
            _vfx = Entity.GetComponent<VFXSystemInstaller>();
            _audio = Entity.GetComponent<SFXSystemInstaller>();

            SetParametersToCoreInstaller(target);

            _core.Install(Entity);
            _visual.Install(Entity);
            _vfx.Install(Entity);
            _audio.Install(Entity);

            if (EnemyPointer != null)
                EnemyPointer.Init(this);
        }

        private void SetParametersToCoreInstaller(Transform target) {
            _core.LifeInstaller.SetConfig(_config.Life);
            _core.MoveToTargetInstaller.SetConfig(_config.Move);
            _core.RotateToTargetInstaller.SetConfig(_config.Rotate);
            _core.MeleeAttackInstaller.SetConfig(_config.Attack);

            _core.SetTarget(target);
        }

        public void Reset() {
            Entity.GetHitPoints().Value = _config.Life.HitPointCount;
            Entity.GetIsDeath().Value = false;
        }
    }
}
