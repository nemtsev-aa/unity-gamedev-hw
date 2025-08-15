using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using GameCycleSystem;

namespace AssetManagementSystem {

    public class AssetPreloader : IGameInitializeListener {
        public event Action<float> PreloadProgressChanged;
        public event Action PreloadCompleted;

        public bool IsPreloadingComplete { get; private set; }
        public float PreloadProgress { get; private set; }

        private readonly AssetManager _assetManager;
        private readonly List<AssetReference> _criticalAssets;
        private readonly CancellationTokenSource _cts;

        public AssetPreloader(AssetManager assetManager, List<AssetReference> criticalAssets) {
            _assetManager = assetManager;
            _criticalAssets = criticalAssets ?? new List<AssetReference>();
            _cts = new CancellationTokenSource();
        }

        public void OnInitializeGame() {
            PreloadCriticalAssets(_cts.Token).Forget();
        }

        public async UniTask PreloadCriticalAssets(CancellationToken token) {
            
            if (_criticalAssets.Count == 0) {
                IsPreloadingComplete = true;
                PreloadCompleted?.Invoke();
                return;
            }

            try {
                for (int i = 0; i < _criticalAssets.Count; i++) {
                    token.ThrowIfCancellationRequested();

                    await _assetManager.LoadAssetAsync<UnityEngine.Object>(_criticalAssets[i], token);

                    PreloadProgress = (float)(i + 1) / _criticalAssets.Count;
                    PreloadProgressChanged?.Invoke(PreloadProgress);

                    Debug.Log($"Preloaded asset {i + 1}/{_criticalAssets.Count}");
                }

                IsPreloadingComplete = true;
                PreloadCompleted?.Invoke();
                Debug.Log("All critical assets preloaded successfully");
            }
            catch (OperationCanceledException) {
                Debug.Log("Asset preloading cancelled");
            }
            catch (Exception e) {
                Debug.LogError($"Asset preloading failed: {e.Message}");
            }
        }

        public void Dispose() {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}
