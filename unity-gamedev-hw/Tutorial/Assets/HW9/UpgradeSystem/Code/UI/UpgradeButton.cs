using R3;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UpgradesSystem.UI {

    public sealed partial class UpgradeButton : MonoBehaviour {
        [SerializeField] private Button _button;
        [Space, SerializeField] private Image _buttonBackground;
        [SerializeField] private Sprite _availableButtonSprite;
        [SerializeField] private Sprite _lockedButtonSprite;
        [SerializeField] private Sprite _maxButtonSprite;
        [Space, SerializeField] private TextMeshProUGUI _priceText;
        [Space, SerializeField] private GameObject _maxTextGO;
        [SerializeField] private GameObject _titleTextGO;
        [SerializeField] private GameObject _priceContainer;
        [Space, SerializeField] private UpgradeButtonStates _state;

        public Observable<Unit> Clicked => _button.OnClickAsObservable();
        public Button Button => _button;
        public UpgradeButtonStates CurrentState => _state;

        public void SetPrice(string price) {
            _priceText.text = price;
        }

        public void SetState(UpgradeButtonStates state) {
            _state = state;

            switch (_state) {
                case UpgradeButtonStates.AVAILABLE:
                    _button.interactable = true;
                    _buttonBackground.sprite = _availableButtonSprite;

                    _priceContainer.SetActive(true);
                    _titleTextGO.SetActive(true);
                    _maxTextGO.SetActive(false);

                    break;

                case UpgradeButtonStates.LOCKED:
                    _button.interactable = false;
                    _buttonBackground.sprite = _lockedButtonSprite;

                    _priceContainer.SetActive(true);
                    _titleTextGO.SetActive(true);
                    _maxTextGO.SetActive(false);

                    break;

                case UpgradeButtonStates.MAX:
                    _button.interactable = false;
                    _buttonBackground.sprite = _maxButtonSprite;

                    _priceContainer.SetActive(false);
                    _titleTextGO.SetActive(false);
                    _maxTextGO.SetActive(true);

                    break;

                default:
                    throw new Exception($"Undefined button state {state}!");
            }
        }

#if UNITY_EDITOR
        private void OnValidate() {
            try {
                SetState(_state);
            }
            catch (Exception) {
                // ignored
            }
        }
#endif
    }
}
