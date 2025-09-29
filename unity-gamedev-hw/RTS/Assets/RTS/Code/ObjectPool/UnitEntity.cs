using System;
using UnityEngine;
using Client.Installer;
using Client.Components.Common;
using Client.Components.Teams;
using Leopotam.EcsLite.Entities;
using Code.Visual;

namespace UnitPoolSystem {

    [Serializable]
    public class UnitEntity : Entity {
        public event Action<UnitEntity> Destroyed;

        [field: SerializeField] public UnitTypes Type { get; private set; }

        public void Init(TeamTypes team, UnitBaseConfig config) {
            var unitInstaller = transform.GetComponent<UnitInstaller>();
            var navMeshAgentInstaller = transform.GetComponent<NavMeshAgentInstaller>();
            var viewInstaller = transform.GetComponent<UnitViewInstaller>();

            unitInstaller.Init(team, config, OnDestroy);
            navMeshAgentInstaller.Init(config);
            viewInstaller.Init(team);
        }

        private void OnDestroy() {
            Destroyed?.Invoke(this);
        }
    }
}