using UnityEngine;
using System;
using FarmingSystem;
using BehaviorTree.PlayerCoreSubsystem;
using BehaviorTree.PlayerVisualSubSystem;

namespace BehaviorTree.PlayerCompanents {

    [Serializable]
    public class FellerCompanent : IUpdatedPlayerCompanent {
        public event Action<float> ReloadedProgressChanged;
        public event Action<bool> ReloadCompleted;
        public event Action ActionCompleted;

        public Transform Root { get; private set; }
        public bool IsActive { get; private set; } = false;
        public float FellingDuration { get; private set; }
        public bool IsCooldown { get; private set; } = false;
        public IExtractive Target => _target;

        private float _time;
        private IExtractive _target;
        private PlayerVisual _visual;
        private AttackAnimationHandler _animationHandler;

        public void Init(Transform root, PlayerConfig config) {
            Root = root;
            FellingDuration = config.FellingDuration;

            _visual = Root.GetComponentInParent<Player>().Visual;
        }

        public void Activate(bool status) {
            IsActive = status;

            if (status == true) {
                InitAttackAnimationHandler();
                ReloadCompleted?.Invoke(false);
                _time = FellingDuration;
                
                return;
            }
        }

        public void SetTarget(IExtractive target) {

            if (IsActive == false || IsCooldown == true)
                return;

            if (_target != target)
                _target = target;
        }

        public void Update(float deltaTime) {
            if (IsCooldown == false)
                return;

            _time -= deltaTime;

            var fellingDelay = Mathf.Clamp(_time, 0, FellingDuration);
            ReloadedProgressChanged?.Invoke(fellingDelay);

            if (_time <= 0) {
                _time = FellingDuration;
                IsCooldown = false;

                ReloadCompleted?.Invoke(true);
            }
        }

        private void InitAttackAnimationHandler() {

            if (_animationHandler != null)
                return;

            var dispatcher = _visual.Dispatcher;
            _animationHandler = new AttackAnimationHandler(dispatcher);
            _animationHandler.AnimationStarted += OnAnimationStarted;
            _animationHandler.AttackEventActivated += OnAttackEventActivated;
        }

        private void OnAnimationStarted(bool status) {

            if (status == false) {
                ActionCompleted?.Invoke();
                _target = null;
            }
        }

        private void OnAttackEventActivated() {

            if (_target != null && IsCooldown == false) {
                _target.Extract();

                IsCooldown = true;
            }
        }

        public void Dispose() {
            _animationHandler.Dispose();
            _animationHandler.AnimationStarted -= OnAnimationStarted;
            _animationHandler.AttackEventActivated -= OnAttackEventActivated;
            _animationHandler = null;
        }
    }
}



