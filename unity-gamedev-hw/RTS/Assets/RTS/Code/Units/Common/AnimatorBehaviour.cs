using Client.Components.Attack;
using Client.Components.Health;
using Client.Components.Visual;
using Client.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Entities;
using System;
using UnityEngine;

namespace Code.Visual {

    public sealed class AnimatorBehaviour : MonoBehaviour, IDisposable {
        private static readonly int s_attackRequest = Animator.StringToHash("Attack");

        public const string ATTACK_EVENT = "AttackAction";
        public const string DESTROY_EVENT = "DestroyAction";

        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationDispatcher _animationDispatcher;

        private EcsWorld _world;
        private EcsPool<AttackEvent> _attackEventPool;
        private EcsPool<DeathEvent> _deathEventPool;

        private Entity _entity;

        public void Init(Entity entity) {
            _entity = entity;
            _world = EcsStartup.Instance.DefaultWorld;

            _attackEventPool = _world.GetPool<AttackEvent>();
            _deathEventPool = _world.GetPool<DeathEvent>();

            _entity.AddData(new AnimatorView { Value = _animator });

            _animationDispatcher.OnEventReceived += OnEventReceived;
        }

        private void OnEventReceived(string eventName) {

            if (eventName == ATTACK_EVENT)
                OnAttacked();

            if (eventName == DESTROY_EVENT)
                OnDestroed();

        }

        private void OnAttacked() {
            _animator.ResetTrigger(s_attackRequest);

            if (_attackEventPool.Has(_entity.Id) == false)
                _attackEventPool.Add(_entity.Id);
        }

        private void OnDestroed() {

            if (_deathEventPool.Has(_entity.Id) == false)
                _deathEventPool.Add(_entity.Id);
        }

        public void Dispose() {
            _animationDispatcher.OnEventReceived -= OnEventReceived;
        }
    }
}


