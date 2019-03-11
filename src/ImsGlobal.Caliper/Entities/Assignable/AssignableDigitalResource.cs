
using Newtonsoft.Json;
using NodaTime;

namespace ImsGlobal.Caliper.Entities.Assignable {

	public class AssignableDigitalResource : DigitalResource, IAssignable {

		public AssignableDigitalResource(string id, ICaliperContext caliperContext = null)
			: base(id, caliperContext) {
			this.Type = EntityType.AssignableDigitalResource;
		}

		[JsonProperty( "dateToActivate", Order = 24 )]
		public Instant? DateToActivate { get; set; }

		[JsonProperty( "dateToShow", Order = 25 )]
		public Instant? DateToShow { get; set; }

		[JsonProperty( "dateToStartOn", Order = 23 )]
		public Instant? DateToStartOn { get; set; }

		[JsonProperty( "dateToSubmit", Order = 26 )]
		public Instant? DateToSubmit { get; set; }

		[JsonProperty( "maxAttempts", Order = 27 )]
		public int MaxAttempts { get; set; }

		[JsonProperty( "maxSubmits", Order = 28 )]
		public int MaxSubmits { get; set; }

		[JsonProperty( "maxScore", Order = 29 )]
		public double MaxScore { get; set; }

	}

}
