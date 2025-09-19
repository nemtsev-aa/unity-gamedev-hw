using System;
using UnityEngine;
using Tutorial.UI;
using Currencies.UI;
using Cysharp.Threading.Tasks;
using BehaviorTree.PlayerCompanents;
using NavigatorService;
using BehaviorTree.PlayerCoreSubsystem;
using InteractionService;
using ShopSystem.Helpers;
using ShopSystem.Product.Data;

namespace Tutorial.Core {

    [Serializable]
    public sealed class SellResourceStepController : TutorialStateControllerBase {
        private const string LABEL = "Collect Wood";
        private readonly Transform _target;
        private CurrencyProvider _currencyProvider;

        private readonly InteractionHandler _interactionHandler;
        private readonly InventoryCompanent _inventory;
        private readonly Navigator _navigator;
        private readonly SellCatalog _sellCatalog;
        private readonly ProductSeller _seller;
        private readonly TutorialStepInfoView _tutorialStepInfoView;

        public SellResourceStepController(SellResourceStepController_Config config,
                                          PlayerProvider playerComponents,
                                          Navigator navigator,
                                          SellCatalog sellCatalog,
                                          ProductSeller seller,
                                          TutorialStepInfoView tutorialStepInfoView) {

            _target = config.Target;
            _currencyProvider = config.CurrencyProvider;

            _interactionHandler = playerComponents.InteractionHandler;
            _inventory = playerComponents.Inventory;
            _navigator = navigator;
            _sellCatalog = sellCatalog;
            _seller = seller;
            _tutorialStepInfoView = tutorialStepInfoView;

            TutorialStep = TutorialStep.SellResource;
        }

        public override void Init(TutorialState state) {
            base.Init(state);

            TutorialState.StepFinished += OnStepFinished;
        }

        public override void OnStepStarted(TutorialStep step) {
            base.OnStepStarted(step);

            if (HasStarted == false)
                return;

            _navigator.SetTarget(_target);
            _currencyProvider.Show(true);

            _inventory.CurrentAmountChanged += OnCurrentAmountChanged;
            _interactionHandler.InteractionStarted += OnInteractionStarted;

            _tutorialStepInfoView.UpdateDescription($"{LABEL} 0/{_inventory.MaxAmount}");
        }

        private void OnStepFinished(TutorialStep step) {

            if (step != TutorialStep)
                return;

            _tutorialStepInfoView.UpdateDescription($"");

            TutorialState.StepFinished -= OnStepFinished;
        }

        private void OnInteractionStarted(InteractionSource source) {

            if (source is SellResourceLoot loot) {
                PreparationBeforeSell(loot);
                SellProgress(loot).Forget();
                return;
            }
        }

        private void OnCurrentAmountChanged(int amount) {

            if (_inventory.CurrentAmount == 0) {
                Dispose();

                TutorialState.FinishStep();
                TutorialState.NextStep();
            }
        }

        private void PreparationBeforeSell(SellResourceLoot loot) {
            string id = loot.Type.ToString();

            if (_inventory.TryFindItem(id, out InventoryItem item) == true) {
                var itemAmount = item.Amount + 1;

                for (int i = 1; i < itemAmount; i++) {
                    _sellCatalog.AddProduct(id);
                }
            }
        }

        private async UniTask SellProgress(SellResourceLoot loot) {
            string id = loot.Type.ToString();

            if (_inventory.TryFindItem(id, out InventoryItem item) == true) {

                var itemAmount = item.Amount + 1;

                for (int i = 1; i < itemAmount; i++) {

                    if (_seller.TrySellProduct(item.ID) == true) {
                          _tutorialStepInfoView.UpdateDescription($"{LABEL} {i}/{_inventory.MaxAmount}");
                        var newInventoryItem = new InventoryItem(id, 1);

                        if (_inventory.TryRemoveItem(newInventoryItem) == true)
                            await UniTask.WaitForSeconds(0.3f);
                    }
                }
            }
        }

        public override void Dispose() {
            TutorialState.StepFinished -= OnStepFinished;
            _interactionHandler.InteractionStarted -= OnInteractionStarted;
            _inventory.CurrentAmountChanged -= OnCurrentAmountChanged;
        }
    }
}