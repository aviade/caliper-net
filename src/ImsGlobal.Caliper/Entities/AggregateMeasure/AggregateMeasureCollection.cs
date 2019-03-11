using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.AggregateMeasure
{
    public class AggregateMeasureCollection : Entity
    {
        public AggregateMeasureCollection(string id, ICaliperContext caliperContext = null)
            : base(id, caliperContext)
        {
            this.Type = EntityType.AggregateMeasureCollection;
        }

        [JsonProperty("items")]
        public AggregateMeasure[] Items { get; set; }
    }
}