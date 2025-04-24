using Cysharp.Threading.Tasks;
using ObservableCollections;
using R3;
using System;

namespace Pattern_Memento {
    public sealed class MementoHistoryController : IDisposable {

        private readonly MomemtosPopup _view;
        private readonly MementoHistory _model;
        private readonly IMementoHandler _mementosManager;
        private readonly CompositeDisposable _compositeDisposable = new();

        public MementoHistoryController(MementoHistory model, MomemtosPopup view, IMementoHandler mementosManager) {
            _model = model;
            _view = view;
            _mementosManager = mementosManager;

            CreateReactiveSubscribes();
            InitView();
        }

        public void AddMemento() {
            _model.AddMemento(_mementosManager.SaveState());
            _view.UpdateViews(_model.History);
        }

        public void RestoreMemento() {

            if (_view.CurrentMementoView == null) {
                _mementosManager.RestoreState(_model.GetLastMemento());
                return;
            }

            _mementosManager.RestoreState(_model.GetMementosByID(_view.CurrentMementoView.ID));
        }

        private void CreateReactiveSubscribes() {
            _view.OnAddSaveButtonClicked
                .Subscribe(OnAddSave)
                .AddTo(_compositeDisposable);

            _view.OnRestoreSaveButtonClicked
                .Subscribe(OnRestoreSave)
                .AddTo(_compositeDisposable);

            _model.History.CollectionChanged += OnHistory_CollectionChanged;
        }

        private void InitView() {
            if (_model.History.Count > 0)
                _view.UpdateViews(_model.History);
        }

        private void OnAddSave(Unit unit) =>
            AddMemento();

        private void OnRestoreSave(Unit unit) =>
            RestoreMemento();

        private void OnHistory_CollectionChanged(in NotifyCollectionChangedEventArgs<IMementos> e) {

            _view.UpdateViews(_model.History);
            CollectionActionsBehaviour(e);
        }

        private void CollectionActionsBehaviour(NotifyCollectionChangedEventArgs<IMementos> e) {
            switch (e.Action) {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    //Debug.Log($"History List changed: Add new companent!");
                    break;

                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    //Debug.Log($"History List changed: Remove companent!");
                    break;

                default:
                    break;
            }
        }

        public void Dispose() {

            if (_compositeDisposable.IsDisposed == false)
                _compositeDisposable.Dispose();

            _model.History.CollectionChanged -= OnHistory_CollectionChanged;
        }
    }
}
