using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.UI {

    public sealed class InventoryItemInfoPopup : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _decriptionText;
        [SerializeField] private Image _iconImage;
        [SerializeField] private StackView _stackView;

        [Space, SerializeField] private Button _closeButton;
        [SerializeField] private Button _consumeButton;

        private IInventoryItemPresentationModel _presenter;

        public void Init(IInventoryItemPresentationModel presenter) {
            _presenter = presenter;

            _titleText.text = presenter.Title;
            _decriptionText.text = presenter.Description;
            _iconImage.sprite = presenter.Icon;

            SetupStackContainer(presenter);
            SetupConsumeButton(presenter);

            _consumeButton.onClick.AddListener(OnConsumeButtonClicked);
            _closeButton.onClick.AddListener(OnHideButtonClicked);

            if (gameObject.activeSelf == false)
                gameObject.SetActive(true);
        }
        private void SetupStackContainer(IInventoryItemPresentationModel presenter) {
            var isStackableItem = presenter.IsStackableItem();
            _stackView.SetVisible(isStackableItem);

            if (isStackableItem == true) {
                presenter.GetStackInfo(out var current, out var size);
                _stackView.SetAmount(current, size);
            }
        }

        private void SetupConsumeButton(IInventoryItemPresentationModel presenter) {
            var isConsumableItem = presenter.IsConsumableItem();
            _consumeButton.gameObject.SetActive(isConsumableItem);

            if (isConsumableItem == true)
                _consumeButton.interactable = presenter.CanConsumeItem();
        }

        private void OnConsumeButtonClicked() {
            _presenter.OnConsumeClicked();
            Hide();
        }

        private void OnHideButtonClicked() {
            Hide();
        }

        private void Hide() {
            _consumeButton.onClick.RemoveListener(OnConsumeButtonClicked);
            _closeButton.onClick.AddListener(OnHideButtonClicked);

            if (gameObject.activeSelf == true)
                gameObject.SetActive(false);
        }
    }
}