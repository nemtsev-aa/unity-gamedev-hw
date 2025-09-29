using System;
using UnityEngine;
using Client.Components.Common;
using Client.Components.Health;
using Client.Components.Movement;
using Client.Components.Teams;
using Client.Components.Visual;
using Leopotam.EcsLite.Entities;
using Client.Components.Targeting;

namespace Client.Installer {

    internal sealed class UnitInstaller : EntityInstaller {
        private TeamTypes _teamType;
        private UnitBaseConfig _config;
        private Action _destroyAction;

        public void Init(TeamTypes teamType, UnitBaseConfig config, Action destroyAction) {
            _teamType = teamType;
            _config = config;

            _destroyAction = destroyAction;
            //Debug.Log($"UnitInstaller: Init {gameObject.name} {_teamType} {_config}");
        }

        protected override void Install(Entity entity) {

            //Debug.Log($"UnitInstaller: Install {gameObject.name} {_teamType} {_config}");
            // Team affiliation
            entity.AddData(new Team { Value = _teamType });
            entity.AddData(new UnitType { Value = _config.Type });

            // Health and visuals
            entity.AddData(new Health { Value = _config.Health, MaxValue = _config.Health });
            entity.AddData(new DamageableTag());

            // Basic components
            entity.AddData(new Rotation { Value = transform.rotation });
            entity.AddData(new RotationSpeed { Value = _config.RotationSpeed });

            entity.AddData(new Position { Value = transform.position });
            entity.AddData(new MoveDirection { Value = transform.forward });
            entity.AddData(new MoveSpeed { Value = _config.MoveSpeed });

            entity.AddData(new ModelRadius { Value = _config.ModelRadius });
            entity.AddData(new TransformView { Value = transform });

            // Добавляем компоненты для таргетирования
            entity.AddData(new TargetingComponent {
                CurrentTargetId = -1,
                LastKnownTargetPosition = Vector3.zero,
                LastTargetUpdateTime = 0f,
                Priority = TargetPriority.Medium
            });

            entity.AddData(new TargetableComponent {
                IsActive = true,
                ThreatLevel = _config.Type == UnitTypes.Archer ? 0.8f : 0.6f,
                Type = TargetType.Unit
            });
        }

        protected override void Dispose(Entity entity) {
            // Помечаем юнит как нетаргетируемый
            if (entity.TryGetData(out TargetableComponent targetable)) {
                targetable.IsActive = false;
                entity.SetData(targetable);
            }

            Debug.Log($"UnitInstaller: Dispose {entity.Id} {_teamType} {_config.Type}");

            _destroyAction?.Invoke();
        }
    }
}

