using System.Collections.Generic;
using ImsGlobal.Caliper.Entities.Collection;
using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Forum {

	/// <summary>
	/// Default base class for Caliper entities.
	/// </summary>
	public class Thread : DigitalResourceCollection {

		public Thread(string id, ICaliperContext caliperContext = null) : base(id, caliperContext) {
			this.Id = id;
			this.Type = EntityType.Thread;
		}

		[JsonProperty("items", Order = 10)]
		public new IList<Message> Items { get; set; }

	}

}
