using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace SaveLoadSystem {
    public class JsonVector3Converter : JsonConverter<Vector3> {
        
        public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer) {
            writer.WriteStartObject();
            writer.WritePropertyName("x");
            writer.WriteValue(value.x);
            writer.WritePropertyName("y");
            writer.WriteValue(value.y);
            writer.WritePropertyName("z");
            writer.WriteValue(value.z);
            writer.WriteEndObject();
        }

        public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer) {
            
            var obj = JObject.Load(reader);

            try {
                
                float x = (float)obj["x"];
                float y = (float)obj["y"];
                float z = (float)obj["z"];

                return new Vector3(x, y, z);
            }
            catch (Exception ex) {
                Debug.LogWarning($"Failed to recognize Vector3: {obj}. The null vector is returned. Exception: {ex.Message}");
                return Vector3.zero;
            }
        }
    }
}