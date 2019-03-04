using Newtonsoft.Json;
using NodaTime;

namespace ImsGlobal.Caliper.Entities.AggregateMeasure
{
    public class AggregateMeasure : Entity
    {
        public AggregateMeasure(string id)
            : base(id)
        {
            this.Type = EntityType.AggregateMeasure;
        }

        [JsonProperty("metricValue", Order = 5)]
        public double Value { get; set; }

        [JsonProperty("metricValueMax", Order = 6)]
        public double ValueMax { get; set; }

        [JsonProperty("metric", Order = 7)]
        public MetricUnitType Metric { get; set; }

        [JsonProperty("startedAtTime", Order = 8)]
        public Instant? StartedAtTime { get; set; }

        [JsonProperty("endedAtTime", Order = 9)]
        public Instant? EndedAtTime { get; set; }
    }
}