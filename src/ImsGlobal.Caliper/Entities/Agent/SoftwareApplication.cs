
using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Agent {
	using ImsGlobal.Caliper.Entities.SchemaDotOrg;

	public class SoftwareApplication : Agent, ISoftwareApplication {

		public SoftwareApplication(string id)
			: base(id) {
			this.Type = EntityType.SoftwareApplication;
		}

		/// <summary>
		/// The current version of the software
		/// </summary>
		[JsonProperty("version", Order = 60)]
		public string Version { get; set; }

	}

}
