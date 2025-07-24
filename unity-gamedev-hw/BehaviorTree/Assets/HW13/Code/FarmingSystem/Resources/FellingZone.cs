using System;
using UnityEngine;
using Sirenix.OdinInspector;
using ObservableCollections;
using System.Collections.Generic;
using R3;

namespace FarmingSystem {

    public sealed class FellingZone : MonoBehaviour, IDisposable {
        public ReadOnlyReactiveProperty<int> ReactiveTreesAmount =>
               Observable
                    .EveryValueChanged(this, x => x._activeTrees.Count)
                    .ToReadOnlyReactiveProperty(_activeTrees.Count);
        public ReadOnlyReactiveProperty<int> ReactiveLootAmount =>
              Observable
                   .EveryValueChanged(this, x => x._loots.Count)
                   .ToReadOnlyReactiveProperty(_loots.Count);

        [ShowInInspector] public int TreesAmount => _activeTrees.Count;
        [ShowInInspector] public int LootAmount => _loots.Count;

        [SerializeField] private List<ResourceSpot> _trees;

        private List<ResourceSpot> _activeTrees = new();
        private List<ResourceSpot> _respawnTrees = new();
        private List<Loot> _loots = new();

        [Button]
        public void InitTrees() {

            if (_trees.Count == 0)
                throw new ArgumentException($"{gameObject.name} is empty!");

            for (int i = 0; i < _trees.Count; i++) {

                ResourceSpot tree = _trees[i];
                tree.Init();
                tree.LootSpawned += OnLootSpawned;
                tree.CurrentStateChanged += OnCurrentStateChanged;

                _activeTrees.Add(tree);
            }
        }

        private void Update() {

            if (_respawnTrees.Count == 0)
                return;

            float deltaTime = Time.deltaTime;

            for (int i = 0; i < _respawnTrees.Count; i++) {
                var tree = _respawnTrees[i];

                tree.UpdateRespawnTime(deltaTime);
            }
        }

        private void OnCurrentStateChanged(ResourceSpot tree, bool status) {

            if (status == true) {

                if (_respawnTrees.Contains(tree) == tree)
                    _respawnTrees.Remove(tree);

                if (_activeTrees.Contains(tree) == false)
                    _activeTrees.Add(tree);

                return;
            }

            if (status == false) {

                if (_activeTrees.Contains(tree) == tree)
                    _activeTrees.Remove(tree);

                if (_respawnTrees.Contains(tree) == false)
                    _respawnTrees.Add(tree);

                return;
            }
        }

        private void OnLootSpawned(List<Loot> list) {

            for (int i = 0; i < list.Count; i++) {
                var iLoot = list[i];

                _loots.Add(iLoot);
                iLoot.Collected += OnLootCollected;
            }
        }

        private void OnLootCollected(Loot loot) {
            loot.Collected -= OnLootCollected;
            _loots.Remove(loot);

            Destroy(loot.gameObject);
        }

        public void Dispose() {

            if (_trees.Count == 0 && _loots.Count == 0)
                throw new ArgumentException($"{gameObject.name} is empty!");

            for (int i = 0; i < _trees.Count; i++) {
                ResourceSpot tree = _trees[i];
                tree.CurrentStateChanged -= OnCurrentStateChanged;
            }

            for (int i = 0; i < _loots.Count; i++) {

                Loot loot = _loots[i];
                loot.Collected -= OnLootCollected;
            }
        }

        #region MonoHelper

        [Button]
        private void TakeDamageToTree(int index) {
            var tree = _trees[index];

            if (tree != null && tree.IsActive)
                _trees[index].TakeDamage();
        }

        [Button]
        private void TakeDamageToAllTree() {

            for (int i = 0; i < _trees.Count; i++) {

                var tree = _trees[i];

                if (tree != null && tree.IsActive)
                    _trees[i].TakeDamage();
            }
        }

        #endregion
    }
}
