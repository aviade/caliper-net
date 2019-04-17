using ImsGlobal.Caliper.Entities.Agent;
using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Feedback
{
    public class FeedbackComment : Feedback
    {
        public FeedbackComment(string id, ICaliperContext caliperContext = null)
            : base(id, caliperContext)
        {
            this.Type = EntityType.Comment;
        }

        /// <summary>
        /// The Person which gave the comment
        /// </summary>
        [JsonProperty("commenter", Order = 12)]
        public Person Commenter { get; set; }

        /// <summary>
        /// The Entity which received the comment
        /// </summary>
        [JsonProperty("commentedOn", Order = 13)]
        public Entity CommentedOn { get; set; }

        /// <summary>
        /// A string value representing the comment's textual value
        /// </summary>
        [JsonProperty("value", Order = 14)]
        public string Value { get; set; }
    }
}