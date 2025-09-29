using Client.Components;
using Client.Components.Teams;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCycleSystem {
    [Serializable]
    public sealed class UnitSpawnPointPresenter {
        [field: SerializeField] public List<UnitSpawnPoint> Points { get; private set; }
        
        public UnitSpawnPoint GetSpawnPointByTeamType(TeamTypes team) {

            for (int i = 0; i < Points.Count; i++) {
                var point = Points[i];

                if (point.Team == team)
                    return point;
            }

            return null;
        }
    }
}