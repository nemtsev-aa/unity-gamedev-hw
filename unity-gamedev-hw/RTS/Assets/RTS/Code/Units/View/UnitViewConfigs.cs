using Client.Components.Teams;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Units.View {

    [Serializable]
    public sealed class UnitViewConfigs {
        [field: SerializeField] public List<UnitViewConfig> Configs { get; private set; }

        public List<UnitViewConfig> GetConfigListByTeamType(TeamTypes type) {

            var configByTeamType = new List<UnitViewConfig>();

            for (int i = 0; i < Configs.Count; i++) {
                var iConfig = Configs[i];

                if (iConfig.Team == type)
                    configByTeamType.Add(iConfig);
            }

            return configByTeamType;
        }
    }
}