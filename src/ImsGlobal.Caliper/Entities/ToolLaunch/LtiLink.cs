using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.ToolLaunch
{
    public class LtiLink : Entity
    {
        public LtiLink(string id, ICaliperContext caliperContext = null)
            : base(id, caliperContext)
        {
            this.Type = EntityType.LtiLink;
        }

        [JsonProperty("messageType")]
        public IType MessageType { get; set; }
    }
}