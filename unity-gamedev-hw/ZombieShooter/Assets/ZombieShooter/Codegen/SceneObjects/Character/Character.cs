using Atomic.Entities;
using AtomicFramework.BulletSystem;
using AtomicFramework.CoreInstallers;
using AtomicFramework.View.SFX;
using AtomicFramework.View.VFX;
using AtomicFramework.View.Visual;
using UnityEngine;

namespace ZombieShooter.SceneObjects {

    public sealed class Character : Unit {
        [field: SerializeField] public FirePoint FirePoint { get; private set; }

        private CharacterConfig _config;

        private CharacterCoreInstaller _core;
        private VisualInstaller _visual;
        private VFXSystemInstaller _vfx;
        private SFXSystemInstaller _audio;

        public void Init(CharacterConfig config) {
            _config = config;

            _core = Entity.GetComponent<CharacterCoreInstaller>();
            _core.SetConfig(_config);

            _visual = Entity.GetComponent<VisualInstaller>();
            _vfx = Entity.GetComponent<VFXSystemInstaller>();
            _audio = Entity.GetComponent<SFXSystemInstaller>();

            _core.Install(Entity);
            _visual.Install(Entity);
            _vfx.Install(Entity);
            _audio.Install(Entity);

            //Debug.Log($"Character Init: {_config}");
        }

        public void Reset() {
            Entity.GetHitPoints().Value = _config.Life.HitPointCount;
            Entity.GetIsDeath().Value = false;
        }
    }
}
