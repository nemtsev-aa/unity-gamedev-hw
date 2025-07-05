using TMPro;
using UnityEngine;

namespace InventorySystem.UI {

    public sealed class StackView : MonoBehaviour {
        [SerializeField] private GameObject _stackContainer;
        [SerializeField] private TextMeshProUGUI _stackText;

        public void SetVisible(bool isVisible) {
            _stackContainer.SetActive(isVisible);
        }

        public void SetAmount(int count, int max) {
            _stackText.text = $"{count}/{max}";
        }
    }
}