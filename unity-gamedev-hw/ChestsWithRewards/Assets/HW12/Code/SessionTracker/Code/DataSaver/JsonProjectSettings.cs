using Newtonsoft.Json;
using System.Collections.Generic;

namespace SessionTrackerSystem {

    public static class JsonProjectSettings {
        public static void ApplyProjectSerializationSettings() {
            var settings = new JsonSerializerSettings {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,

                Converters = new List<JsonConverter> {
                    new JsonDateTimeConverter(),
                    new JsonDateTimeConverter()
                }
            };

            JsonConvert.DefaultSettings = () => settings;
        }
    }
}

