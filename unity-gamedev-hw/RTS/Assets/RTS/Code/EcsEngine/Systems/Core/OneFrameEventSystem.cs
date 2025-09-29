using Client.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Client.Components.Common;

namespace Client.Systems {

    internal sealed class OneFrameEventSystem : IEcsRunSystem {
        private readonly EcsFilterInject<Inc<OneFrame>> _filter = EcsWorlds.EVENTS;
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;

        public void Run(IEcsSystems systems) {
            foreach (int @event in _filter.Value) {
                _eventWorld.Value.DelEntity(@event);
            }
        }
    }
}
