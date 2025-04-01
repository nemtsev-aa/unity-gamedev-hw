using ShootEmUp;

public class Enemy : Unit {
    private EnemyConfig _config;

    public EnemyMoveAgent EnemyMoveAgent { get; private set; }
    public EnemyAttackAgent EnemyAttackAgent { get; private set; }
    public BulletConfig BulletConfig => _config.BulletConfig;

    public void Init(EnemyConfig config) {
        _config = config;

        base.Init(_config.HitPointCount, _config.Speed, false);

        EnemyMoveAgent = new EnemyMoveAgent(MoveController, config.ReachedOffset);
        EnemyAttackAgent = new EnemyAttackAgent(this, config.FireCountdown);
    }

    public void UpdateState() {
        if (_isInit == false)
            return;

        EnemyMoveAgent.Move(transform);
        EnemyAttackAgent.TryFire();
    }
}
