using R3;
using UnityEngine;
using UnityEngine.UI;

namespace GameplaySystem {

    public sealed class ToolsView : MonoBehaviour {
        [SerializeField] private Button _inventaryButton;
        [SerializeField] private Button _equipmentButton;

        public Observable<Unit> InventaryButtonClicked => _inventaryButton.OnClickAsObservable();
        public Observable<Unit> EquipmentButtonClicked => _equipmentButton.OnClickAsObservable();
    }
}

