using UnityEngine;
using Tutorial.UI;
using FarmingSystem;
using BehaviorTree.PlayerCompanents;
using BehaviorTree.PlayerCoreSubsystem;
using NavigatorService;
using InteractionService;
using ShopSystem.Product.Data;

namespace Tutorial.Core {

    public sealed class CollectLootStepController : TutorialStateControllerBase {
        private const string LABEL = "Collect Wood";

        private readonly Transform _target;
        private readonly int _lumberMaxCount;

        private readonly InteractionHandler _interactionHandler;
        private readonly InventoryCompanent _inventory;
        private readonly CollectorCompanent _collector;

        private readonly Navigator _navigator;
        private readonly SellCatalog _sellCatalog;
        private readonly TutorialStepInfoView _tutorialStepInfoView;
        
        private int _lumberCollectCount = 0;

        public CollectLootStepController(CollectLootStepController_Config config,
                                         PlayerProvider playerComponents,
                                         Navigator navigator,
                                         SellCatalog sellCatalog,
                                         TutorialStepInfoView tutorialStepInfoView) {

            _target = config.Target;
            _lumberMaxCount = config.LumberMaxCount;

            _interactionHandler = playerComponents.InteractionHandler;
            _inventory = playerComponents.Inventory;
            _collector = playerComponents.Collector;
            _navigator = navigator;

            _tutorialStepInfoView = tutorialStepInfoView;

            TutorialStep = TutorialStep.CollectLoot;
        }

        public override void Init(TutorialState state) {
            base.Init(state);
        }

        public override void OnStepStarted(TutorialStep step) {
            base.OnStepStarted(step);

            if (HasStarted == false)
                return;

            _inventory.CurrentAmountChanged += OnCurrentAmountChanged;
            _interactionHandler.InteractionStarted += OnInteractionStarted;
            _interactionHandler.InteractionComplited += OnInteractionComplited;

            _navigator.SetTarget(_target);

            _tutorialStepInfoView.UpdateDescription($"{LABEL} {_lumberCollectCount}/{_lumberMaxCount}");
        }

        private void OnInteractionStarted(InteractionSource source) {

            if (source is LootCollect collect) 
                AddItemToInventory(collect.Loot);
        }

        private void OnInteractionComplited(InteractionSource source) {
            _collector.Activate(false);
        }

        private void OnCurrentAmountChanged(int amount) {
            _tutorialStepInfoView.UpdateDescription($"{LABEL} {amount}/{_lumberMaxCount}");

            if (_inventory.IsFill == true) {
                Dispose();

                TutorialState.FinishStep();
                TutorialState.NextStep();
            }
        }

        private void AddItemToInventory(Loot loot) {

            if (loot == null || loot.gameObject.activeInHierarchy == false)
                return;

            if (_inventory.IsFill == true || _inventory.VacantPlace < loot.Amount)
                return;

            _collector.Activate(true);

            if (_collector.TryCollect(loot) == false)
                return;

            var item = new InventoryItem(loot.Id, loot.Amount);

            _inventory.Activate(true);

            if (_inventory.TryAddItem(item) == true)
                Debug.Log($"Add Item Succes!");
        }

        public override void Dispose() {
            _interactionHandler.InteractionStarted -= OnInteractionStarted;
            _interactionHandler.InteractionComplited -= OnInteractionComplited;
            _inventory.CurrentAmountChanged -= OnCurrentAmountChanged;
        }
    }
}