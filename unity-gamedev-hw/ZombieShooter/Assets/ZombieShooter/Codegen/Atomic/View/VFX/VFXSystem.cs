using UnityEngine;
using Atomic.Elements;
using Atomic.Entities;
using System.Collections.Generic;

namespace AtomicFramework.View.VFX {

    public sealed class VFXSystem : IEntityInit {
        private List<VFXData> _collection;

        public void Init(IEntity entity) {

            _collection = entity.GetVFXCollection();

            foreach (var vfxData in _collection) {

                if (vfxData == null || string.IsNullOrEmpty(vfxData.Id.ToString())) {
                    Debug.LogWarning("Invalid VFX data in collection");

                    continue;
                }

                SubscribeToAnyEvent(entity, vfxData);
            }
        }

        private void SubscribeToAnyEvent(IEntity entity, VFXData vfxData) {
            var eventObj = entity.GetValue<object>(vfxData.Id);

            switch (eventObj) {
                case BaseEvent<float> floatEvent:
                    floatEvent.Subscribe(_ => ActivateVFX(vfxData));
                    break;

                case BaseEvent<bool> boolEvent:
                    boolEvent.Subscribe(value => {

                        if (value)
                            ActivateVFX(vfxData);
                    });
                    break;

                case BaseEvent baseEvent:
                    baseEvent.Subscribe(() => ActivateVFX(vfxData));
                    break;

                default:
                    Debug.LogWarning($"Unsupported event type for ID: {vfxData.Id}");
                    break;
            }
        }

        private void ActivateVFX(VFXData vfxData) {
            
            if (vfxData.Effect == null) {
                Debug.LogError($"VFX GameObject is null for ID: {vfxData.Id}");
                return;

            }

            var particleSystem = vfxData.Effect.GetComponent<ParticleSystem>();

            if (particleSystem == null) {
                Debug.LogError($"ParticleSystem component missing on VFX for ID: {vfxData.Id}");
                return;

            }

            vfxData.Effect.SetActive(true);
            particleSystem.Play();
        }
    }
}
