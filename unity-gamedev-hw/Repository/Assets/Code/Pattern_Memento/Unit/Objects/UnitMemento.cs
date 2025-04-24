using Newtonsoft.Json;

namespace Pattern_Memento {

    public class UnitMemento : IUnitMemento {
        [JsonConstructor]
        public UnitMemento(string id, UnitData data) {
            ID = id;
            Data = data;
        }

        public string ID { get; }
        public UnitData Data { get; }
    }
}

