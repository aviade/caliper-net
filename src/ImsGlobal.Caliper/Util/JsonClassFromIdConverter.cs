namespace ImsGlobal.Caliper.Util
{
    using System;
    using ImsGlobal.Caliper.Entities.Agent;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class JsonClassFromIdConverter : JsonConverter
    {
        private readonly Func<string, object> factory;

        public JsonClassFromIdConverter(Func<string, object> factory)
        {
            this.factory = factory;
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Person);
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {

            JToken jToken = JToken.Load(reader);
            var id = jToken.ToString();

            return factory(id);
        }

        public override void WriteJson(
            JsonWriter writer,
            object value,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

    }
}