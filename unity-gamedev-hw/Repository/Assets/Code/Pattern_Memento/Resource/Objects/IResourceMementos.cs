using System.Collections.Generic;

namespace Pattern_Memento {

    public interface IResourceMementos : IMementos {
        public IReadOnlyList<ResourceMemento> Mementos { get; }
    }
}

