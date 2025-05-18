using Atomic.Entities;
using AtomicFramework.AttackCompanent;
using AtomicFramework.LifeCompanent;
using AtomicFramework.MoveCompanent;
using AtomicFramework.RotationCompanent;
using UnityEngine;

namespace AtomicFramework.CoreInstallers {

    public sealed class EnemyCoreInsteller : SceneEntityInstallerBase {
        [SerializeField] private Transform _root;

        private Transform _target;

        [field: SerializeField] public LifeInstaller LifeInstaller { get; private set; }
        [field: SerializeField] public MoveToTargetInstaller MoveToTargetInstaller { get; private set; }
        [field: SerializeField] public RotateToTargetInstaller RotateToTargetInstaller { get; private set; }
        [field: SerializeField] public MeleeAttackInstaller MeleeAttackInstaller { get; private set; }

        public override void Install(IEntity entity) {
            entity.AddRoot(_root);

            entity.Name = _root.gameObject.name;

            LifeInstaller.Install(entity);
            MoveToTargetInstaller.Install(entity);
            RotateToTargetInstaller.Install(entity);
            MeleeAttackInstaller.Install(entity);

            entity.SetTargetTransform(_target);
            entity.GetTargetChanged().Invoke(_target);

            //Debug.Log($"EnemyCoreInsteller Installed");
        }

        public void SetTarget(Transform target) {
            _target = target;
        }
    }
}


