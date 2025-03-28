using ShootEmUp;
using UnityEngine;

public class Character : Unit {
    private CharacterConfig _config;
    private InputManager _input;

    public CharacterAttackAgent AttackAgent { get; private set; }

    public void Init(CharacterConfig config, BulletSystem bulletSystem, InputManager input, LevelBounds levelBounds) {
        _config = config;
        _input = input;
        
        base.Init(_config.HitPointCount, _config.Speed, true);

        AttackAgent = new CharacterAttackAgent(this, bulletSystem, _config.BulletConfig);
        MoveController.SetLevelBounds(levelBounds);

        AddCharacterListeners();
    }

    private void AddCharacterListeners() {
        _input.HorizontalDirectionChanged += OnInput_HorizontalDirectionChanged;
        _input.FireStatusChanged += OnInput_FireStatusChanged;
    }

    private void RemoveCharacterListeners() {
        _input.HorizontalDirectionChanged -= OnInput_HorizontalDirectionChanged;
        _input.FireStatusChanged -= OnInput_FireStatusChanged;
    }

    private void OnInput_HorizontalDirectionChanged(int value) {
        if (value == 0)
            return;

        Vector2 direction = Vector2.right * value * Time.fixedDeltaTime;
        MoveController.MoveByRigidbodyVelocity(direction);
    }

    private void OnInput_FireStatusChanged() =>
        AttackAgent.SetFireRequired(true);


    public override void Dispose() {
        base.Dispose();
        RemoveCharacterListeners();
    }
}
