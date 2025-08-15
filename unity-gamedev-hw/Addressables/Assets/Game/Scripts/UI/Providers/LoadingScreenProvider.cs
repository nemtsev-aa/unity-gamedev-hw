using System;
using System.Threading;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UI.Components.Screens;

namespace UI.Core {

    public class LoadingScreenProvider : IDisposable {
        private readonly UIManager _uiManager;
        //private readonly Queue<ILoadingOperation> _operationQueue = new();

        private bool _isLoading;
        private LoadingScreen _currentLoadingScreen;
        private CancellationTokenSource _cts;

        public LoadingScreenProvider(UIManager uiManager) {
            _uiManager = uiManager;
            _cts = new CancellationTokenSource();
        }

        public async UniTask ShowLoadingAsync(string message = "Loading...", bool cancellable = false) {

            if (_currentLoadingScreen == null) {
                _currentLoadingScreen = await _uiManager.ShowScreenAsync<LoadingScreen>(UIScreenType.LoadingScreen, false);
                _currentLoadingScreen.SetLoadingText(message);
                _currentLoadingScreen.SetCancellable(cancellable);

                return;
            }

            _currentLoadingScreen.ResetProgress();
            ProgressChanged(0f);
        }

        public async UniTask HideLoadingAsync() {

            if (_currentLoadingScreen != null) {
                await _uiManager.HideScreenAsync(UIScreenType.LoadingScreen);
                _currentLoadingScreen = null;
            }
        }

        public void UpdateLoadingText(string message) {

            if (message != null)
                _currentLoadingScreen.SetLoadingText(message);
        }

        public void ProgressChanged(float progress) {
            _currentLoadingScreen.SetProgress(progress);
        }

        //public async UniTask ExecuteWithLoadingAsync(ILoadingOperation operation, string customMessage = null) {
        //    await ShowLoadingAsync(customMessage ?? operation.Description);

        //    try {
        //        await operation.Load(OnProgress);
        //    }
        //    finally {
        //        await HideLoadingAsync();
        //    }
        //}

        //    public async UniTask ExecuteOperationsAsync(Queue<ILoadingOperation> operations) {

        //        if (_isLoading == true)
        //            return;

        //        _isLoading = true;

        //        try {
        //            await ShowLoadingAsync();

        //            var totalOperations = operations.Count;
        //            var completedOperations = 0;

        //            while (operations.Count > 0) {
        //                _currentLoadingScreen.ResetProgress();

        //                var operation = operations.Dequeue();
        //                var operationProgress = 0f;

        //                await operation.Load(progress => {
        //                    operationProgress = progress;
        //                    var totalProgress = (completedOperations + operationProgress) / totalOperations;
        //                    //Debug.Log($"TotalProgress {totalProgress}");

        //                    OnProgress(totalProgress);
        //                    UpdateLoadingText(operation.Description);
        //                    //Debug.Log($"{operation.Description} progress {operationProgress}");
        //                });

        //                completedOperations++;
        //            }
        //        }
        //        finally {
        //            await HideLoadingAsync();
        //            _isLoading = false;
        //        }
        //    }


        //    public void Dispose() {
        //        _cts?.Cancel();
        //        _cts?.Dispose();
        //    }

        public void Dispose() {
            
        }
    }
}
