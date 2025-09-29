using Client.Components.Attack;
using Client.Components.Targeting;
using Client.Components.Teams;
using Client.Components.Weapon;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Installer {

    public sealed class RangeWeaponInstaller : EntityInstaller {
        [SerializeField] private LayerMask _enemyLayerMask;
        [SerializeField] private Transform _firePoint;
        [Space(10)]
        [SerializeField] private RangeWeaponConfig _config;
        [SerializeField] private ProjectaleConfig _projectalenConfig;

        protected override void Install(Entity entity) {
            entity.AddData(new AttackerTag {
                SourceID = entity.Id,
                ScaningRange = _config.ScaningRange,
                AttackRange = _config.AttackRange,
                AttackRate = _config.AttackRate,
            });

            var currentTeam = entity.GetData<Team>().Value;

            if (_projectalenConfig.TryGetPrefabByTeamType(currentTeam, out var prefab) == true) {

                entity.AddData(new RangeWeapon {
                    FirePoint = _firePoint,
                    ProjectalePrefab = prefab,
                    ProjectaleSpeed = _projectalenConfig.MoveSpeed,
                    ProjectaleDamage = _projectalenConfig.Damage
                });
            }

            // Добавляем компонент зрения для лучников
            entity.AddData(new VisionComponent {
                VisionRange = _config.ScaningRange * 1.2f, // Видит дальше чем стреляет
                VisionAngle = 120f,
                VisionBlockingLayers = LayerMask.GetMask("Obstacles", "Buildings")
            });
        }

        protected override void Dispose(Entity entity) {
            
        }
    }
}
