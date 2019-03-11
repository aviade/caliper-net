
using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Annotation {
	using ImsGlobal.Caliper.Entities.Agent;
	using ImsGlobal.Caliper.Entities.SchemaDotOrg;


	/// <summary>
	/// Base type for all annotation types. Direct sub-types, such as
	/// Highlight, Attachment, etc, are specified in the Caliper
	/// Annotation Metric Profile.
	/// </summary>
	public class Annotation : Entity, IThing {
		public Annotation(string id, ICaliperContext caliperContext = null)
			: base(id, caliperContext) {
			this.Type = EntityType.Annotation;
		}

        [JsonProperty("annotator", Order = 20)]
        public Person Annotator { get; set; }

        [JsonProperty( "annotated", Order = 21 )]
		//[JsonConverter( typeof( JsonIdConverter<DigitalResource> ) )]
		public DigitalResource Annotated { get; set; }

	}
}
