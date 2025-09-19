using BehaviorTree.PlayerCompanents;
using InputService;
using InteractionService;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BehaviorTree.PlayerCoreSubsystem {

    public sealed class PlayerCore : MonoBehaviour {
        [field: SerializeField] public MoveCompanent Mover { get; private set; }
        [field: SerializeField] public CollectorCompanent Collector { get; private set; }
        [field: SerializeField] public FellerCompanent Feller { get; private set; }
        [field: SerializeField] public InventoryCompanent Inventory { get; private set; }
        [field: SerializeField] public InteractionHandler InteractionHandler { get; private set; }

        private List<IPlayerCompanent> Companents = new();
        private InputController _inputController;

        [Inject]
        public void Construct(InputController inputController) {
            _inputController = inputController;
        }

        public void Init(PlayerConfig config) {
            Companents.Add(Mover);
            Companents.Add(Collector);
            Companents.Add(Feller);
            Companents.Add(Inventory);

            foreach (var iCompanent in Companents) {
                iCompanent.Init(transform, config);
            }

            InteractionHandler.Init(_inputController, transform, config);
        }

        private void Update() {
            Feller.Update(Time.deltaTime);
        }

        private void FixedUpdate() {
            InteractionHandler.FixedUpdate();
        }
    }
}



