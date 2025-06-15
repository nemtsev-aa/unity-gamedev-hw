using Elementary;

namespace Conveyors.Entity.Core {

    public sealed class WorkMechanics : IEnableListener,
                                        IDisableListener,
                                        IFixedUpdateListener {

        private IVariable<bool> _isEnable;
        private IVariableLimited<int> _loadStorage;
        private IVariableLimited<int> _unloadStorage;
        private ITimer _workTimer;

        public void Construct(IVariable<bool> isEnable,
                              IVariableLimited<int> loadStorage,
                              IVariableLimited<int> unloadStorage,
                              ITimer workTimer) {

            _isEnable = isEnable;
            _loadStorage = loadStorage;
            _unloadStorage = unloadStorage;
            _workTimer = workTimer;
        }

        void IEnableListener.OnEnable() {
            _workTimer.OnFinished += OnWorkFinished;
        }

        void IDisableListener.OnDisable() {
            _workTimer.OnFinished -= OnWorkFinished;
        }

        void IFixedUpdateListener.FixedUpdate(float deltaTime) {
            if (_isEnable.Current == false)
                return;

            if (CanStartWork() == true)
                StartWork();
        }

        private bool CanStartWork() {

            if (_workTimer.IsPlaying == true)
                return false;


            if (_loadStorage.Current == 0)
                return false;


            if (_unloadStorage.IsLimit == true)
                return false;

            return true;
        }

        private void StartWork() {
            _loadStorage.Current--;
            _workTimer.ResetTime();
            _workTimer.Play();

            //Debug.Log($"StartWork");
        }

        private void OnWorkFinished() {
            _unloadStorage.Current++;

            //Debug.Log($"OnWorkFinished");
        }
    }
}