using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChestsSystem {
    
    public sealed class ChestsPopup : MonoBehaviour, IDisposable {
        
        public bool IsActive {
            get { return _views.Count > 0; }
        } 
        
        [SerializeField] private Button _closeButton;
        [SerializeField] private ChestView _viewPrefab;
        [SerializeField] private RectTransform _container;

        private IChestsPopupViewModel _viewModel;
        private CompositeDisposable _disposables = new CompositeDisposable();
        private List<ChestView> _views = new List<ChestView>();

        public void Init(IChestsPopupViewModel viewModel) {
            _viewModel = viewModel;
            _closeButton.onClick.AddListener(OnCloseButtonClick);
        }

        public void Show(bool status) {

            gameObject.SetActive(status);

            if (status == true)
                CreateViews();
        }

        private void CreateViews() {
            var chests = _viewModel.Chests;

            if (chests.Count == 0)
                throw new ArgumentException($"ReactiveChests not found!");

            if (_views.Count > 0)
                return;

            foreach (var iChest in chests) {
                ReactiveChest rChest = iChest.Value;
                ChestVisual modelPrefab = _viewModel.ChestModelProvider.GetModelByType(rChest.Type);
                ChestView newView = Instantiate(_viewPrefab, _container);
                ChestVisual model = Instantiate(modelPrefab, newView.transform);
                
                ChestViewModel viewModel = new ChestViewModel(rChest, model);
                newView.Init(viewModel);
                CreateReactiveSubscribes(newView);

                _views.Add(newView);
            }
        }

        private void ClearViews() {

            foreach (var iView in _views) {
                iView.Dispose();
                Destroy(iView.gameObject);
            }

            _views.Clear();
        }

        private void CreateReactiveSubscribes(ChestView newView) {

            newView.OpenAnimationFinished
                    .Subscribe(OnOpenAnimationFinished)
                    .AddTo(_disposables);
        }

        private void OnCloseButtonClick() =>
            Show(false);

        private void OnOpenAnimationFinished(ReactiveChest chest) {
            _viewModel.TryOpenReactiveChest.Execute(chest);
        }

        public void Dispose() {

            if (_viewModel != null)
                _viewModel = null;

            ClearViews();
            _closeButton.onClick.RemoveListener(OnCloseButtonClick);
        }
    }
}
