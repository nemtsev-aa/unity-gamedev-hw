using R3;
using System;

namespace Pattern_Memento {

    public sealed class SaveLoadPopupController : IDisposable {
        private readonly MementoCoordinator _model;
        private readonly SaveLoadPopup _view;
        private readonly CompositeDisposable _compositeDisposable = new();

        public SaveLoadPopupController(MementoCoordinator coordinator, SaveLoadPopup popup) {
            _model = coordinator;
            _view = popup;

            CreateReactiveSubscribes();
        }

        private void CreateReactiveSubscribes() {
            _view.OnSaveButtonClicked
                .Subscribe(OnAddSave)
                .AddTo(_compositeDisposable);
        }

        private void OnAddSave(Unit unit) =>
            _model.CreateMementos();

       

        public void Dispose() {

            if (_compositeDisposable.IsDisposed == false) {
                _compositeDisposable.Dispose();
            }
        }
    }
}

