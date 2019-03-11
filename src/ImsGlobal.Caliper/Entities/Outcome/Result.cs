
using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Outcome {
	using ImsGlobal.Caliper.Entities.Assignable;
	using ImsGlobal.Caliper.Entities.Foaf;

	public class Result : Entity {

		public Result(string id, ICaliperContext caliperContext = null)
			: base(id, caliperContext) {
			this.Type = EntityType.Result;
		}

		[JsonProperty("attempt", Order = 11)]
 		public Attempt Attempt { get; set; }

		[JsonProperty( "maxResultScore", Order = 12 )]
		public double MaxResultScore { get; set; } 

		[JsonProperty("resultScore", Order = 13)]
		public double ResultScore { get; set; } 

		[JsonProperty( "comment", Order = 18 )]
		public string Comment { get; set; }

		[JsonProperty( "scoredBy", Order = 19 )]
		public IAgent ScoredBy { get; set; }

	}

}
