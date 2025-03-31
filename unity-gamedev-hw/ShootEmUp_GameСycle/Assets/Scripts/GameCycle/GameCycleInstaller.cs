using ShootEmUp;

public class GameCycleInstaller {
    private GameCycle _gameCycle;
    private LevelBackground _levelBackground;
    private InputManager _input;
    private BulletSpawner _bulletSpawner;
    private BulletSystem _bulletSystem;
    private GameMediator _gameMediator;
    private Character _character;
    private EnemyManager _enemyManager;

    public GameCycleInstaller(GameCycle gameCycle, LevelBackground levelBackground,
                              InputManager input, BulletSpawner bulletSpawner,
                              BulletSystem bulletSystem, GameMediator gameMediator,
                              Character character, EnemyManager enemyManager) {

        _gameCycle = gameCycle;

        _levelBackground = levelBackground;
        _input = input;
        _bulletSpawner = bulletSpawner;
        _bulletSystem = bulletSystem;
        _gameMediator = gameMediator;
        _character = character;
        _enemyManager = enemyManager;
    }

    public void AddGameListeners() {
        _gameCycle.Add(_levelBackground);
        _gameCycle.Add(_input);
        _gameCycle.Add(_bulletSpawner);
        _gameCycle.Add(_bulletSystem);
        _gameCycle.Add(_gameMediator);
        _gameCycle.Add(_character);
        _gameCycle.Add(_enemyManager);
    }
}


