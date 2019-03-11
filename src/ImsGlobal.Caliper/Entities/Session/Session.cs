
using Newtonsoft.Json;
using NodaTime;

namespace ImsGlobal.Caliper.Entities.Session {
	using ImsGlobal.Caliper.Entities.Agent;

	public class Session : Entity {

		public Session(string id, ICaliperContext caliperContext = null)
			: base(id, caliperContext) {
			this.Type = EntityType.Session;
		}

		[JsonProperty( "user", Order = 11 )]
		public Person User { get; set; }

		[JsonProperty( "startedAtTime", Order = 12 )]
		public Instant? StartedAt { get; set; }

		[JsonProperty( "endedAtTime", Order = 13 )]
		public Instant? EndedAt { get; set; }

		[JsonProperty( "duration", Order = 14 )]
		public Period Duration { get; set; }

	}

}
