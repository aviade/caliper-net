
using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Agent {
	using ImsGlobal.Caliper.Entities.Foaf;
	using ImsGlobal.Caliper.Entities.SchemaDotOrg;

	public class SoftwareApplication : Entity, IAgent, ISoftwareApplication {

		public SoftwareApplication(string id, ICaliperContext caliperContext = null)
			: base(id, caliperContext) {
			this.Type = EntityType.SoftwareApplication;
		}

		/// <summary>
		/// The current version of the software
		/// </summary>
		[JsonProperty("version", Order = 60)]
		public string Version { get; set; }

	}

}
