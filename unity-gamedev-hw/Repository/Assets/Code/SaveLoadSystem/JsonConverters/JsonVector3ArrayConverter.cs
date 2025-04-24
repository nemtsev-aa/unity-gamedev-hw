using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace SaveLoadSystem {

    public class JsonVector3ArrayConverter : JsonConverter<Vector3> {
        public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer) {
            writer.WriteStartArray();
            writer.WriteValue(value.x);
            writer.WriteValue(value.y);
            writer.WriteValue(value.z);
            writer.WriteEndArray();
        }

        public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer) {
            var array = JArray.Load(reader);

            if (array.Count != 3) {
                Debug.LogWarning("Incorrect Vector3 format. An array of 3 elements is expected.");
                return Vector3.zero;
            }

            return new Vector3(
                (float)array[0],
                (float)array[1],
                (float)array[2]
            );
        }
    }
}
