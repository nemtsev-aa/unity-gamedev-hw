using Newtonsoft.Json;
using System;

namespace Pattern_Memento {

    [Serializable]
    public sealed class ResourceData {
        [JsonConstructor]
        public ResourceData(string iD, int amount) {
            ID = iD;
            Amount = amount;
        }

        public string ID { get; }
        public int Amount { get; }
    }
}

