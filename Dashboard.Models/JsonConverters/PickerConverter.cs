using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Dashboard.Models.JsonConverters
{
    public class PickerConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Picker);
        }

        public override bool CanRead => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                return new Picker
                {
                    Name = serializer.Deserialize(reader, typeof(string)).ToString()
                };
            }
            throw new JsonSerializationException("Could not convert to Package");
        }

        public override bool CanWrite => true;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var picker = value as Picker;
            if (picker != null)
            {
                var name = new JValue(picker.Name);
                name.WriteTo(writer);
            }
        }
    }
}