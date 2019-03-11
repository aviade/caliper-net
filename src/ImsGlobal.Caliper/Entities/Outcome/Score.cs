using System;
using ImsGlobal.Caliper.Entities.Assignable;
using ImsGlobal.Caliper.Entities.Foaf;
using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Outcome {
	public class Score : Entity {

		public Score(string id, ICaliperContext caliperContext = null) 
			: base(id, caliperContext) {
            this.Type = EntityType.Score;		
		}

		[JsonProperty("attempt", Order = 11)]
		public Attempt Attempt { get; set; }

		[JsonProperty("maxScore", Order = 12)]
		public double MaxScore { get; set; }

		[JsonProperty("scoreGiven", Order = 13)]
		public double ScoreGiven { get; set; } 

		[JsonProperty("comment", Order = 14)]
		public string Comment { get; set; }

		[JsonProperty("scoredBy", Order = 15)]
		public IAgent ScoredBy { get; set; }


	}

	
}
