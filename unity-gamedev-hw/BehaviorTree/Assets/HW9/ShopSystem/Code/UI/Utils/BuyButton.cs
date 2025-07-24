using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ShopSystem.UI {
    public sealed class BuyButton : MonoBehaviour {
        [SerializeField] private ButtonState _state;
        [SerializeField] private Button _button;
        [Space]
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _priceText;
        [Space]
        [SerializeField] private Sprite _availableSprite;
        [SerializeField] private Sprite _lockedSprite;

        private Image _priceIcon;
        public Button Button => _button;

        public void AddListener(UnityAction action) {
            Button.onClick.AddListener(action);
        }

        public void RemoveListener(UnityAction action) {
            Button.onClick.RemoveListener(action);
        }

        public void SetPrice(string price) {
            _priceText.text = price;
        }

        public void SetIcon(Sprite icon) {
            _priceIcon.sprite = icon;
        }

        public void SetAvailable(bool isAvailable) {
            var state = isAvailable ?
                ButtonState.Available :
                ButtonState.Locked;

            SetState(state);
        }

        public void SetState(ButtonState state) {
            _state = state;

            switch (state) {
                case ButtonState.Available:
                    Button.interactable = true;
                    _background.sprite = _availableSprite;
                    break;

                case ButtonState.Locked:
                    Button.interactable = false;
                    _background.sprite = _lockedSprite;
                    break;

                default:
                    throw new Exception($"Undefined button state {state}!");
            }
        }
    }
}
