using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Pattern_Memento {

    public class ResourceMementoConverter : JsonConverter<ResourceMemento> {
        public override void WriteJson(JsonWriter writer, ResourceMemento value, JsonSerializer serializer) {
            writer.WriteStartObject();

            writer.WritePropertyName("ID");
            writer.WriteValue(value.ID);

            writer.WritePropertyName("Data");
            serializer.Serialize(writer, value.Data);

            writer.WriteEndObject();
        }

        public override ResourceMemento ReadJson(JsonReader reader, Type objectType, ResourceMemento existingValue,
                                            bool hasExistingValue, JsonSerializer serializer) {

            JObject jsonObject = JObject.Load(reader);

            string id = jsonObject["ID"].Value<string>();
            ResourceData data = jsonObject["Data"].ToObject<ResourceData>(serializer);

            return new ResourceMemento(id, data);
        }

        public override bool CanRead => true;
        public override bool CanWrite => true;
    }
}


