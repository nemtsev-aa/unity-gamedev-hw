using System;
using UnityEngine;

namespace SaveSystem {

    [Serializable]
    public class SaveManagerConfig {
        [field: SerializeField] public SaveType SaveType { get; private set; }
        [field: SerializeField] public string SavePath { get; private set; }
    }
}