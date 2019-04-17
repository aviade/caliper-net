using Newtonsoft.Json;
using NodaTime;

namespace ImsGlobal.Caliper.Entities.AggregateMeasure
{
    public class AggregateMeasure : Entity
    {
        public AggregateMeasure(string id, ICaliperContext caliperContext = null)
            : base(id, caliperContext)
        {
            this.Type = EntityType.AggregateMeasure;
        }

        [JsonProperty("metricValue", Order = 5)]
        public double MetricValue { get; set; }

        [JsonProperty("maxMetricValue", Order = 6)]
        public double MaxMetricValue { get; set; }

        [JsonProperty("metric", Order = 7)]
        public MetricUnitType Metric { get; set; }

        [JsonProperty("startedAtTime", Order = 8)]
        public Instant? StartedAtTime { get; set; }

        [JsonProperty("endedAtTime", Order = 9)]
        public Instant? EndedAtTime { get; set; }
    }
}