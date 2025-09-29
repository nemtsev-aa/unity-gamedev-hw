using Client.Components.Teams;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using Zenject;

namespace Client.Installer {

    internal sealed class UnitViewInstaller : EntityInstaller {
        [SerializeField] private MeshRenderer _headRenderer;
        [SerializeField] private SkinnedMeshRenderer _bodyRenderer;
        
        private TeamMaterialProvider _provider;
        private Material _material;

        [Inject]
        public void Construct(TeamMaterialProvider provider) {
            _provider = provider;
        }

        public void Init(TeamTypes teamType) {

            if (_provider.TryGetTeamMaterial(teamType, out Material material) == false)
                return;

            _material = material;
        }

        protected override void Install(Entity entity) {
            _headRenderer.material = _material;
            _bodyRenderer.material = _material;
        }

        protected override void Dispose(Entity entity) {
            _material = null;
        }
    }
}

