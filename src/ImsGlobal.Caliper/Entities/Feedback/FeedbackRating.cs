using ImsGlobal.Caliper.Entities.Agent;
using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Feedback
{
    public class FeedbackRating : Feedback
    {
        public FeedbackRating(string id, ICaliperContext caliperContext = null)
            : base(id, caliperContext)
        {
            this.Type = EntityType.Rating;
        }

        /// <summary>
        /// The Person who provided the Rating
        /// </summary>
        [JsonProperty("rater", Order = 12)]
        public Person Rater { get; set; }

        /// <summary>
        /// The Entity which received the rating
        /// </summary>
        [JsonProperty("rated", Order = 13)]
        public Entity Rated { get; set; }

        /// <summary>
        /// The Question used for the Rating
        /// </summary>
        [JsonProperty("question", Order = 14)]
        public Question Question { get; set; }

        /// <summary>
        /// An array of the values representing the rater's selected response
        /// </summary>
        [JsonProperty("selections", Order = 15)]
        public string[] Selections { get; set; }

        /// <summary>
        /// The Comment left with the Rating
        /// </summary>
        [JsonProperty("ratingComment", Order = 16)]
        public FeedbackComment RatingComment { get; set; }
    }
}