using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace AssetManagementSystem {

    public class AssetManager : IDisposable {
        private readonly Dictionary<string, Queue<GameObject>> _objectPools = new();
        private readonly Dictionary<string, AsyncOperationHandle> _loadedAssets = new();
        private readonly Dictionary<string, int> _assetReferenceCounts = new();

        private CancellationTokenSource _cts = new();

        public async UniTask<T> LoadAssetAsync<T>(AssetReference assetRef, CancellationToken externalToken = default) where T : UnityEngine.Object {
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, externalToken);
            var token = linkedCts.Token;

            var key = assetRef.AssetGUID;

            if (_loadedAssets.ContainsKey(key) == true) {
                _assetReferenceCounts[key]++;
                return _loadedAssets[key].Result as T;
            }

            try {
                var handle = Addressables.LoadAssetAsync<T>(assetRef);
                await handle.ToUniTask(cancellationToken: token);

                if (handle.Status == AsyncOperationStatus.Succeeded) {
                    _loadedAssets[key] = handle;
                    _assetReferenceCounts[key] = 1;
                    return handle.Result;
                }

                throw new InvalidOperationException($"Failed to load asset: {assetRef.AssetGUID}");
            }
            catch (OperationCanceledException e) {
                
                throw new ArgumentException($"Asset loading cancelled: {assetRef.AssetGUID} {e.Message}");
            }
        }

        public async UniTask LoadSceneAsync(AssetReference sceneRef,
                                            LoadSceneMode loadMode = LoadSceneMode.Single,
                                            Action<float> onProgress = null,
                                            CancellationToken externalToken = default) {

            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, externalToken);
            var token = linkedCts.Token;

            var key = sceneRef.AssetGUID;

            try {
                var handle = Addressables.LoadSceneAsync(sceneRef, loadMode, activateOnLoad: true);

                if (onProgress != null) {
                    handle.Completed += _ => onProgress(1f);
                    
                    while (handle.IsDone == false && token.IsCancellationRequested == false) {
                        onProgress(handle.PercentComplete);
                        await UniTask.Yield();
                    }
                }

                await handle.ToUniTask(cancellationToken: token);

                if (handle.Status == AsyncOperationStatus.Succeeded) {
                    _loadedAssets[key] = handle;
                    return;
                }

                throw new InvalidOperationException($"Failed to load scene: {sceneRef.AssetGUID}");
            }
            catch (OperationCanceledException) {
                throw new OperationCanceledException($"Scene loading cancelled: {sceneRef.AssetGUID}");
            }
        }

        public async UniTask UnloadSceneAsync(AssetReference sceneRef) {
            var key = sceneRef.AssetGUID;

            if (_loadedAssets.TryGetValue(key, out var handle)) {
                await Addressables.UnloadSceneAsync(handle).ToUniTask();
                _loadedAssets.Remove(key);
            }
        }

        public GameObject GetFromPool(string poolKey) {
            if (_objectPools.TryGetValue(poolKey, out var pool) && pool.Count > 0) {
                var obj = pool.Dequeue();
                obj.SetActive(true);
                return obj;
            }

            return null;
        }

        public void ReturnToPool(string poolKey, GameObject obj) {
            if (!_objectPools.ContainsKey(poolKey)) {
                _objectPools[poolKey] = new Queue<GameObject>();
            }

            obj.SetActive(false);
            _objectPools[poolKey].Enqueue(obj);
        }

        public void ReleaseAsset(AssetReference assetRef) {
            var key = assetRef.AssetGUID;

            if (!_assetReferenceCounts.ContainsKey(key)) return;

            _assetReferenceCounts[key]--;

            if (_assetReferenceCounts[key] <= 0) {
                if (_loadedAssets.TryGetValue(key, out var handle)) {
                    Addressables.Release(handle);
                    _loadedAssets.Remove(key);
                    _assetReferenceCounts.Remove(key);
                }
            }
        }

        public void ClearPool(string poolKey) {
            if (_objectPools.TryGetValue(poolKey, out var pool)) {
                while (pool.Count > 0) {
                    var obj = pool.Dequeue();
                    if (obj != null) {
                        UnityEngine.Object.Destroy(obj);
                    }
                }
                _objectPools.Remove(poolKey);
            }
        }

        public void ClearAllPools() {
            foreach (var poolKey in _objectPools.Keys.ToList()) {
                ClearPool(poolKey);
            }
        }

        public void Dispose() {
            _cts?.Cancel();
            _cts?.Dispose();

            ClearAllPools();

            foreach (var handle in _loadedAssets.Values) {
                Addressables.Release(handle);
            }

            _loadedAssets.Clear();
            _assetReferenceCounts.Clear();
        }
    }
}
