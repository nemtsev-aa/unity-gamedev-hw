using UnityEngine;
using System;

namespace StepByStepButler.Gameplay.Heroes {

    [Serializable]
    public sealed class EffectPair {
        public HeroType HeroType;
        public GameObject EffectPrefab;
    }
}