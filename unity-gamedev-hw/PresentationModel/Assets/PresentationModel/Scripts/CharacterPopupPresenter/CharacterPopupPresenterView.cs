using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PresentationModel {
    public sealed class CharacterPopupPresenterView : MonoBehaviour, IDisposable {
        [SerializeField] private Button _initButton;
        [SerializeField] private Button _showPopupButton;
        [SerializeField] private Button _addExperienceButton;

        private readonly CompositeDisposable _compositeDisposable = new();
        private List<Button> _buttons;

        public Observable<Unit> OnInitClickObservable => _initButton.OnClickAsObservable();
        public Observable<Unit> OnShowPopupClickObservable => _showPopupButton.OnClickAsObservable();
        public Observable<Unit> OnAddExperienceClickObservable => _addExperienceButton.OnClickAsObservable();

        public void Init() {
            CreateListButtons();
            CreateReactiveSubscribes();
            
            Show(false);
        }

        public void Show(bool status) {
            gameObject.SetActive(status);
            Reset();
        }

        public void Reset() {
            HideAllButton();

            if (gameObject.activeSelf == true)
                ShowButton(_initButton);
        }

        private void CreateListButtons() {
            _buttons = new List<Button>();

            if (_initButton != null)
                _buttons.Add(_initButton);

            if (_showPopupButton != null)
                _buttons.Add(_showPopupButton);

            if (_addExperienceButton != null)
                _buttons.Add(_addExperienceButton);

            HideAllButton();
        }

        private void CreateReactiveSubscribes() {
            OnInitClickObservable
                .Subscribe(OnInitClick)
                .AddTo(_compositeDisposable);

            OnShowPopupClickObservable
                .Subscribe(OnShowPopupClick)
                .AddTo(_compositeDisposable);
        }

        private void OnInitClick(Unit unit) {
            HideAllButton();
            ShowButton(_showPopupButton);
        }

        private void OnShowPopupClick(Unit unit) {
            HideAllButton();
            ShowButton(_addExperienceButton);
        }

        private void HideAllButton() {
            for (int i = 0; i < _buttons.Count; i++) {
                _buttons[i].gameObject.SetActive(false);
            }
        }

        private void ShowButton(Button button) =>
            button.gameObject.SetActive(true);
        

        public void Dispose() {
            if (_compositeDisposable.IsDisposed == false) {
                _compositeDisposable.Dispose();
            }
        }
    }
}


