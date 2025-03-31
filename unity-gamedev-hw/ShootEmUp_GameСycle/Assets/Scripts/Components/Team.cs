namespace ShootEmUp {
    public sealed class Team {
        private bool _isPlayer;

        public Team(bool isPlayer) {
            _isPlayer = isPlayer;
        }

        public bool IsPlayer => _isPlayer;
    }
}