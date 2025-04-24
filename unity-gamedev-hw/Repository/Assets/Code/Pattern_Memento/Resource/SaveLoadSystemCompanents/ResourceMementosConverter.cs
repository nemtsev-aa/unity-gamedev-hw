using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Pattern_Memento {

    public class ResourceMementosConverter : JsonConverter<ResourceMementos> {
        public override void WriteJson(JsonWriter writer, ResourceMementos value, JsonSerializer serializer) {

            var temp = new {
                ID = value.ID,
                Mementos = value.Mementos
            };

            serializer.Serialize(writer, temp);
        }

        public override ResourceMementos ReadJson(JsonReader reader, Type objectType, ResourceMementos existingValue, bool hasExistingValue, JsonSerializer serializer) {
            JObject jsonObject = JObject.Load(reader);
            string id = jsonObject["ID"].Value<string>();
            var mementos = jsonObject["Mementos"].ToObject<List<ResourceMemento>>(serializer);

            return new ResourceMementos(id, mementos);
        }
    }
    
}


