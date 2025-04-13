using R3;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace PresentationModel {
    public sealed class LevelUpButton : MonoBehaviour {
        [SerializeField] private LevelUpButtonState _state;
        [SerializeField] private Button _button;
        [Space]
        [SerializeField] private Image _background;
        [Space]
        [SerializeField] private Sprite _availableSprite;
        [SerializeField] private Sprite _lockedSprite;

        public Button Button => _button;
        public Observable<Unit> OnClickObservable => _button.OnClickAsObservable();

        public void SetAvailable(bool isAvailable) {
            var state = isAvailable ?
                LevelUpButtonState.Available :
                LevelUpButtonState.Locked;

            SetState(state);
        }

        public void SetState(LevelUpButtonState state) {
            _state = state;

            switch (state) {
                case LevelUpButtonState.Available:
                    Button.interactable = true;
                    _background.sprite = _availableSprite;
                    break;

                case LevelUpButtonState.Locked:
                    Button.interactable = false;
                    _background.sprite = _lockedSprite;
                    break;

                default:
                    throw new Exception($"Undefined button state {state}!");
            }
        }
    }
}
