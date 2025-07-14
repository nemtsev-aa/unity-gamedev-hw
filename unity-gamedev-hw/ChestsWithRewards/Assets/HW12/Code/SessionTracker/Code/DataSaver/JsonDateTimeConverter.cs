using System;
using Newtonsoft.Json;

namespace SessionTrackerSystem {

    /// <summary>
    /// Конвертер для DateTime с фиксированным форматом "yyyy-MM-dd HH:mm:ss"
    /// </summary>
    public class JsonDateTimeConverter : JsonConverter<DateTime> {
        private const string Format = "yyyy-MM-dd HH:mm:ss";

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer) {
            writer.WriteValue(value.ToString(Format));
        }

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer) {
            if (reader.Value == null)
                return default;

            if (reader.TokenType == JsonToken.Date)
                return (DateTime)reader.Value;

            if (reader.TokenType == JsonToken.String) {
                string dateString = (string)reader.Value;

                if (DateTime.TryParseExact(dateString, Format, null, System.Globalization.DateTimeStyles.None, out DateTime result))
                    return result;

                throw new JsonSerializationException($"Invalid datetime format. Expected '{Format}' but got '{dateString}'");
            }

            throw new JsonSerializationException($"Unexpected token type {reader.TokenType} when parsing DateTime");
        }
    }
}

