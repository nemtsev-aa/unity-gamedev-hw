namespace StepByStepButler.Gameplay.Heroes {

    public interface IComponent { }

    public struct HealthComponent : IComponent {
        public int CurrentHealth;
        public int MaxHealth;

        public HealthComponent(int health) {
            CurrentHealth = health;
            MaxHealth = health;
        }
    }

    public struct AttackComponent : IComponent {
        public int AttackPower;

        public AttackComponent(int attack) {
            AttackPower = attack;
        }
    }

    public struct PlayerComponent : IComponent {
        public PlayerType PlayerType;

        public PlayerComponent(PlayerType playerType) {
            PlayerType = playerType;
        }
    }

    public struct HeroTypeComponent : IComponent {
        public HeroType Type;

        public HeroTypeComponent(HeroType type) {
            Type = type;
        }
    }

    public struct FrozenComponent : IComponent {
        public int TurnsRemaining;

        public FrozenComponent(int turns) {
            TurnsRemaining = turns;
        }
    }

    public struct DivineShieldComponent : IComponent {
        public bool IsActive;

        public DivineShieldComponent(bool active) {
            IsActive = active;
        }
    }

    public struct ViewIndexComponent : IComponent {
        public int Index;

        public ViewIndexComponent(int index) {
            Index = index;
        }
    }
}