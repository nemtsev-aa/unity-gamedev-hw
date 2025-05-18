using Atomic.Entities;
using AtomicFramework.BulletSystem;
using System.Collections.Generic;
using UnityEngine;

namespace AtomicFramework.EntityPool {

    public sealed class SceneEntityPool : IEntityPool {
        private readonly SceneEntity _prefab;
        private readonly BulletConfig _config;

        private readonly Transform _worldContainer;
        private readonly Transform _poolContainer;

        private readonly Queue<SceneEntity> _queue = new();

        public SceneEntityPool(SceneEntity prefab,
                               Transform poolContainer,
                               Transform worldContainer,
                               int initalCount = 0) {

            _prefab = prefab;
            _poolContainer = poolContainer;
            _worldContainer = worldContainer;

            for (int i = 0; i < initalCount; i++) {
                var entity = CreateEntity(_poolContainer);

                _queue.Enqueue(entity);
            }
        }

        public IEntity Rent() {
            if (_queue.TryDequeue(out SceneEntity entity)) {
                entity.transform.SetParent(_worldContainer);
                return entity;
            }

            return CreateEntity(_worldContainer);
        }

        public void Return(IEntity entity) {
            SceneEntity sceneEntity = SceneEntity.Cast(entity);
            sceneEntity.transform.SetParent(_poolContainer);

            _queue.Enqueue(sceneEntity);
        }

        private SceneEntity CreateEntity(Transform container) {
            SceneEntity entity = Object.Instantiate(_prefab, container);
            entity.gameObject.name = entity.Name = $"({_queue.Count})";

            return entity;
        }
    }
}
