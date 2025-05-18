using Atomic.Contexts;
using Atomic.Entities;
using AtomicFramework.Contextes;
using AtomicFramework.Effects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieShooter.UI {

    public sealed class EffectViews : MonoBehaviour, IDisposable {
        private EffectViewFactory _factory;
        private EffectsSystem _system;
        private List<EffectView> _views;
        private EffectView _currentView;
         
        public void Init() {
            var context = GameContext.Instance;

            _factory = context.GetSystem<EffectViewFactory>();
            _system = context.GetCharacter().GetBehaviour<EffectsSystem>();
            _system.AddEffectAction.Subscribe(ShowEffect);

            _views = new List<EffectView>();
        }

        public void ShowEffect(IEffect effect) {

            if (_currentView != null)
                RemoveView(_currentView);

            _currentView = CreateView(effect);
            _currentView.OnComplite.Subscribe(RemoveView);
        }

        private EffectView CreateView(IEffect effect) {
            return _factory.Get(effect);
        }

        private void RemoveView(EffectView view) {
            view.OnComplite.Unsubscribe(RemoveView);
            _views.Remove(view);

            Destroy(view.gameObject);
        }

        public void Dispose() {
            _system.AddEffectAction.Unsubscribe(ShowEffect);
        }
    }
}
