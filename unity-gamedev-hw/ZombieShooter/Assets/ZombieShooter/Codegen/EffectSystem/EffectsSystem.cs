using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using AtomicFramework.Contextes;
using AtomicFramework.EnemySystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AtomicFramework.Effects {

    public sealed class EffectsSystem : IEntityInit, IEntityUpdate, IEntityDispose {
        private readonly Dictionary<EffectType, IEffect> _activeEffects = new();

        private IEntity _entity;
        private EffectSystemConfig _configs;
        private IEvent<bool> _speedBoostAction;
        private BaseEvent<float> _takeDamageAction;
        private EffectConfig _stunEffectConfig;
        private SpeedBoostEffectConfig _speedBoostEffectConfig;

        private bool _showLogAfterExecution = false;

        public BaseEvent<IEffect> AddEffectAction { get; private set; }

        public EffectsSystem() {
            AddEffectAction = new BaseEvent<IEffect>();
        }

        public void Init(IEntity entity) {
            _entity = entity;

            _takeDamageAction = entity.GetTakeDamageAction();
            _takeDamageAction.Subscribe(OnTakeDamage);

            var context = GameContext.Instance;
            var killEnemyCount = context.GetSystem<EnemyManager>().KillEnemyCount;
            killEnemyCount.Subscribe(OnKillEnemyCountChanged);

            _configs = context.GetEffectSystemConfig();
            _stunEffectConfig = _configs.GetConfigByType(EffectType.Stun);
            _speedBoostEffectConfig = (SpeedBoostEffectConfig)_configs.GetConfigByType(EffectType.SpeedBoost);

            if (_showLogAfterExecution == true)
                Debug.Log($"{nameof(EffectsSystem)} installed!");
        }

        public void OnUpdate(IEntity entity, float deltaTime) {

            foreach (var effect in _activeEffects.Values.ToList()) {
                effect.Duration -= deltaTime;

                if (effect.Duration <= 0) {
                    effect.Remove(entity);
                    _activeEffects.Remove(effect.Type);
                }
            }
        }

        public void AddEffect(IEffect effect) {

            if (_activeEffects.TryGetValue(effect.Type, out var existingEffect)) {
                existingEffect.Duration = Mathf.Max(existingEffect.Duration, effect.Duration);
                AddEffectAction?.Invoke(existingEffect);
            } else {
                _activeEffects.Add(effect.Type, effect);
                effect.Apply(_entity);
                AddEffectAction?.Invoke(effect);
            }
        }

        private void OnTakeDamage(float damage) {

            if (_entity.GetIsDeath().Value == true)
                return;

            AddEffect(new StunEffect(_stunEffectConfig.Duration));
        }

        private void OnKillEnemyCountChanged(int value) {

            if (value % _speedBoostEffectConfig.ActivateCondition == 0)
                AddEffect(new SpeedBoostEffect(_speedBoostEffectConfig.Duration));
        }

        public void Dispose(IEntity entity) {
            _takeDamageAction.Unsubscribe(OnTakeDamage);

            foreach (var effect in _activeEffects.Values) {
                effect.Remove(entity);
            }

            _activeEffects.Clear();
        }
    }
}