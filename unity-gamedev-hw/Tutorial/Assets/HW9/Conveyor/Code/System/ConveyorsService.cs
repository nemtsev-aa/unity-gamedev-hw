using Entities;
using Game.GameEngine.Mechanics;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Conveyors.System {

    [Serializable]
    public sealed class ConveyorsService {
        [ShowInInspector] private IEntity[] _conveyors;

        public IEntity FindConveyor(string id) {

            for (int i = 0, count = _conveyors.Length; i < count; i++) {
                var conveyour = _conveyors[i];
                var conveyourId = conveyour.Get<IComponent_GetId>().Id;
                
                if (conveyourId == id) 
                    return conveyour;
            }

            throw new Exception($"Conveyor with {id} is not found!");
        }

        public IEntity[] GetAllConveyors() {
            return this._conveyors;
        }

        public void SetupConveyours(IEnumerable<IEntity> conveyors) {
            _conveyors = conveyors.ToArray();
        }
    }
}