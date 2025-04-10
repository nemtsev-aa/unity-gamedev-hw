using UnityEngine;

namespace ShootEmUp {
    public class EntryPoint : MonoBehaviour {
        [SerializeField] private InputManager _input;
        [SerializeField] private CharacterConfig _characterConfig;
        [SerializeField] private Character _character;
        [Space(10)]
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private LevelBounds _levelBounds;
        [Space(10)]
        [SerializeField] private int _initialCount = 50;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletSpawner _bulletSpawner;
        [SerializeField] private BulletFactory _bulletFactory;
        [Space(10)]
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private float _enemySpawnDelay;

        private EnemyManager _enemyManager;

        private void Start() {
            _bulletSpawner.Init(_bulletFactory, _initialCount);
            _bulletSystem = new BulletSystem(_bulletSpawner, _levelBounds);

            _character.Init(_characterConfig, _bulletSystem, _input, _levelBounds);

            _enemyPool.Init(_character, _enemyConfig);
            _enemyManager = new EnemyManager(_bulletSystem, _enemyPool, _enemySpawnDelay);
            _gameManager.Init(_character, _enemyManager, _bulletSystem);
            _bulletSpawner.CreateBulletPool();

            _gameManager.StartGame();
        }
    }
}
