using ShootEmUp;
using UnityEngine;

public class EntryPoint : MonoBehaviour {

    [SerializeField] private GameCycle _gameCycle;
    [SerializeField] private UIManager _uIManager;
    [Space(10)]
    [SerializeField] private InputManager _input;
    [SerializeField] private LevelBounds _levelBounds;
    [SerializeField] private LevelBackground _levelBackground;
    [Space(10)]
    [SerializeField] private CharacterConfig _characterConfig;
    [SerializeField] private Character _character;
    [SerializeField] private EnemyConfig _enemyConfig;
    [Space(10)]
    [SerializeField] private int _bulletInitialCount = 50;
    [SerializeField] private float _bulletPositionCheckingInterval = 3f;
    [SerializeField] private BulletSystem _bulletSystem;
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private BulletFactory _bulletFactory;
    [Space(10)]
    [SerializeField] private int _enemyPoolSize = 7;
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private float _enemySpawnDelay;

    private GameMediator _gameMediator;
    private EnemyManager _enemyManager;
    private GameCycleInstaller _gameCycleInstaller;

    private void Start() {
        _bulletSpawner.Init(_bulletFactory, _bulletInitialCount);
        _bulletSystem = new BulletSystem(_bulletSpawner, _levelBounds, _bulletPositionCheckingInterval);

        _character.Init(_characterConfig, _bulletSystem, _levelBounds);

        _enemyPool.Init(_enemyPoolSize, _enemyConfig, _character);
        _enemyManager = new EnemyManager(_bulletSystem, _enemyPool, _enemySpawnDelay);

        _uIManager.Init();
        _gameMediator = new GameMediator(_gameCycle, _uIManager, _character, _input);

        _gameCycleInstaller = new GameCycleInstaller(_gameCycle, _levelBackground, _input,
            _bulletSpawner, _bulletSystem, _gameMediator, _character, _enemyManager);
        _gameCycleInstaller.AddGameListeners();
    }
}


