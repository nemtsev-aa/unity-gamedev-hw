using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Pattern_Memento {
    public class UnitMementosConverter : JsonConverter<UnitMementos> {
        public override void WriteJson(JsonWriter writer, UnitMementos value, JsonSerializer serializer) {

            var temp = new {
                ID = value.ID,
                Mementos = value.Mementos
            };

            serializer.Serialize(writer, temp);
        }

        public override UnitMementos ReadJson(JsonReader reader, Type objectType, UnitMementos existingValue, bool hasExistingValue, JsonSerializer serializer) {
            JObject jsonObject = JObject.Load(reader);
            string id = jsonObject["ID"].Value<string>();
            var mementos = jsonObject["Mementos"].ToObject<List<UnitMemento>>(serializer);

            return new UnitMementos(id, mementos);
        }
    }

}


