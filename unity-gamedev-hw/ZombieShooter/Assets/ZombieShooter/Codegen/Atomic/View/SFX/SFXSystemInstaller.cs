using UnityEngine;
using Atomic.Entities;
using System.Collections.Generic;

namespace AtomicFramework.View.SFX {

    public sealed class SFXSystemInstaller : SceneEntityInstallerBase {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private List<SFXData> _collection;

        public override void Install(IEntity entity) {

            if (_collection == null || _collection.Count == 0) {
                Debug.LogWarning("SFX collection is empty or null");

                return;
            }

            entity.SetAudioSource(_audioSource);
            entity.SetSFXCollection(_collection);

            entity.AddBehaviour(new SFXSystem());
        }
    }
}
