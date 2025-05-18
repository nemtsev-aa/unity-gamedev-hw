using System;
using UnityEngine;
using Atomic.Contexts;

namespace AtomicFramework.EnemyPointerSystem {

    [Serializable]
    public sealed class PointerSystemInstaller : IContextInstaller {
        [SerializeField] private PointerIcon _pointerIconPrefab;

        public void Install(IContext context) {
            context.AddPointerIconPrefab(_pointerIconPrefab);
            context.AddSystem<PointerSystem>();
        }
    }
}
