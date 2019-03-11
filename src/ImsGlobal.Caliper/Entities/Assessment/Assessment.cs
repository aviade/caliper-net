using System.Collections.Generic;

namespace ImsGlobal.Caliper.Entities.Assessment {
	using ImsGlobal.Caliper.Entities.Assignable;
	using ImsGlobal.Caliper.Entities.Collection;
	using ImsGlobal.Caliper.Entities.Qti;
	using Newtonsoft.Json;

	public class Assessment : AssignableDigitalResource, IDigitalResourceCollection, IAssessment {

		public Assessment(string id, ICaliperContext caliperContext = null)
			: base(id, caliperContext) {
			this.Type = EntityType.Assessment;
		}

		[JsonProperty("items", Order = 62)]
		public IList<DigitalResource> Items { get; set; }

	}

}