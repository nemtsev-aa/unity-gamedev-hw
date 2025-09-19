using Sirenix.OdinInspector;

namespace Conveyors.System {

    public sealed class ConveyorModule  {
        [ReadOnly, ShowInInspector] private ConveyorsService _conveyorsService = new();

        private ConveyorsEnableController _enableController = new();
    }
}