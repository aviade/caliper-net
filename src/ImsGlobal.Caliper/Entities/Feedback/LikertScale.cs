using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Feedback
{
    public class LikertScale : Scale
    {
        public LikertScale(string id, ICaliperContext caliperContext = null)
            : base(id, caliperContext)
        {
            this.Type = EntityType.LikertScale;
        }

        /// <summary>
        /// A integer value used to determine the amount of points on the LikertScale
        /// </summary>
        [JsonProperty("scalePoints", Order = 12)]
        public int ScalePoints { get; set; }

        /// <summary>
        /// The ordered list of labels for each point on the Likert scale
        /// </summary>
        [JsonProperty("itemLabels", Order = 13)]
        public string[] ItemLabels { get; set; }

        /// <summary>
        /// The ordered list of values for each point on the Likert scale
        /// </summary>
        [JsonProperty("itemValues", Order = 14)]
        public int[] ItemValues { get; set; }
    }
}