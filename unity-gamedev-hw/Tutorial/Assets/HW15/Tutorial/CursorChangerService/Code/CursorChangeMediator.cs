using R3;
using System;

namespace CursorChangeService {

    public sealed class CursorChangeMediator : IDisposable {
        private readonly CursorChangeRaycaster _raycaster;
        private readonly CursorChangeView _view;
        private readonly CompositeDisposable _disposables = new();

        private CursorType _currentCursorType = CursorType.Default;

        public CursorChangeMediator(CursorChangeRaycaster raycaster,
                                    CursorChangeView view) {

            _raycaster = raycaster;
            _view = view;

            CreateReactiveSubscribes();
        }

        public void ActivateRaycaster() {
            _raycaster.Activate();
        }

        public void ShowCursorByType(CursorType type) {

            if (_currentCursorType == type)
                return;

            _currentCursorType = type;
            _view.SetCursor(_currentCursorType);
        }

        private void CreateReactiveSubscribes() {

            _raycaster.CurrentCursorType
                    .Subscribe(ShowCursorByType)
                    .AddTo(_disposables);
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}

