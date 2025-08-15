using AssetManagementSystem;
using Cysharp.Threading.Tasks;
using System;
using UI.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace SampleGame.Core {

    public sealed class SceneLoaderWithProgress : BaseSceneLoader {
        private readonly LoadingScreenProvider _loadingScreen;

        public SceneLoaderWithProgress(AssetManager assetManager,
                                       SceneReferencesProvider references,
                                       LoadingScreenProvider loadingScreen)
                                       : base(assetManager, references) {
            _loadingScreen = loadingScreen;
        }

        protected override async UniTask LoadScene(AssetReference reference) {
            try {
                await _loadingScreen.ShowLoadingAsync();

                await AssetManager.LoadSceneAsync(
                    reference,
                    LoadSceneMode.Single,
                    onProgress: progress => UpdateProgress(reference, progress),
                    LoadCancellation.Token);

                await _loadingScreen.HideLoadingAsync();
            }
            catch (OperationCanceledException) {
                Debug.LogWarning("Scene loading was cancelled");
            }
            catch (Exception e) {
                Debug.LogError($"Failed to load scene: {e.Message}");
            }
        }

        private void UpdateProgress(AssetReference reference, float progress) {
            _loadingScreen.ProgressChanged(progress);
            _loadingScreen.UpdateLoadingText($"Loading {reference.SubObjectName} {(int)(progress * 100)}%");
        }
    }
}
