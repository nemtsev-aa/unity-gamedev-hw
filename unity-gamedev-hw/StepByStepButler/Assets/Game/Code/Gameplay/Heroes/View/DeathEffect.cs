using System;
using UnityEngine;

namespace StepByStepButler.Gameplay.Heroes {

    [Serializable]
    public sealed class DeathEffect {
        public HeroType HeroType;
        public GameObject EffectPrefab;
    }
}