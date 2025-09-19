using Conveyors.Entity.Core;
using Elementary;
using System;
using UnityEngine;

namespace Conveyors.Entity {

    [Serializable]
    public sealed class ConveyorCore {
        [field: SerializeField] public BoolVariable EnableVariable { get; private set; } = new();
        [field: SerializeField] public IntVariableLimited LoadStorage { get; private set; } = new();
        [field: SerializeField] public IntVariableLimited UnloadStorage { get; private set; } = new();
        [field: SerializeField] public Timer WorkTimer { get; private set; } = new();
        [field: SerializeField] public WorkMechanics WorkMechanics { get; private set; } = new();

        public void Init(ConveyourConfig config) {
            LoadStorage.MaxValue = config.InputCapacity;
            UnloadStorage.MaxValue = config.OutputCapacity;

            WorkTimer.Duration = config.WorkTime;
            WorkMechanics.Construct(
                 isEnable: EnableVariable,
                 loadStorage: LoadStorage,
                 unloadStorage: UnloadStorage,
                 workTimer: WorkTimer
            );

            EnableVariable.OnValueChanged += OnEnableChanged;
        }

        private void OnEnableChanged(bool isEnabled) {
            if (isEnabled == true)
                (WorkMechanics as IEnableListener)?.OnEnable();
            else
                (WorkMechanics as IDisableListener)?.OnDisable();
        }
    }
}