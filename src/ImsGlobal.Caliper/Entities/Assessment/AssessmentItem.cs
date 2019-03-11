
using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Assessment {
	using ImsGlobal.Caliper.Entities.Assignable;
	using ImsGlobal.Caliper.Entities.Qti;

	public class AssessmentItem : AssignableDigitalResource, IAssessmentItem {

		public AssessmentItem(string id, ICaliperContext caliperContext = null)
			: base(id, caliperContext) {
			this.Type = EntityType.AssessmentItem;
		}

		[JsonProperty( "isTimeDependent", Order = 31 )]
		public bool? IsTimeDependent { get; set; }

	}

}
