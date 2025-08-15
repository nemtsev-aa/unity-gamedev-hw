using R3;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cysharp.Threading.Tasks;

namespace CharactersSystem.Spawner {

    public sealed class CharacterFactory : IDisposable {
        public ReadOnlyReactiveProperty<Dictionary<int, AssetReference>> PrefabReferences => _prefabReferencesReactive.ToReadOnlyReactiveProperty();

        private readonly CharacterPrefabReferenceProvider _provider;
        private readonly Dictionary<int, AssetReference> _prefabReferences = new();
        private readonly ReactiveProperty<Dictionary<int, AssetReference>> _prefabReferencesReactive = new();

        private bool _isPreloaded;
        private CancellationTokenSource _cts;

        public CharacterFactory(CharacterPrefabReferenceProvider provider) {
            _provider = provider;
            _cts = new CancellationTokenSource();            
        }

        public async UniTask<Character> Get(int index, Transform parent, CancellationToken externalToken = default) {
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, externalToken);
            var token = linkedCts.Token;

            if (_isPreloaded == false)
                await PreloadCharacterPrefabs(_cts.Token).AttachExternalCancellation(token);

            token.ThrowIfCancellationRequested();

            if (_prefabReferences.TryGetValue(index, out var preloadedPrefab) == false)
                throw new KeyNotFoundException($"Character prefab with index {index} not found");

            try {
                var instance = await Addressables.InstantiateAsync(preloadedPrefab, parent)
                    .ToUniTask(cancellationToken: token);

                if (instance.TryGetComponent<Character>(out var character) == false) {
                    Addressables.ReleaseInstance(instance);

                    throw new MissingComponentException($"Character component not found on prefab!");
                }

                return character;
            }
            catch (OperationCanceledException) {
                Debug.LogWarning($"Character instantiation cancelled for index {index}");
                throw;
            }
            catch (Exception e) {
                Debug.LogError($"Error instantiating Character: {e.Message}");
                throw;
            }
        }

        private async UniTask PreloadCharacterPrefabs(CancellationToken token) {

            try {
                for (int i = 0; i < _provider.References.Count; i++) {
                    token.ThrowIfCancellationRequested();

                    var config = _provider.References[i];

                    if (config.Reference.Asset != null)
                        continue;

                    var loadHandle = config.Reference.LoadAssetAsync<GameObject>();
                    await loadHandle.ToUniTask(cancellationToken: token);

                    if (loadHandle.Status == AsyncOperationStatus.Succeeded) {
                        _prefabReferences[i] = config.Reference;
                    } else {
                        Debug.LogError($"Failed to load CharacterPrefab {i}");
                    }
                }

                _isPreloaded = true;
                _prefabReferencesReactive.Value = new Dictionary<int, AssetReference>(_prefabReferences);
            }
            catch (OperationCanceledException) {
                Debug.LogWarning("Character prefabs preloading cancelled!");
                throw;
            }
            catch (Exception e) {
                Debug.LogError($"Error preloading CharacterPrefabs: {e.Message}");
                throw;
            }
        }

        public void Dispose() {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;

            foreach (var reference in _prefabReferences.Values) {
                Addressables.Release(reference);
            }

            _prefabReferences.Clear();
        }
    }
}