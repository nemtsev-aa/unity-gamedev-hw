using Client.Components;
using Code.OOP;
using UnityEngine;
using Client.Services;
using Leopotam.EcsLite.Entities;
using Client.Components.Common;
using Client.Components.Movement;
using Client.Components.Teams;
using Client.Components.Projectile;

namespace Client.Views {

    [RequireComponent(typeof(EntityProxy))]
    public sealed class ProjectileCollisionComponent : MonoBehaviour {
        private Entity _entity;

        private void Awake() {
            _entity = GetComponent<EntityProxy>().Entity;
        }

        private void OnCollisionEnter(Collision collision) {

            if (collision.gameObject.TryGetComponent(out EntityProxy target) == true) {
                //Debug.Log($"ON COLLISION ENTER {target.Entity.name}", this);

                if (target.Entity.GetData<Team>().Value == _entity.GetData<Team>().Value)
                    return;

                EcsStartup.Instance.EntityFactory.CreateEntity(EcsWorlds.EVENTS)
                    .Add(new CollisionEnterRequest())
                    .Add(new ProjectileTag())
                    .Add(new SourceEntity { Value = _entity.Id })
                    .Add(new TargetEntity { Value = target.Entity.Id })
                    .Add(new Position { Value = collision.GetContact(0).point });
            }
        }
    }
}
