using System;
using UnityEngine;
using FarmingSystem;

namespace BehaviorTree.Gameplay {

    [Serializable]
    public sealed class UIManager  {
        [SerializeField] private FellingZoneView _zoneView;
        
        public void Install(FellingZone fellingZone) {

            FellingZoneViewModel viewModel = new FellingZoneViewModel(fellingZone);
            _zoneView.Init(viewModel);
        }
    }
}