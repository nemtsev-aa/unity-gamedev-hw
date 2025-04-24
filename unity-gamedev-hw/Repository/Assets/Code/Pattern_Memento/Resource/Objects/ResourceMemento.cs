using Newtonsoft.Json;

namespace Pattern_Memento {

    public sealed class ResourceMemento : IResourceMemento {

        [JsonConstructor]
        public ResourceMemento(string id, ResourceData data) {
            ID = id;
            Data = data;
        }

        public string ID { get; }
        public ResourceData Data { get; }
    }
}

