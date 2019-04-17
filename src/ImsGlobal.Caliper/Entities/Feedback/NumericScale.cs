using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Feedback
{
    public class NumericScale : Scale
    {
        public NumericScale(string id, ICaliperContext caliperContext = null)
            : base(id, caliperContext)
        {
            this.Type = EntityType.NumericScale;
        }

        /// <summary>
        /// A decimal value used to determine the minimum value of the NumericScale
        /// </summary>
        [JsonProperty("minValue", Order = 12)]
        public double MinValue { get; set; }

        /// <summary>
        /// The label for the minimum value
        /// </summary>
        [JsonProperty("minLabel", Order = 13)]
        public string MinLabel { get; set; }

        /// <summary>
        /// A decimal value used to determine the maximum value of the NumericScale
        /// </summary>
        [JsonProperty("maxValue", Order = 14)]
        public double MaxValue { get; set; }

        /// <summary>
        /// The label for the maximum value
        /// </summary>
        [JsonProperty("maxLabel", Order = 15)]
        public string MaxLabel { get; set; }

        /// <summary>
        /// Indicates the decimal step used for determining the options between the minimum and maximum values
        /// </summary>
        [JsonProperty("step", Order = 16)]
        public double Step { get; set; }
    }
}