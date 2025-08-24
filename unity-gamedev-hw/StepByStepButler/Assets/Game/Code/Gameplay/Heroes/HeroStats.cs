using System;

namespace StepByStepButler.Gameplay.Heroes {

    [Serializable]
    public sealed class HeroStats {
        public int Health;
        public int AttackPower; 

        public HeroStats(int health, int attackPower) {
            Health = health;
            AttackPower = attackPower;
        }
    }
}