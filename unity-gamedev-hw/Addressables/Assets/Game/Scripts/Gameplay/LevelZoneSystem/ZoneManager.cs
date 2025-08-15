using Cysharp.Threading.Tasks;
using LevelZoneSystem;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SampleLevelZoneSystemGame {

    public sealed class ZoneManager : IDisposable {
        public event Action<int> OnZoneLoaded;
        public event Action<int> OnZoneUnloaded;
        public event Action<float> OnLoadProgress; // Прогресс загрузки

        private ZoneReferenceProvider _provider;
        private List<TransitionToZone> _transitions;
        private int _startZoneIndex;
        private Transform _worldRoot;

        private bool _isDisposed;
        private CancellationTokenSource _cts;

        private Dictionary<int, AssetReference> _zoneReferences = new();
        private Dictionary<int, GameObject> _zoneInstances = new();
        private HashSet<int> _activeZones = new();
        private HashSet<int> _loadedAssets = new();

        public ZoneManager(ZoneReferenceProvider provider,
                         List<TransitionToZone> transitions,
                         int startZoneIndex,
                         Transform worldRoot) {

            _provider = provider;
            _transitions = transitions;
            _startZoneIndex = startZoneIndex;
            _worldRoot = worldRoot;
            _cts = new CancellationTokenSource();

            // Начинаем фоновую предзагрузку
            PreloadZonesWithPriority().Forget();
        }

        public async UniTask Init() {
            if (_isDisposed == true)
                throw new ObjectDisposedException("ZoneManager");

            InitTransitions();
            await LoadStartZone();
        }

        private async UniTask LoadStartZone() {
            try {
                await OnLoadZoneRequested(_startZoneIndex);
            }
            catch (OperationCanceledException) {
                Debug.Log("Zone loading was cancelled");
            }
            catch (Exception e) {
                Debug.LogError($"Failed to load start zone: {e}");
            }
        }

        private async UniTaskVoid PreloadZonesWithPriority() {
            if (_isDisposed) return;

            try {
                // 1. Сначала загружаем стартовую зону
                if (_provider.TryGetConfig(_startZoneIndex, out var startConfig) == true) {
                    await LoadZoneAsset(startConfig);
                }

                // 2. Затем остальные зоны
                foreach (var config in _provider.Configs) {
                    if (_cts.IsCancellationRequested) return;
                    if (config.Index == _startZoneIndex) continue;

                    await LoadZoneAsset(config);
                }
            }
            catch (Exception e) {
                if (!_isDisposed)
                    Debug.LogError($"Preload failed: {e}");
            }
        }

        private async UniTask LoadZoneAsset(ZoneReferenceConfig config) {
            if (_loadedAssets.Contains(config.Index))
                return;

            try {
                var loadHandle = config.Reference.LoadAssetAsync<GameObject>();
                _zoneReferences[config.Index] = config.Reference;

                // Отправляем прогресс загрузки
                while (!loadHandle.IsDone) {
                    OnLoadProgress?.Invoke(loadHandle.PercentComplete);
                    await UniTask.Yield(_cts.Token);
                }

                if (loadHandle.Status == AsyncOperationStatus.Succeeded) {
                    _loadedAssets.Add(config.Index);
                } else {
                    Addressables.Release(loadHandle);
                    throw new Exception($"Failed to load zone {config.Index}");
                }
            }
            catch (OperationCanceledException) {
                // Игнорируем отмену
            }
        }

        private void InitTransitions() {
            
            foreach (var transition in _transitions) {
                
                if (transition == null)
                    continue;

                transition.SetStatus(true);

                transition.LoadZoneRequested += OnLoadZoneRequestedHandler;
                transition.UnloadZoneRequested += OnUnloadZoneRequested;
            }
        }

        private void OnLoadZoneRequestedHandler(int zoneIndex) {
            _ = OnLoadZoneRequested(zoneIndex); // Запуск без ожидания
        }

        private async UniTask OnLoadZoneRequested(int zoneIndex) {
            if (_isDisposed || _activeZones.Contains(zoneIndex))
                return;

            if (!_zoneReferences.TryGetValue(zoneIndex, out var reference)) {
                Debug.LogError($"Zone {zoneIndex} not found in references");
                return;
            }

            await InstantiateZone(reference, zoneIndex);
        }

        private async UniTask InstantiateZone(AssetReference reference, int zoneIndex) {
            try {

                var instance = await Addressables.InstantiateAsync(
                    reference,
                    _worldRoot,
                    true // trackHandle
                );

                _zoneInstances[zoneIndex] = instance;
                _activeZones.Add(zoneIndex);

                OnZoneLoaded?.Invoke(zoneIndex);
            }
            catch (OperationCanceledException) {
                Debug.Log($"Zone {zoneIndex} loading was cancelled");
            }
            catch (Exception e) {
                Debug.LogError($"Failed to instantiate zone {zoneIndex}: {e}");
                throw;
            }
        }

        private void OnUnloadZoneRequested(int zoneIndex) {
            if (_isDisposed || !_activeZones.Contains(zoneIndex))
                return;

            if (_zoneInstances.TryGetValue(zoneIndex, out var instance)) {
                Addressables.ReleaseInstance(instance);
                _zoneInstances.Remove(zoneIndex);
                _activeZones.Remove(zoneIndex);

                OnZoneUnloaded?.Invoke(zoneIndex);
            }

            // Можно добавить выгрузку ассета, если он больше не нужен
            // Addressables.Release(_zoneReferences[zoneIndex]);
        }

        public void Dispose() {
            if (_isDisposed == true)
                return;

            _isDisposed = true;

            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;

            foreach (var transition in _transitions) {

                if (transition == null)
                    continue;

                transition.LoadZoneRequested -= OnLoadZoneRequestedHandler;
                transition.UnloadZoneRequested -= OnUnloadZoneRequested;
                
                transition.Dispose();
            }

            _transitions.Clear();

            foreach (var instance in _zoneInstances.Values) {
                Addressables.ReleaseInstance(instance);
            }
            _zoneInstances.Clear();

            foreach (var reference in _zoneReferences.Values) {
                if (reference.IsValid()) {
                    Addressables.Release(reference);
                }
            }
            _zoneReferences.Clear();

            _activeZones.Clear();
            _loadedAssets.Clear();
        }
    }
}