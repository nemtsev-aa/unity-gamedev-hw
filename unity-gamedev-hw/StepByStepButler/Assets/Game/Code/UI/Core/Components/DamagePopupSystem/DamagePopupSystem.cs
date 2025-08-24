using EventBusService;
using StepByStepButler.Gameplay.Systems;
using UI.Components.DamagePopupSystem;

namespace StepByStepButler.Gameplay.Heroes {

    public sealed class DamagePopupSystem : IGameSystem {
        private readonly IEventBus _eventBus;
        private readonly DamagePopupPool _popupPool;

        public DamagePopupSystem(IEventBus eventBus, DamagePopupPool popupPool) {
            _eventBus = eventBus;
            _popupPool = popupPool;
        }

        public void OnInitializeGame() {
            _eventBus.Subscribe<DamageEvent>(OnDamageEvent);
            _eventBus.Subscribe<HealEvent>(OnHealEvent);
        }

        public void OnFinishGame() {
            _eventBus.Unsubscribe<DamageEvent>(OnDamageEvent);
            _eventBus.Unsubscribe<HealEvent>(OnHealEvent);
            _popupPool.Reset();
        }

        public void OnRestartGame() {
            _eventBus.Unsubscribe<DamageEvent>(OnDamageEvent);
            _eventBus.Unsubscribe<HealEvent>(OnHealEvent);
            _popupPool.Reset();
        }

        private void OnDamageEvent(DamageEvent evt) {
            _popupPool.ShowDamage(evt.Damage, evt.TargetId);
        }

        private void OnHealEvent(HealEvent evt) {
            _popupPool.ShowHeal(evt.Amount, evt.TargetId);
        }
    }
}