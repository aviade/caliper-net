using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.AggregateMeasure
{
    public class AggregateMeasureCollection : Entity
    {
        public AggregateMeasureCollection(string id)
            : base(id)
        {
            this.Context = CaliperContext.ToolUseProfileExtension.Value;
            this.Type = EntityType.AggregateMeasureCollection;
        }

        [JsonProperty("items")]
        public AggregateMeasure[] Items { get; set; }
    }
}