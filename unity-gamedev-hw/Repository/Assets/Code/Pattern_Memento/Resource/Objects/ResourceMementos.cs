using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pattern_Memento {
    public sealed class ResourceMementos : IResourceMementos {
        [JsonConstructor]
        public ResourceMementos(string iD, IReadOnlyList<ResourceMemento> mementos) {
            ID = iD;
            Mementos = mementos ?? new List<ResourceMemento>();
        }

        [JsonProperty]
        public string ID { get; }

        [JsonConverter(typeof(ResourceMementosConverter))]
        [JsonProperty]
        public IReadOnlyList<ResourceMemento> Mementos { get; }

    }
}

