using System;
using UnityEngine;
using Atomic.Entities;

namespace AtomicFramework.View.Visual {

    [Serializable]
    public sealed class AnimatorInstaller : IEntityInit {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationDispatcher _animationDispatcher;

        public void Init(IEntity entity) {
            entity.SetAnimator(_animator);
            entity.SetAnimationDispatcher(_animationDispatcher);

            entity.AddBehaviour(new AnimatorBehaviour());
        }
    }
}


