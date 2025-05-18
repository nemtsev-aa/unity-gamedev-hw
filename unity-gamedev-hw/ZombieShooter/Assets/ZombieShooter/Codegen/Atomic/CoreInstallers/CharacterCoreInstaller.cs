using System;
using UnityEngine;
using Atomic.Elements;
using Atomic.Entities;
using AtomicFramework.Effects;
using AtomicFramework.LifeCompanent;
using AtomicFramework.RotationCompanent;
using AtomicFramework.ZombieShooter;
using ZombieShooter.SceneObjects;

namespace AtomicFramework.CoreInstallers {

    public sealed class CharacterCoreInstaller : SceneEntityInstallerBase {
        [SerializeField] private Character _root;
        [SerializeField] private bool _showLogAfterExecution;
        
        private CharacterConfig _config;

        [field: SerializeField] public LifeInstaller LifeInstaller { get; private set; }
        [field: SerializeField] public MoveInstaller MoveInstaller { get; private set; }
        [field: SerializeField] public MouseLookRotationInstaller RotationInstaller { get; private set; }
        [field: SerializeField] public EffectsInstaller EffectsInstaller { get; private set; }

        public override void Install(IEntity entity) {

            entity.AddRoot(_root.transform);
            entity.AddAttackRequest(new BaseEvent());
            entity.AddAttackAction(new BaseEvent());
            entity.AddPointerPosition(new ReactiveVariable<Vector3>());

            if (_config == null)
                throw new ArgumentException($"CharacterCoreInstaller: CharacterConfig is empty!");

            SetConfigValuesFromInstallers();

            LifeInstaller.Install(entity);
            MoveInstaller.Install(entity);
            RotationInstaller.Install(entity);
            EffectsInstaller.Install(entity);

            if (_showLogAfterExecution == true)
                Debug.Log($"{nameof(CharacterCoreInstaller)} installed");
        }

        public void SetConfig(CharacterConfig config) {
            _config = config;
        }

        private void SetConfigValuesFromInstallers() {
            LifeInstaller.SetConfig(_config.Life);
            MoveInstaller.SetConfig(_config.Move);
            RotationInstaller.SetConfig(_config.Rotate);
        }
    }
}


