using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ImsGlobal.Caliper.Util {

	internal sealed class CaliperContextJsonConverter : JsonConverter
    {
        public override bool CanRead => false;

        public override bool CanWrite => true;

        public override bool CanConvert(Type objectType)
        {
			return objectType == typeof(ICaliperContextCollectionJsonValue);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
			var propertyValue = (ICaliperContextCollectionJsonValue)value;
            if (propertyValue.Value.Length <= 1)
            {
                writeCaliperContextString(writer, propertyValue);
            }
            else
            {
                writeCaliperContextArray(writer, propertyValue);
            }
        }

        private static void writeCaliperContextString(JsonWriter writer, ICaliperContextCollectionJsonValue propertyValue)
        {
            writer.WriteValue(propertyValue.Value[0].Value);
        }

        private static void writeCaliperContextArray(JsonWriter writer, ICaliperContextCollectionJsonValue propertyValue)
        {
            writer.WriteStartArray();

            foreach (var context in propertyValue.Value)
            {
                try
                {
                    var obj = JsonConvert.DeserializeObject(context.Value);
                    JToken.FromObject(obj).WriteTo(writer);
                }
                catch
                {
                    writer.WriteValue(context.Value);
                }
            }

            writer.WriteEndArray();
        }
    }

}
