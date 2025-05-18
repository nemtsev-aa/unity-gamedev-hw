using Atomic.Contexts;
using Atomic.Elements;
using AtomicFramework.Effects;
using UnityEngine;

namespace ZombieShooter.UI {
    public sealed class EffectViewFactory : IContextInit {
        private readonly RectTransform _root;
        private readonly EffectSystemConfig _config;

        private IContext _context;

        public EffectViewFactory(RectTransform root, EffectSystemConfig configs) {
            _root = root;
            _config = configs;
        }

        public void Init(IContext context) {
            _context = context;
        }

        public EffectView Get(IEffect effect) {
            var prefab = _config.GetConfigByType(effect.Type).Prefab;

            if (prefab == null)
                prefab = _config.Configs[0].Prefab;

            EffectViewModel viewModel = new EffectViewModel(
                            effect.Type,
                            new ReactiveVariable<float>(effect.Duration));

            EffectView view = Object.Instantiate(prefab, _root);
            view.Init(viewModel);

            return view;
        }
    }
}
