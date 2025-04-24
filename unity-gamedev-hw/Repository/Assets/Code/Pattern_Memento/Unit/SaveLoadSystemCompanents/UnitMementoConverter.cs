using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Pattern_Memento {
    public class UnitMementoConverter : JsonConverter<UnitMemento> {
        public override void WriteJson(JsonWriter writer, UnitMemento value, JsonSerializer serializer) {
            writer.WriteStartObject();

            writer.WritePropertyName("ID");
            writer.WriteValue(value.ID);

            writer.WritePropertyName("Data");
            serializer.Serialize(writer, value.Data);

            writer.WriteEndObject();
        }

        public override UnitMemento ReadJson(JsonReader reader, Type objectType, UnitMemento existingValue,
                                            bool hasExistingValue, JsonSerializer serializer) {
            
            JObject jsonObject = JObject.Load(reader);

            string id = jsonObject["ID"].Value<string>();
            UnitData data = jsonObject["Data"].ToObject<UnitData>(serializer);

            return new UnitMemento(id, data);
        }

        public override bool CanRead => true;
        public override bool CanWrite => true;
    }
}


