using Newtonsoft.Json;
using Pattern_Memento;
using System.Collections.Generic;

namespace SaveLoadSystem {

    public static class JsonProjectSettings {
        public static void ApplyProjectSerializationSettings() {
            var settings = new JsonSerializerSettings {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = TypeNameHandling.None,
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new List<JsonConverter> {
                    new JsonVector3ArrayConverter(),
                    new JsonVector3Converter(),
                    new UnitMementoConverter(),
                    new UnitMementosConverter(),
                    new ResourceMementoConverter(),
                    new ResourceMementosConverter()
                }
            };

            JsonConvert.DefaultSettings = () => settings;
        }
    }
}

