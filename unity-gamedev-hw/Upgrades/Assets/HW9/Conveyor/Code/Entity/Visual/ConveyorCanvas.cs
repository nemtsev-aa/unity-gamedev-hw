using System;
using UnityEngine;
using Conveyors.Entity.Core;
using Game.GameEngine.GameResources;

namespace Conveyors.Entity.Visual {

    [Serializable]
    public sealed class ConveyorCanvas {
        [SerializeField] private InfoWidget _infoView;

        private readonly InfoWidgetAdapter _infoViewAdapter = new();

        public void Init(ConveyourConfig config,
                         ResourceInfoCatalog resourceCatalog,
                         ConveyorCore core) {

            _infoViewAdapter.Construct(core.WorkTimer, _infoView);

            var inputType = config.InputResourceType;
            var inputIcon = resourceCatalog.FindResource(inputType).Icon;
            _infoView.SetInputIcon(inputIcon);

            var outputType = config.OutputResourceType;
            var outputIcon = resourceCatalog.FindResource(outputType).Icon;
            _infoView.SetOutputIcon(outputIcon);

            core.EnableVariable.OnValueChanged += OnEnableChanged;
        }

        private void OnEnableChanged(bool isEnabled) {
            if (isEnabled == true) {
                (_infoViewAdapter as IAwakeListener)?.Awake();
                (_infoViewAdapter as IEnableListener)?.OnEnable();
            } else
                (_infoViewAdapter as IDisableListener)?.OnDisable();
        }
    }
}