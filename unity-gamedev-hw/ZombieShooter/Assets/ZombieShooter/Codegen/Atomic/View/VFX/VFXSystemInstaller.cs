using UnityEngine;
using Atomic.Entities;
using System.Collections.Generic;

namespace AtomicFramework.View.VFX {

    public sealed class VFXSystemInstaller : SceneEntityInstallerBase {
        [SerializeField] private List<VFXData> _collection;

        public override void Install(IEntity entity) {

            if (_collection == null || _collection.Count == 0) {
                Debug.LogWarning("VFX collection is empty or null");

                return;
            }

            entity.SetVFXCollection(_collection);
            entity.AddBehaviour(new VFXSystem());
        }
    }
}
