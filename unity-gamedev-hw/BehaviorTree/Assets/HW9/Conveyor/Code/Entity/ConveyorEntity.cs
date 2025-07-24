using Conveyors.Entity.Components;
using Conveyors.Entity.Core;
using Entities;
using Game.GameEngine;
using Game.GameEngine.Mechanics;
using UnityEngine;

namespace Conveyors.Entity {

    [RequireComponent(typeof(ConveyorModel))]
    public sealed class ConveyorEntity : MonoEntityBase {
        [SerializeField] private Transform _unloadPoint;

        private void Awake() {
            CreateComponents();
            InitTriggers();
        }

        private void CreateComponents() {
            var model = GetComponent<ConveyorModel>();
            var core = model.Core;
            var config = model.Config;

            AddRange(
                new Component_Id(config.ID),
                new Component_ObjectType(config.ObjectType),
                new Component_Enable(core.EnableVariable),
                new Component_LoadZone(core.LoadStorage, config.InputResourceType),
                new Component_UnloadZone(core.UnloadStorage, config.OutputResourceType, this._unloadPoint)
            );
        }

        private void InitTriggers() {

            foreach (var trigger in GetComponentsInChildren<ConveyorTrigger>()) {
                trigger.Setup(this);
            }
        }
    }
}