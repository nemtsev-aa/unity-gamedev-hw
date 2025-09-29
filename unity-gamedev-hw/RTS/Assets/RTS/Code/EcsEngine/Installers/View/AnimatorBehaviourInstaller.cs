using UnityEngine;
using Client.Components.Visual;
using Leopotam.EcsLite.Entities;

namespace Code.Visual {

    public sealed class AnimatorBehaviourInstaller : EntityInstaller {

        [SerializeField] private AnimatorBehaviour _animatorBehaviour;

        private Entity _entity;

        protected override void Install(Entity entity) {
            _entity = entity;
            _animatorBehaviour.Init(entity);

            _entity.AddData(new AnimatorBehaviourView { Value = _animatorBehaviour });
        }

        protected override void Dispose(Entity entity) {

        }
    }
}


