using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Feedback
{
    public class RatingScaleQuestion : Question
    {
        public RatingScaleQuestion(string id, ICaliperContext caliperContext = null)
            : base(id, caliperContext)
        {
            this.Type = EntityType.RatingScaleQuestion;
        }

        /// <summary>
        /// The Scale used in the question
        /// </summary>
        [JsonProperty("scale", Order = 12)]
        public Scale Scale { get; set; }
    }
}