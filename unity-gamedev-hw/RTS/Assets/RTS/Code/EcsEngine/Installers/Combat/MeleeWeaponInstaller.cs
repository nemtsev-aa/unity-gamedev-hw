using Client.Components.Attack;
using Client.Components.Targeting;
using Client.Components.Weapon;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Installer {
    public sealed class MeleeWeaponInstaller : EntityInstaller {
        [SerializeField] private MeleeWeaponConfig _config;
        [SerializeField] private LayerMask _enemyLayerMask;

        protected override void Install(Entity entity) {

            entity.AddData(new AttackerTag {
                SourceID = entity.Id,
                ScaningRange = _config.ScaningRange,
                AttackRange = _config.AttackRange,
                AttackRate = _config.AttackRate
            });

            entity.AddData(new MeleeWeapon {
                Damage = _config.Damage
            });

            // Добавляем компонент зрения для мечников
            entity.AddData(new VisionComponent {
                VisionRange = _config.ScaningRange * 1.5f, // Видит дальше чем бьет
                VisionAngle = 220f, // Широкий угол обзора
                VisionBlockingLayers = LayerMask.GetMask("Obstacles", "Buildings")
            });
        }

        protected override void Dispose(Entity entity) {

        }
    }
}
