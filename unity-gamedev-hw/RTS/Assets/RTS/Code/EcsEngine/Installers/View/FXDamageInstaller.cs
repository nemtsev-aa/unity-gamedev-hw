using Client.Components.Visual;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Installer {

    public sealed class FXDamageInstaller : EntityInstaller {
        [SerializeField] private ParticleSystem _smallDamage;
        [SerializeField] private ParticleSystem _hightDamage;
        [SerializeField] private ParticleSystem _destroy;

        private Entity _entity;

        protected override void Install(Entity entity) {
            _entity = entity;

            _smallDamage.gameObject.SetActive(false);
            _hightDamage.gameObject.SetActive(false);
            _destroy.gameObject.SetActive(false);

            _entity.AddData(new SmallDamageEffect { Value = _smallDamage });
            _entity.AddData(new HightDamageEffect { Value = _hightDamage });
            _entity.AddData(new DestroyEffect { Value = _destroy });
        }

        protected override void Dispose(Entity entity) {

        }
    }
}
