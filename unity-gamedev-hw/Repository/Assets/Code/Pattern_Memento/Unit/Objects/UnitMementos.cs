using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pattern_Memento {
    public sealed class UnitMementos : IUnitMementos {
        
        [JsonConstructor]
        public UnitMementos(string id, IReadOnlyList<IUnitMemento> mementos) {
            ID = id;
            Mementos = mementos ?? new List<IUnitMemento>();
        }

        [JsonProperty]
        public string ID { get; }

        [JsonConverter(typeof(UnitMementosConverter))]
        [JsonProperty]
        public IReadOnlyList<IUnitMemento> Mementos { get; }


    }
}

