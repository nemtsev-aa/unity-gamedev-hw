using R3;
using UnityEngine;
using System.Collections.Generic;
using Zenject;
using System;

namespace Pattern_Memento {

    public class CommonPopup : MonoBehaviour {
        [SerializeField] private MementosTypeSelectionView _selectionTypeView;

        private readonly CompositeDisposable _compositeDisposable = new();
        private MomemtosPopup _unitPopup;
        private MomemtosPopup _resourcePopup;

        private Dictionary<MementoTypes, MementoServices> _services;

        [Inject]
        public void Construct(MementoCoordinator coordinator) {
            _services = coordinator.Services;

            _unitPopup = _services[MementoTypes.Units].Popup;
            _resourcePopup = _services[MementoTypes.Resources].Popup;

            CreateReactiveSubscribes();
        }

        public void Show(bool status) {
            gameObject.SetActive(status);

            _unitPopup.Show(false);
            _resourcePopup.Show(false);
        }

        private void CreateReactiveSubscribes() {

            _selectionTypeView.OnUnitPopupShowButtonClicked
                .Subscribe(OnUnitPopupShow)
                .AddTo(_compositeDisposable);

            _selectionTypeView.OnResourcePopupShowButton
                .Subscribe(OnResourcePopupShow)
                .AddTo(_compositeDisposable);
        }

        private void OnUnitPopupShow(Unit unit) {
            bool status = _unitPopup.gameObject.activeSelf;
            _unitPopup.gameObject.SetActive(!status);
            _resourcePopup.gameObject.SetActive(false);
        }

        private void OnResourcePopupShow(Unit unit) {
            bool status = _resourcePopup.gameObject.activeSelf;
            _resourcePopup.gameObject.SetActive(!status);
            _unitPopup.gameObject.SetActive(false);
        }

        public void Dispose() {

            if (_compositeDisposable.IsDisposed == false) {
                _compositeDisposable.Dispose();
            }
        }
    }
}