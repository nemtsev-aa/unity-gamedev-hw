using GameEngine;
using UnityEngine;

namespace Unit_Spawn_System {
    public sealed class UnitSpawnArgs {
        public UnitSpawnArgs(Unit prefab, Vector3 position, Quaternion rotation) {
            Prefab = prefab;
            Position = position;
            Rotation = rotation;
        }

        public Unit Prefab { get; }
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
    }
}