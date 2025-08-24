using System;
using System.Collections.Generic;

namespace StepByStepButler.Gameplay.Heroes {

    public sealed class EntityFactory {

        private readonly Dictionary<HeroType, HeroStats> _heroStats = new() {
            { HeroType.Devourer, new HeroStats(50, 3) },
            { HeroType.Huntress, new HeroStats(25, 10) },
            { HeroType.StupidOrc, new HeroStats(32, 8) },
            { HeroType.LordVamp, new HeroStats(22, 6) },
            { HeroType.Paladin, new HeroStats(30, 30) },
            { HeroType.IceMage, new HeroStats(50, 2) },
            { HeroType.Meditator, new HeroStats(25, 4) },
            { HeroType.Electro, new HeroStats(35, 7) }
        };

        private int _nextId = 0;

        public Entity CreateHero(HeroType heroType, PlayerType playerType, int viewIndex) {

            if (_heroStats.TryGetValue(heroType, out var stats) == false)
                throw new ArgumentException($"No stats defined for hero type: {heroType}");

            var entity = new Entity(_nextId++);

            entity.AddComponent(new PlayerComponent(playerType));
            entity.AddComponent(new HeroTypeComponent(heroType));
            entity.AddComponent(new ViewIndexComponent(viewIndex));
            entity.AddComponent(new HealthComponent(stats.Health));
            entity.AddComponent(new AttackComponent(stats.AttackPower)); // Теперь используется AttackPower

            if (heroType == HeroType.Paladin) 
                entity.AddComponent(new DivineShieldComponent(true));

            return entity;
        }

        public void ResetIdCounter() {
            _nextId = 0;
        }
    }
}