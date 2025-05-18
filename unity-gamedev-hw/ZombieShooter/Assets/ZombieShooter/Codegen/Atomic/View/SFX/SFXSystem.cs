using System;
using UnityEngine;
using Atomic.Entities;
using Atomic.Elements;
using System.Collections.Generic;

namespace AtomicFramework.View.SFX {

    [Serializable]
    public sealed class SFXSystem : IEntityInit {
        private AudioSource _audioSource;
        private List<SFXData> _collection;
        
        public void Init(IEntity entity) {

            _audioSource = entity.GetAudioSource();
            _collection = entity.GetSFXCollection();

            foreach (var sfxData in _collection) {

                if (sfxData == null || string.IsNullOrEmpty(sfxData.ActionId.ToString())) {
                    Debug.LogWarning("Invalid AudioEffectData in collection");

                    continue;
                }

                SubscribeToAnyEvent(entity, sfxData);
            }
        }

        private void SubscribeToAnyEvent(IEntity entity, SFXData sfxData) {
            var eventObj = entity.GetValue<object>(sfxData.ActionId);

            switch (eventObj) {
                case BaseEvent<float> floatEvent:
                    floatEvent.Subscribe(_ => ActivateAudioEffect(sfxData));
                    break;

                case BaseEvent<bool> boolEvent:
                    boolEvent.Subscribe(value => {

                        if (value)
                            ActivateAudioEffect(sfxData);
                    });
                    break;

                case BaseEvent baseEvent:
                    baseEvent.Subscribe(() => ActivateAudioEffect(sfxData));
                    break;

                default:
                    Debug.LogWarning($"Unsupported event type for ID: {sfxData.ActionId}");
                    break;
            }
        }

        private void ActivateAudioEffect(SFXData sfxData) {

            if (sfxData.Effect == null) {
                Debug.LogError($"AudioClip is null for ID: {sfxData.ActionId}");
                return;
            }

            _audioSource.PlayOneShot(sfxData.Effect);
        }
    }
}


