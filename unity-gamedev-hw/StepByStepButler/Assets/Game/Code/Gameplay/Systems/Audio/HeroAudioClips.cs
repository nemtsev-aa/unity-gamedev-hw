using System;
using UnityEngine;
using StepByStepButler.Gameplay.Heroes;

namespace StepByStepButler.Gameplay.Systems.Audio {

    [Serializable]
    public sealed class HeroAudioClips {
        public HeroType heroType;
        public AudioClip[] startTurnClips;
        public AudioClip lowHealthClip;
        public AudioClip abilityClip;
        public AudioClip deathClip;
    }
}