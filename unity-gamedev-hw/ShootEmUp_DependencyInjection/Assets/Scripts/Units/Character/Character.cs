using ShootEmUp;
using UnityEngine;
using Zenject;

public class Character : Unit, IGameStartListener, IGameFinishListener {
    private CharacterConfig _config;
    private Vector2 _defaultPosition;
    public CharacterAttackAgent AttackAgent { get; private set; }

    [Inject]
    public void Construct(CharacterConfig config, BulletSystem bulletSystem) {
        _config = config;
        
        base.Init(_config.HitPointCount, _config.Speed, true);

        AttackAgent = new CharacterAttackAgent(this, bulletSystem, _config.BulletConfig);
       
    }

    public void OnStartGame() {
        _defaultPosition = transform.position;

        if (HitPointCounter != null)
            return;

        HitPointCounter = new HitPointCounter(_config.HitPointCount);
        HitPointCounter.HitPointsEmpty += OnHitPointsEmpty;
    }

    public void OnFinishGame() {
        transform.position = _defaultPosition;

        HitPointCounter.HitPointsEmpty -= OnHitPointsEmpty;
        HitPointCounter = null;
    }

    public void SetHorizontalDirection(int value) {
        if (value == 0)
            return;

        Vector2 direction = Vector2.right * value * Time.fixedDeltaTime;
        MoveController.MoveByRigidbodyVelocity(direction);
    }

    public void SetFireStatus() =>
        AttackAgent.SetFireRequired(true);

    public override void Dispose() {
        base.Dispose();
    }
}
