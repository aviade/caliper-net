namespace ImsGlobal.Caliper.Util
{
    using ImsGlobal.Caliper.Entities.Agent;

    public class JsonSoftwareApplicationFromIdConverter: JsonClassFromIdConverter
    {
        public JsonSoftwareApplicationFromIdConverter() : base(s => new SoftwareApplication(s))
        {
        }
    }
}