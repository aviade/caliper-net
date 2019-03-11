using System.Collections.Generic;

using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Forum {

	/// <summary>
	/// Default base class for Caliper entities.
	/// </summary>
	public class Message : DigitalResource {

        public Message(string id, ICaliperContext caliperContext = null) : base(id, caliperContext)
        {
            this.Id = id;
            this.Type = EntityType.Message;
        }

        [JsonProperty("replyTo", Order = 20)]
        public Message ReplyTo { get; set; }

        [JsonProperty("body", Order = 21)]
        public string Body { get; set; }

        [JsonProperty("attachments", Order = 22)]
        public IList<DigitalResource> Attachments { get; set; }

    }

}
