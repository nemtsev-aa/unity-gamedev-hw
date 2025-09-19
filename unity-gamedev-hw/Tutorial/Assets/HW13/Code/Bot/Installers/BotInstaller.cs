using System;
using Zenject;
using UnityEngine;
using BehaviorTree.Brain;

namespace BehaviorTree.Bot {

    public sealed class BotInstaller : MonoInstaller {
        [Space, SerializeField] private BotBrainDataConfig _brainDataConfig;
        [SerializeField] private CoreInstaller _core;
        [SerializeField] private UIInstaller _ui;

        public override void InstallBindings() {

            if (ValidateData(_brainDataConfig) == false)
                throw new ArgumentException($"Invalid BotBrainData!");

            var brainData = new BotBrainData(_brainDataConfig);
 
            _core.Install(Container, brainData);
            _ui.Install(Container, brainData);
        }

        private bool ValidateData(BotBrainDataConfig config) {

            if (config.DeliveryTarget == null)
                return false;

            if (config.Root == null)
                return false;

            if (config.Waypoints == null)
                return false;

            if (config.Waypoints.Points.Count == 0)
                return false;

            if (config.FellingZones.Zones.Count == 0)
                return false;

            return true;
        }
    }
}



