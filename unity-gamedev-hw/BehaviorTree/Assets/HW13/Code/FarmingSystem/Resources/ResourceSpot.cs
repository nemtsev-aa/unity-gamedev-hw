using System;
using Zenject;
using UnityEngine;
using System.Collections.Generic;

namespace FarmingSystem {

    public sealed class ResourceSpot : MonoBehaviour, IExtractive {
        public event Action<ResourceSpot, bool> CurrentStateChanged;
        public event Action<List<Loot>> LootSpawned;

        public bool IsActive { get; private set; } = true;
        public Resource Resource { get; private set; }

        [SerializeField] private ResourceConfig _config;
        [SerializeField] private int _amount;
        [SerializeField] private Transform _view;

        private float _respawnTimer;
        private ResourceLootSpawner _lootSpawner;

        [Inject]
        public void Construct(ResourceLootSpawner lootSpawner) {
            _lootSpawner = lootSpawner;
        }

        public void Init() {

            if (_config != null && _amount != 0) {
                Resource = new Resource(_config, _amount);
                _respawnTimer = _config.RespawnTime;
                IsActive = true;
            }
        }

        public void Extract() {

            IsActive = false;
            _view.gameObject.SetActive(IsActive);

            if (_lootSpawner.TryStartSpawnLoot(this, out List<Loot> spawnLoot) == true)
                LootSpawned?.Invoke(spawnLoot);

            CurrentStateChanged?.Invoke(this, IsActive);
        }

        public void UpdateRespawnTime(float deltaTime) {

            if (IsActive == true)
                return;

            _respawnTimer -= deltaTime;

            if (_respawnTimer <= 0) {
                IsActive = true;
                _respawnTimer = _config.RespawnTime;
                _view.gameObject.SetActive(IsActive);

                CurrentStateChanged?.Invoke(this, IsActive);
            }
        }
    }
}



