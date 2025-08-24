namespace StepByStepButler.Gameplay.Heroes {

    public static class EntityExtensions {

        public static bool IsAlive(this Entity entity) {
            return entity.GetComponent<HealthComponent>().CurrentHealth > 0;
        }

        public static bool IsFrozen(this Entity entity) {
            return entity.HasComponent<FrozenComponent>() == true;
        }

        public static void ReduceFreezeTurns(this Entity entity) {
            var frozen = entity.GetComponent<FrozenComponent>();
            frozen.TurnsRemaining--;

            if (frozen.TurnsRemaining <= 0)
                entity.RemoveComponent<FrozenComponent>();
            else
                entity.UpdateComponent(frozen);
        }

        public static bool IsRedTeam(this Entity entity) {
            return entity.GetComponent<PlayerComponent>().PlayerType == PlayerType.Red;
        }

        public static bool IsBlueTeam(this Entity entity) {
            return entity.GetComponent<PlayerComponent>().PlayerType == PlayerType.Blue;
        }

        public static HeroType GetHeroType(this Entity entity) {
            return entity.GetComponent<HeroTypeComponent>().Type;
        }

        public static int GetAttackPower(this Entity entity) {

            if (entity == null)
                return 0;

            return entity.GetComponent<AttackComponent>().AttackPower;
        }

        public static PlayerType GetTeam(this Entity entity) {
            return entity.GetComponent<PlayerComponent>().PlayerType;
        }
    }
}