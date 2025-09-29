using System;
using UnityEngine;

namespace Client.Components.Visual {

    [Serializable]
    public struct ImpactEffect {
        public ParticleSystem Value;
    }

    [Serializable]
    public struct SmallDamageEffect {
        public ParticleSystem Value;
    }

    [Serializable]
    public struct HightDamageEffect {
        public ParticleSystem Value;
    }

    [Serializable]
    public struct DestroyEffect {
        public ParticleSystem Value;
    }

    [Serializable]
    public struct UnitDamageEffect {
        public ParticleSystem Value;
    }
    
}