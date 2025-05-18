using UnityEngine;
using Atomic.Entities;

namespace AtomicFramework.View.Visual {

    public sealed class VisualInstaller : SceneEntityInstallerBase {
        [SerializeField] private AnimatorInstaller _animatorInstaller;

        public override void Install(IEntity entity) {
            _animatorInstaller.Init(entity);
        }
    }
}
