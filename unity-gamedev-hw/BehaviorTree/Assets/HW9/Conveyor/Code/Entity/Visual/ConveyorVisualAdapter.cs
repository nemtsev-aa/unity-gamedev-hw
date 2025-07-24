using Zenject;
using Elementary;
using Conveyors.Entity.Core;

namespace Conveyors.Entity.Visual {

    public sealed class ConveyorVisualAdapter : IEnableListener,
                                                IDisableListener {
        
        private ITimer _workTimer;
        private ConveyorAnimator _conveyor;

        [Inject]
        public void Construct(ITimer workTimer, ConveyorAnimator conveyor) {
            _workTimer = workTimer;
            _conveyor = conveyor;
        }

        void IEnableListener.OnEnable() {
            _workTimer.OnStarted += OnStartWork;
            _workTimer.OnFinished += OnFinishWork;
        }

        void IDisableListener.OnDisable() {
            _workTimer.OnStarted -= OnStartWork;
            _workTimer.OnFinished -= OnFinishWork;
        }

        private void OnStartWork() {
            _conveyor.Play();
        }

        private void OnFinishWork() {
            _conveyor.Stop();
        }
    }

}