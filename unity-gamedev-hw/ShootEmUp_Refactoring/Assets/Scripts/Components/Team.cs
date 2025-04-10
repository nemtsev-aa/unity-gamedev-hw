namespace ShootEmUp {
    public sealed class Team {
        private readonly bool _isPlayer;

        public Team(bool isPlayer) {
            _isPlayer = isPlayer;
        }

        public bool IsPlayer => _isPlayer;
    }
}