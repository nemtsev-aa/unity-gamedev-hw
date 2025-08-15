using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.AddressableAssets;

namespace AssetManagementSystem {

    public abstract class BaseSceneLoader : ISceneLoader {
        protected AssetManager AssetManager { get; }
        protected SceneReferencesProvider References { get; }
        protected CancellationTokenSource LoadCancellation { get; private set; }

        protected BaseSceneLoader(AssetManager assetManager, SceneReferencesProvider references) {
            AssetManager = assetManager;
            References = references;
        }

        public void LoadGame() => LoadSceneInternal(References.GameSceneRef);
        public void LoadMenu() => LoadSceneInternal(References.MenuSceneRef);
        public void UnloadGame() => AssetManager.UnloadSceneAsync(References.GameSceneRef).Forget();

        public void CancelLoading() {
            LoadCancellation?.Cancel();
            LoadCancellation?.Dispose();
            LoadCancellation = null;
        }

        private void LoadSceneInternal(AssetReference reference) {
            LoadCancellation = new CancellationTokenSource();
            LoadScene(reference).Forget();
        }

        protected abstract UniTask LoadScene(AssetReference reference);
    }
}
