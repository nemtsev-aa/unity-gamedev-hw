using ShootEmUp;

public sealed class GameCycleInstaller {
    private GameCycle _gameCycle;
    private InputManager _input;
    private LevelBackground _levelBackground;
    private BulletSpawner _bulletSpawner;
    private BulletSystem _bulletSystem;
    private GameMediator _gameMediator;
    private Character _character;
    private EnemyManager _enemyManager;

    public GameCycleInstaller(GameCycle gameCycle, InputManager input, LevelBackground levelBackground,
                              BulletSpawner bulletSpawner, BulletSystem bulletSystem, GameMediator gameMediator,
                              Character character, EnemyManager enemyManager) {
        
        _gameCycle = gameCycle;
        _input = input;
        _levelBackground = levelBackground;
        _bulletSpawner = bulletSpawner;
        _bulletSystem = bulletSystem;
        _gameMediator = gameMediator;
        _character = character;
        _enemyManager = enemyManager;

        AddGameListeners();
    }

    private void AddGameListeners() {
        _gameCycle.Add(_levelBackground);
        _gameCycle.Add(_input);
        _gameCycle.Add(_bulletSpawner);
        _gameCycle.Add(_bulletSystem);
        _gameCycle.Add(_gameMediator);
        _gameCycle.Add(_character);
        _gameCycle.Add(_enemyManager);
    }
}


