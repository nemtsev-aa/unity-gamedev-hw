using Client.Systems;
using Client.Systems.Spatial;
using Client.Components.Health;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Entities;
using Leopotam.EcsLite.ExtendedSystems;

namespace Client.Services {

    public sealed class EcsStartup : SingletonMonoBehaviour<EcsStartup>,
                                     IGameStartListener,
                                     IGamePauseListener,
                                     IGameUpdateListener,
                                     IGameFinishListener {

        public EcsWorld DefaultWorld { get; private set; }
        public EcsWorld EventWorld { get; private set; }
        public EcsEntityFactory EntityFactory => _entityFactory;
        public EntityManager EntityManager { get; private set; }

        private EcsWorldsService _worldsService;
        private EcsSystemsService _systemsService;
        private EcsEntityFactory _entityFactory;

        private bool _isPaused = true;

        protected override void Awake() {
            base.Awake();

            InitializeServices();
            ConfigureSystems();
        }

        private void InitializeServices() {
            EntityManager = new EntityManager();

            _worldsService = new EcsWorldsService();
            _entityFactory = new EcsEntityFactory(_worldsService);
            _systemsService = new EcsSystemsService(_worldsService);
        }

        private void ConfigureSystems() {
            DefaultWorld = _worldsService.GetWorld();
            EventWorld = _worldsService.GetWorld(EcsWorlds.EVENTS);

            var spatialTargeting = new SpatialTargetingGridSystem();
            var sphereCastTargeting = new SphereCastTargetingSystem();
            var targetingVisualization = new TargetingVisualizationSystem();

            var mainSystems = _systemsService.CreateSystems(EcsWorlds.DEFAULT, DefaultWorld);
            mainSystems.AddWorld(EventWorld, EcsWorlds.EVENTS);

            mainSystems
                // Spatial
                .Add(spatialTargeting)
                .Add(sphereCastTargeting)
                // Targeting
                .Add(new TargetingSystem())
                //.Add(targetingVisualization)
                // Moving & Attacking
                .Add(new NavMeshMovementSystem())
                .Add(new RotateToTargetSystem())
                .Add(new MovementSystem())
                // Combat
                .Add(new AttackRequestSystem())
                .Add(new AttackTimerSystem())
                .Add(new RangeWeaponSystem())
                .Add(new MeleeAttackSystem())

                .Add(new SpawnEventSystem())
                .Add(new HealthEmptySystem())
                .Add(new DeathRequestSystem())
                .Add(new ProjectileCollisionRequestSystem())
                .Add(new TakeDamageRequestSystem())
                .Add(new ProjectileDestroySystem())
                .Add(new DestroySystem())

                .Add(new ProjectileSpawnSystem())
                .Add(new ProjectileInitSystem())
                .Add(new FXDamageListenerSystem())

                //View:
                .Add(new TransformViewSynchronizerSystem())
                .Add(new AnimatorDeathListenerSystem())
                .Add(new AnimatorTakeDamageListenerSystem())
                .Add(new AnimatorMoveStateListenerSystem())
                .Add(new AnimatorAttackRequestListenerSystem())

                //Editor:
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(EcsWorlds.EVENTS))
#endif
                //Clean Up:
                .Add(new OneFrameEventSystem())
                .DelHere<DeathEvent>();

            // Инжектим общие зависимости во все системы
            _systemsService.InjectToSystems(EntityManager);
            _systemsService.InjectToSystems(spatialTargeting);
            _systemsService.InjectToSystems(sphereCastTargeting);
            //_systemsService.InjectToSystems(targetingVisualization);
        }

        public void OnStartGame() {
            EntityManager.Initialize(_worldsService.GetWorld());
            _systemsService.InitAll();
            _isPaused = false;
        }

        public void OnUpdateGame() {

            if (_isPaused == true)
                return;

            _systemsService.RunAll();
        }

        public void OnPauseGame() => _isPaused = !_isPaused;

        public void OnFinishGame() => OnDestroy();

        protected override void OnDestroy() {
            base.OnDestroy();

            _systemsService?.Dispose();
            EntityManager = null;
            _worldsService?.Dispose();
        }
    }
}