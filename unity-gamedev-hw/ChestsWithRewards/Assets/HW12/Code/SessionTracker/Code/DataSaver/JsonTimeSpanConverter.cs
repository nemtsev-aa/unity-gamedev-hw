namespace SessionTrackerSystem {

    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Конвертер для TimeSpan с форматом "hh:mm:ss.fff" (часы:минуты:секунды.миллисекунды)
    /// </summary>
    public class JsonTimeSpanConverter : JsonConverter<TimeSpan> {
        private const string Format = @"hh\:mm\:ss\.fff";

        public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer) {
            writer.WriteValue(value.ToString(Format));
        }

        public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue,
                                        bool hasExistingValue, JsonSerializer serializer) {
            if (reader.Value == null)
                return default;

            if (reader.TokenType == JsonToken.String) {
                string timeString = (string)reader.Value;

                if (TimeSpan.TryParseExact(timeString, Format, null, out TimeSpan result))
                    return result;

                // Попробуем парсить как стандартный TimeSpan, если не подходит наш формат
                if (TimeSpan.TryParse(timeString, out result))
                    return result;

                throw new JsonSerializationException(
                    $"Invalid TimeSpan format. Expected '{Format}' but got '{timeString}'");
            }

            throw new JsonSerializationException(
                $"Unexpected token type {reader.TokenType} when parsing TimeSpan");
        }
    }
}

