using System.Collections.Generic;

namespace Pattern_Memento {

    public interface IUnitMementos : IMementos {
        public IReadOnlyList<IUnitMemento> Mementos { get; }
    }
}

