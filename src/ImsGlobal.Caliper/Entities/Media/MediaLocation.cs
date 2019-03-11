
using Newtonsoft.Json;
using NodaTime;

namespace ImsGlobal.Caliper.Entities.Media {

	public class MediaLocation : DigitalResource {

		public MediaLocation(string id, ICaliperContext caliperContext = null)
			: base(id, caliperContext) {
			this.Type = EntityType.MediaLocation;
		}

		/// <summary>
		/// The time value (from beginning of media) that indicates the
		/// current location.
		/// </summary>
		[JsonProperty( "currentTime", Order = 71 )]
		public Period CurrentTime { get; set; }

	}

}
