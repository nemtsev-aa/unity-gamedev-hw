using Leopotam.EcsLite.Helpers;

namespace Client.Services {
    public sealed class EcsEntityFactory {
        private readonly EcsWorldsService _worldsService;

        public EcsEntityFactory(EcsWorldsService worldsService) {
            _worldsService = worldsService;
        }

        public EcsEntityBuilder CreateEntity(string worldName = null) {
            return new EcsEntityBuilder(_worldsService.GetWorld(worldName));
        }
    }
}