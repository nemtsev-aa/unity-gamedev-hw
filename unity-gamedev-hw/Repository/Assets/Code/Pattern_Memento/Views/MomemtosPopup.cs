using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Pattern_Memento {

    public sealed class MomemtosPopup : MonoBehaviour, IDisposable {
        [SerializeField] private MementoView _mementoViewPrefab;
        [SerializeField] private RectTransform _viewParent;

        [Space(10)]
        [SerializeField] private Button _addSaveButton;
        [SerializeField] private Button _restoreSaveButton;

        private readonly CompositeDisposable _compositeDisposable = new();
        private List<MementoView> _views = new();
        private MementoView _currentView;

        public Observable<Unit> OnAddSaveButtonClicked => _addSaveButton.OnClickAsObservable();
        public Observable<Unit> OnRestoreSaveButtonClicked => _restoreSaveButton.OnClickAsObservable();
        public MementoView CurrentMementoView => _currentView;
        
        public void Show(bool status) {
            gameObject.SetActive(status);
        }

        public void UpdateViews(IReadOnlyList<IMementos> history) {

            if (_views.Count == 0) {
                CreateViews(history);
                return;
            }

            var newViews = new List<MementoView>();

            foreach (var memento in history) {
                bool found = false;

                foreach (var view in _views) {

                    if (view.ID == memento.ID) {
                        newViews.Add(view);
                        found = true;

                        break;
                    }
                }

                if (found == false)
                    newViews.Add(CreateView(memento));

            }

            foreach (var oldView in _views) {
                bool existsInHistory = history.Any(m => m.ID == oldView.ID);

                if (existsInHistory == false) 
                    DestroyView(oldView);

                _views = newViews;
            }
        }

        private void CreateViews(IReadOnlyList<IMementos> history) {

            foreach (var iMementos in history) {
                _views.Add(CreateView(iMementos));
            }
        }

        private MementoView CreateView(IMementos mementos) {
            MementoView view = Instantiate(_mementoViewPrefab, _viewParent);
            MementoViewModel viewModel = new MementoViewModel(mementos.ID);

            view.Init(viewModel);
            view.ViewSelected
                .Subscribe(OnSelectedViewChanged)
                .AddTo(_compositeDisposable);

            return view;
        }

        private void DestroyView(MementoView view) {
            Destroy(view.gameObject);
        }

        private void OnSelectedViewChanged(MementoView view) {

            if (_currentView != null) 
                _currentView.SetSelectionStatus(false);

            _currentView = view;
            _currentView.SetSelectionStatus(true);
        }

        private void DestroyViews() {

            if (_views.Count == 0)
                return;

            foreach (var iView in _views) {
                Destroy(iView.gameObject);
            }

            _views.Clear();
        }

        public void Dispose() {
            if (_compositeDisposable.IsDisposed == false) {
                _compositeDisposable.Dispose();
            }
        }
    }
}