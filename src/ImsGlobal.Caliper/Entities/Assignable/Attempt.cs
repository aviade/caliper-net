
using Newtonsoft.Json;
using NodaTime;

namespace ImsGlobal.Caliper.Entities.Assignable {
	using ImsGlobal.Caliper.Entities.Agent;

	/// <summary>
	/// Representation of an Attempt. Attempts are generated as part of or
	/// are the object of an interaction represented by an AssignableEvent.
	/// </summary>
	public class Attempt : Entity {

		public Attempt(string id, ICaliperContext caliperContext = null)
			: base(id, caliperContext) {
			this.Type = EntityType.Attempt;
		}

		[JsonProperty("assignable", Order = 11)]
		public DigitalResource Assignable { get; set; }

		[JsonProperty("assignee", Order = 12)]
		public Person Assignee { get; set; }

		[JsonProperty( "count", Order = 13 )]
		public int Count { get; set; }

		[JsonProperty( "startedAtTime", Order = 14 )]
		public Instant? StartedAtTime { get; set; }

		[JsonProperty( "endedAtTime", Order = 15 )]
		public Instant? EndedAtTime { get; set; }

		[JsonProperty( "duration", Order = 16 )]
		public Period Duration { get; set; }

		[JsonProperty("isPartOf", Order = 17)]
		public Attempt IsPartOf { get; set; }

	}

}
