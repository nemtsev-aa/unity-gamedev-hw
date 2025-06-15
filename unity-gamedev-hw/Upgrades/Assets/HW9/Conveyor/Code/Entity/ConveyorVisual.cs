using Conveyors.Entity.Core;
using Conveyors.Entity.Visual;
using System;
using UnityEngine;

namespace Conveyors.Entity {

    [Serializable]
    public sealed class ConveyorVisual {
        [SerializeField] private ConveyorAnimator _conveyorAnimator;
        [Space, SerializeField] private ZoneVisual _loadZoneView;
        [SerializeField] private ZoneInfoView _loadZoneInfoView;
        [Space, SerializeField] private ZoneVisual _unloadZoneView;
        [SerializeField] private ZoneInfoView _unloadZoneInfoView;

        private readonly ConveyorVisualAdapter _conveyorViewAdapter = new();
        private readonly ZoneVisualAdapter _loadZoneViewAdapter = new();
        private readonly ZoneInfoAdapter _loadZoneInfoAdapter = new();
        private readonly ZoneVisualAdapter _unloadZoneViewAdapter = new();
        private readonly ZoneInfoAdapter _unloadZoneInfoAdapter = new();

        public void Init(ConveyorCore core) {
            _conveyorViewAdapter.Construct(core.WorkTimer, _conveyorAnimator);
            _loadZoneViewAdapter.Construct(core.LoadStorage, _loadZoneView);
            _loadZoneInfoAdapter.Construct(core.LoadStorage, _loadZoneInfoView);
            _unloadZoneViewAdapter.Construct(core.UnloadStorage, _unloadZoneView);
            _unloadZoneInfoAdapter.Construct(core.UnloadStorage, _unloadZoneInfoView);

            core.EnableVariable.OnValueChanged += OnEnableChanged;
        }

        private void OnEnableChanged(bool isEnabled) {
            if (isEnabled == true) {
                (_conveyorViewAdapter as IEnableListener)?.OnEnable();
                (_loadZoneViewAdapter as IAwakeListener)?.Awake();
                (_loadZoneViewAdapter as IEnableListener)?.OnEnable();
                (_unloadZoneViewAdapter as IAwakeListener)?.Awake();
                (_unloadZoneViewAdapter as IEnableListener)?.OnEnable();
                (_loadZoneInfoAdapter as IEnableListener)?.OnEnable();
                (_unloadZoneInfoAdapter as IEnableListener)?.OnEnable();
            } else {
                (_conveyorViewAdapter as IDisableListener)?.OnDisable();
                (_loadZoneViewAdapter as IDisableListener)?.OnDisable();
                (_unloadZoneViewAdapter as IDisableListener)?.OnDisable();
                (_loadZoneInfoAdapter as IDisableListener)?.OnDisable();
                (_unloadZoneInfoAdapter as IDisableListener)?.OnDisable();
            }
        }
    }
}