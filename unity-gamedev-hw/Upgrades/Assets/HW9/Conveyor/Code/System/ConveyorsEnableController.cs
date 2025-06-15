using Game.GameEngine.Mechanics;
using Zenject;

namespace Conveyors.System {

    public sealed class ConveyorsEnableController : IGameStartElement,
                                                    IGameFinishElement {
        
        private ConveyorsService _conveyorsService;

        [Inject]
        public void Construct(ConveyorsService conveyorsService) {
            _conveyorsService = conveyorsService;
        }

        public void StartGame() {
            EnableConveyors(true);
        }

        public void FinishGame() {
            EnableConveyors(false);
        }

        private void EnableConveyors(bool isEnable) {
            var conveyors = _conveyorsService.GetAllConveyors();
            
            foreach (var conveyor in conveyors) {
                var enableComponent = conveyor.Get<IComponent_Enable>();
                enableComponent.SetEnable(isEnable);
            }
        }
    }
}