using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Feedback
{
    public class MultiSelectScale : Scale
    {
        public MultiSelectScale(string id, ICaliperContext caliperContext = null)
            : base(id, caliperContext)
        {
            this.Type = EntityType.MultiSelectScale;
        }

        /// <summary>
        /// A integer value used to determine the amount of points on the MultiselectScale
        /// </summary>
        [JsonProperty("scalePoints", Order = 12)]
        public int ScalePoints { get; set; }

        /// <summary>
        /// The ordered list of labels for each point on the scale
        /// </summary>
        [JsonProperty("itemLabels", Order = 13)]
        public string[] ItemLabels { get; set; }

        /// <summary>
        /// The ordered list of values for each point on the scale
        /// </summary>
        [JsonProperty("itemValues", Order = 14)]
        public string[] ItemValues { get; set; }

        /// <summary>
        /// Indicates whether the order of the selected items is important
        /// </summary>
        [JsonProperty("isOrderedSelection", Order = 15)]
        public bool? IsOrderedSelection { get; set; }

        /// <summary>
        /// Indicates the minimum number of selections that can be chosen
        /// </summary>
        [JsonProperty("minSelections", Order = 16)]
        public int MinSelections { get; set; }

        /// <summary>
        /// Indicates the maximum number of selections that can be chosen
        /// </summary>
        [JsonProperty("maxSelections", Order = 17)]
        public int MaxSelections { get; set; }
    }
}