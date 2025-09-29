using UnityEngine;
using Client.Components.Common;
using Client.Components.Movement;
using Client.Components.Projectile;
using Client.Components.Visual;
using Leopotam.EcsLite.Entities;
using Client.Components.Teams;

namespace Client.Installer {

    internal sealed class ProjectileInstaller : EntityInstaller {
        [SerializeField] private TeamTypes _team;
        [SerializeField] private float _speed = 15f;
        [SerializeField] private float _damage = 10f;
        [SerializeField] private ParticleSystem _impactEffect;

        protected override void Install(Entity entity) {
            entity.AddData(new Team { Value = _team });
            entity.AddData(new ProjectileInitTag { ViewEntity = entity });
            entity.AddData(new Position { Value = transform.position });
            entity.AddData(new Rotation { Value = transform.rotation });
            entity.AddData(new TransformView { Value = transform });

            if (_impactEffect != null)
                entity.AddData(new ImpactEffect { Value = _impactEffect });

            entity.AddData(new DestroyOneFrame());

            //Debug.Log($"ProjectileInstaller Install");
        }

        protected override void Dispose(Entity entity) {
            Destroy(gameObject);
        }
    }
}
