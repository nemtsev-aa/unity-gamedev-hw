using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace AssetManagementSystem {

    public sealed class SimpleSceneLoader : BaseSceneLoader {

        public SimpleSceneLoader(AssetManager assetManager,
                                 SceneReferencesProvider references)
                                : base(assetManager, references) { }

        protected override async UniTask LoadScene(AssetReference reference) {
            try {
                await AssetManager.LoadSceneAsync(
                    reference,
                    LoadSceneMode.Single,
                    externalToken: LoadCancellation.Token);
            }
            catch (OperationCanceledException) {
                Debug.LogWarning("Scene loading was cancelled");
            }
            catch (Exception e) {
                Debug.LogError($"Failed to load scene: {e.Message}");
            }
        }
    }
}
