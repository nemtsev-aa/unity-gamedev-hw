using Client.Components.Common;
using Client.Components.Movement;
using Client.Components.Targeting;
using Client.Components.Teams;
using Code.OOP;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;
using UnityEngine;

namespace Client.Systems {

    public class SphereCastTargetingSystem : IEcsInitSystem, IEcsRunSystem {
        private const float SPHERE_CAST_RADIUS = 2f;
        private const int MAX_TARGETS = 20;

        private readonly EcsFilterInject<
            Inc<Position, Team, TargetableComponent>,
            Exc<Inactive>> _targetsFilter;

        private readonly EcsPoolInject<Position> _positionPool;
        private readonly EcsPoolInject<Team> _teamPool;
        private readonly EcsPoolInject<TargetableComponent> _targetablePool;

        private Collider[] _overlapResults = new Collider[MAX_TARGETS];
        private List<TargetCandidate> _candidatesCache = new List<TargetCandidate>(MAX_TARGETS);
        private EcsWorld _world;

        public void Init(IEcsSystems systems) {
            _world = systems.GetWorld();
        }

        public void Run(IEcsSystems systems) { }

        public List<TargetCandidate> GetPotentialTargetsInRange(Vector3 center,
                                                                float radius,
                                                                Team attackerTeam,
                                                                LayerMask targetLayers,
                                                                bool sortByDistance = true) {
            _candidatesCache.Clear();

            int hitCount = Physics.OverlapSphereNonAlloc(
                center,
                radius,
                _overlapResults,
                targetLayers,
                QueryTriggerInteraction.Ignore
            );

            //Debug.Log($"SphereCastTargetingSystem: HitCount {hitCount}");

            for (int i = 0; i < hitCount; i++) {
                var collider = _overlapResults[i];
                var entityLink = collider.GetComponent<EntityProxy>();

                if (entityLink == null || !entityLink.Entity.IsAlive())
                    continue;

                int entityId = entityLink.Entity.Id;

                if (!IsValidTarget(entityId, attackerTeam))
                    continue;

                var position = _positionPool.Value.Get(entityId).Value;
                float distance = Vector3.Distance(center, position);

                _candidatesCache.Add(new TargetCandidate {
                    EntityId = entityId,
                    Position = position,
                    Distance = distance,
                    Collider = collider
                });
            }

            if (sortByDistance) {
                _candidatesCache.Sort((a, b) => a.Distance.CompareTo(b.Distance));
            }

            return _candidatesCache;
        }

        private bool IsValidTarget(int entityId, Team attackerTeam) {
            return _world.IsEntityAlive(entityId) &&
                   _teamPool.Value.Has(entityId) &&
                   _teamPool.Value.Get(entityId).Value != attackerTeam.Value &&
                   _targetablePool.Value.Has(entityId) &&
                   _targetablePool.Value.Get(entityId).IsActive;
        }
    }
}

