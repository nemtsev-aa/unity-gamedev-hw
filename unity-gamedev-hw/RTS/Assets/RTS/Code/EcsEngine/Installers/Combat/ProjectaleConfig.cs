using Client.Components.Teams;
using Leopotam.EcsLite.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace Client.Installer {

    [CreateAssetMenu(
       fileName = nameof(ProjectaleConfig),
       menuName = "ProjectalenConfigs/" + nameof(ProjectaleConfig)
    )]
    public class ProjectaleConfig : ScriptableObject {
        [field: SerializeField] public List<ProjectalePrefab> Prefabs { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; } = 10f;
        [field: SerializeField] public int Damage { get; private set; } = 10;

        public bool TryGetPrefabByTeamType(TeamTypes team, out Entity entity) {

            for (var i = 0; i < Prefabs.Count; i++) { 
                var iPrefab = Prefabs[i];

                if (iPrefab.Team == team) {
                    entity = iPrefab.Entity;
                    return true;                
                }
            }

            entity = null;
            return false;
        }
    }
}



