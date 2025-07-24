using R3;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTree.Bot {

    public sealed class ShowHideButton : MonoBehaviour, IDisposable {
        public Observable<Unit> ActionButton => _actionButton.OnClickAsObservable();
        
        [SerializeField] private Image _icon;
        [SerializeField] private Button _actionButton;
        [SerializeField] private TMP_Text _label;
        [Space, SerializeField] private Sprite _showSprite;
        [SerializeField] private Sprite _hideSprite;
        
        private CompositeDisposable _disposables = new();

        public void Init(bool status) {
            ShowDefaultState(status);

            ActionButton
                .Skip(0)
                .Subscribe(OnActionButtonClicked)
                .AddTo(_disposables);
        }

        private void ShowDefaultState(bool status) {

            if (status == true) {
                _icon.sprite = _hideSprite;
                _label.text = "Hide";
            } else {
                _icon.sprite = _showSprite;
                _label.text = "Show";
            }
        }

        private void OnActionButtonClicked(Unit unit) {

            if (_icon.sprite == _showSprite) {
                _icon.sprite = _hideSprite;
                _label.text = "Hide";
            } else {
                _icon.sprite = _showSprite;
                _label.text = "Show";
            }
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}



