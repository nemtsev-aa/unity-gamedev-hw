using Game.GameEngine.Mechanics;

namespace Conveyors.Entity.Components {

    public sealed class Component_Id : IComponent_GetId {
        
        public string Id { get; }

        public Component_Id(string id) {
            Id = id;
        }
    }
}