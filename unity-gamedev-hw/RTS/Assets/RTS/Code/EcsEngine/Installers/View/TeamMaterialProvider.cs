using System;
using System.Collections.Generic;
using UnityEngine;
using Client.Components.Teams;

namespace Client.Installer {

    [Serializable]
    public sealed class TeamMaterialProvider {
        [SerializeField] private List<TeamMaterial> _teamMaterials;

        public bool TryGetTeamMaterial(TeamTypes type, out Material material) {

            for (int i = 0; i < _teamMaterials.Count; i++) {
                var iVariant = _teamMaterials[i];

                if (iVariant.Type == type) {
                    material = iVariant.Material;
                    return true;
                }
            }

            material = default;
            return true;
        }
    }
}

