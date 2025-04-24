using R3;
using System;
using Pattern_Memento;
using SaveLoadSystem.Core;

namespace SaveLoadSystem {

    public sealed class SaveLoadMediator : IDisposable {
        private readonly SaveLoadManager _saveLoadManager;
        private readonly MementoCoordinator _coordinator;
        private readonly SaveLoadPopup _popup;
        private readonly CompositeDisposable _compositeDisposable = new();

        public SaveLoadMediator(SaveLoadManager manager, MementoCoordinator coordinator, SaveLoadPopup popup) {
            _saveLoadManager = manager;
            _coordinator = coordinator;
            _popup = popup;

            CreateReactiveSubscribes();
        }

        private void CreateReactiveSubscribes() {
            _coordinator.MementosCreated
                .Subscribe(OnSave)
                .AddTo(_compositeDisposable);

            _popup.OnLoalButtonClicked
                .Subscribe(OnLoadState)
                .AddTo(_compositeDisposable);

            _saveLoadManager.StateLoaded
                .Subscribe(OnStateLoad)
                .AddTo(_compositeDisposable);
        }

        private void OnStateLoad(Unit unit) {
            _coordinator.RestoreMementos();
        }

        private void OnSave(Unit unit) =>
            _saveLoadManager.SaveGame();

        private void OnLoadState(Unit unit) =>
            _saveLoadManager.LoadGame();

        public void Dispose() {

            if (_compositeDisposable.IsDisposed == false) {
                _compositeDisposable.Dispose();
            }
        }
    }
}




