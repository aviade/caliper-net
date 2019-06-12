namespace ImsGlobal.Caliper.Util
{
    using ImsGlobal.Caliper.Entities.Agent;

    internal sealed class JsonPersonFromIdConverter : JsonClassFromIdConverter
    {
        public JsonPersonFromIdConverter() : base(id => new Person(id))
        {
        }
    }
}