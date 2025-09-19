using System;
using UnityEngine;

namespace ProgressService {

    [Serializable]
    public sealed class PlayerProgressData {
        [field: SerializeField] public int Chapter { get; set; }
        [field: SerializeField] public int Coins { get; set; }
        [field: SerializeField] public int HealthLevel { get; set; }
        [field: SerializeField] public int DamageLevel { get; set; }
        [field: SerializeField] public int SpeedLevel { get; set; }

        public PlayerProgressData() { }

        public PlayerProgressData(int chapter,
                                  int coins,
                                  int damageLevel,
                                  int healthLevel,
                                  int speedLevel) {

            Chapter = chapter;
            Coins = coins;
            HealthLevel = healthLevel;
            DamageLevel = damageLevel;
            SpeedLevel = speedLevel;
        }
    }
}