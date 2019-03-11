
using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Media {
	using ImsGlobal.Caliper.Entities.SchemaDotOrg;
	using NodaTime;

	public class MediaObject : DigitalResource, IMediaObject {

		public MediaObject(string id, ICaliperContext caliperContext = null)
			: this(id, EntityType.MediaObject, caliperContext) {
		}

		public MediaObject(string id, EntityType type, ICaliperContext caliperContext = null)
			: base(id, caliperContext) {
			this.Type = type;
		}

		[JsonProperty( "duration", Order = 71 )]
		public Period Duration { get; set; }

	}

}
