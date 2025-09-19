using Entities;
using UnityEngine;

namespace Conveyors.Entity.Core {

    [RequireComponent(typeof(Collider))]
    public sealed partial class ConveyorTrigger : MonoBehaviour {
        
        [SerializeField] private ZoneType _zone;
        
        public ZoneType Zone => _zone;
        public IEntity Conveyor { get; private set; }

        public void Setup(IEntity conveyor) {
            Conveyor = conveyor;
        }
    }
}