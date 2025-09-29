using Client.Components.Health;
using Client.Components.Visual;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems {

    internal sealed class FXDamageListenerSystem : IEcsRunSystem {
        private const float SMALLDAMAGE_EFFECT_LIMIT = 0.6f;
        private const float HIGHTDAMAGE_EFFECT_LIMIT = 0.4f;
        private const float DESTROY_EFFECT_LIMIT = 0f;

        private readonly EcsFilterInject<
            Inc<Health, SmallDamageEffect, HightDamageEffect, DestroyEffect>> _filter;

        public void Run(IEcsSystems systems) {
            EcsPool<Health> healthPool = _filter.Pools.Inc1;
            EcsPool<SmallDamageEffect> smallDamageEffectPool = _filter.Pools.Inc2;
            EcsPool<HightDamageEffect> hightDamageEffectPool = _filter.Pools.Inc3;
            EcsPool<DestroyEffect> destroyEffectPool = _filter.Pools.Inc4;

            foreach (int entity in _filter.Value) {
                var health = healthPool.Get(entity);
                var currentHealth = health.Value;
                var maxHealth = health.MaxValue;

                ParticleSystem smallEffect = smallDamageEffectPool.Get(entity).Value;
                ParticleSystem hightEffect = hightDamageEffectPool.Get(entity).Value;
                ParticleSystem destroyEffect = destroyEffectPool.Get(entity).Value;

                float percent = (float)currentHealth / maxHealth;
                //Debug.Log($"FXRequestListenerSystem: {percent}");

                if (percent <= DESTROY_EFFECT_LIMIT) {

                    if (hightEffect.isPlaying == true) {
                        hightEffect.Stop();
                        hightEffect.gameObject.SetActive(false);
                    }

                    if (destroyEffect.isPlaying == true)
                        return;

                    destroyEffect.gameObject.SetActive(true);
                    destroyEffect.Play();

                    return;
                }

                if (percent <= HIGHTDAMAGE_EFFECT_LIMIT) {

                    if (smallEffect.isPlaying == true) {
                        smallEffect.Stop();
                        smallEffect.gameObject.SetActive(false);
                    }

                    if (hightEffect.isPlaying == true)
                        return;

                    hightEffect.gameObject.SetActive(true);
                    hightEffect.Play();
                }

                if (percent <= SMALLDAMAGE_EFFECT_LIMIT) {

                    if (smallEffect.isPlaying == true)
                        return;

                    smallEffect.gameObject.SetActive(true);
                    smallEffect.Play();
                }
            }
        }
    }
}
