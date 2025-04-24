using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Pattern_Memento {

    [Serializable]
    public class UnitData {

        [JsonConstructor]
        public UnitData(string id, string type, int hitPoints, Vector3 position, Vector3 rotation) {
            ID = id;
            Type = type;
            HitPoints = hitPoints;
            Position = position;
            Rotation = rotation;
        }

        public string ID { get; }
        public string Type { get; }
        public int HitPoints { get; }
        public Vector3 Position { get; }
        public Vector3 Rotation { get; }
    }
}

