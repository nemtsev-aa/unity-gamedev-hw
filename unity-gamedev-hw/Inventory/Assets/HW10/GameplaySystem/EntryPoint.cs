using Zenject;
using UnityEngine;
using InventorySystem.Core;
using Sirenix.OdinInspector;
using Character;

namespace GameplaySystem {

    public sealed class EntryPoint : MonoBehaviour {

        [ShowInInspector] private InventoryHelper _helper;
        [ShowInInspector] private CharacterModel _characterModel;

        [Inject]
        public void Construct(InventoryHelper helper, CharacterModel characterModel) {
            _helper = helper;
            _characterModel = characterModel;
        }

        private void Start() {
            _helper.FillInventory();
        }
    }
}

