using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.UI {

    public abstract class InventoryItemView : MonoBehaviour {
        [SerializeField] protected TextMeshProUGUI _titleText;
        [SerializeField] protected Image _iconImage;
        [SerializeField] protected StackView _stackView;

        public StackView Stack => _stackView;
        public string ID { get; private set; }
  
        public void SetItemId(string id) {
            ID = id;
        }

        public void SetTitle(string title) {
            _titleText.text = title;
        }

        public void SetIcon(Sprite icon) {
            _iconImage.sprite = icon;
        }
    }
}