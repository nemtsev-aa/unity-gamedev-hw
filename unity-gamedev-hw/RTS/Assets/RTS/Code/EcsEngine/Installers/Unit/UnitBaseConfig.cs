using System;
using UnityEngine;
using Client.Components.Common;

namespace Client.Installer {

    [Serializable]
    public class UnitBaseConfig {
        [field: SerializeField] public UnitTypes Type { get; private set; }
        [field: SerializeField] public int Health { get; private set; } = 100;
        [field: SerializeField] public float MoveSpeed { get; private set; } = 5.0f;
        [field: SerializeField] public float RotationSpeed { get; private set; } = 90f;
        [field: SerializeField] public float ModelRadius { get; private set; } = 0.5f;
        [field: SerializeField] public float Acceleration { get; private set; } = 8f;
        [field: SerializeField] public float StoppingDistance { get; private set; } = 0.7f;
        [field: SerializeField] public bool AutoBraking { get; private set; } = true;
    }
}

