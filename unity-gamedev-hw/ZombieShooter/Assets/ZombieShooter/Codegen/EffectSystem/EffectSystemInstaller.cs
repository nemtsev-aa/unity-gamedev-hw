using Atomic.Contexts;
using AtomicFramework.Effects;
using System;
using UnityEngine;

namespace ZombieShooter.UI {

    [Serializable]
    public sealed class EffectSystemInstaller : IContextInstaller {
        [SerializeField] private EffectSystemConfig _config;
        [SerializeField] private RectTransform _root;

        public void Install(IContext context) {
            context.AddEffectSystemConfig(_config);
            context.AddSystem(new EffectViewFactory(_root, _config));
        }
    }
}
