using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

namespace SceneManagementSystem {
    public sealed class SceneLoader : ISceneLoader {

        private SceneReferencesProvider _references;

        public SceneLoader(SceneReferencesProvider references) {
            _references = references;
        }

        public void LoadGame() => LoadSceneInternal(_references.GameSceneRef);
        public void LoadMenu() => LoadSceneInternal(_references.MenuSceneRef);

        private void LoadSceneInternal(string name) {
            LoadScene(name).Forget();
        }

        private async UniTask LoadScene(string name) {
            try {
                await SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
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

