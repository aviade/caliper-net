using System.Collections.Generic;

using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Annotation {

	public class TagAnnotation : Annotation {

		public TagAnnotation(string id, ICaliperContext caliperContext = null)
			: base(id, caliperContext) {
			this.Type = EntityType.Tag;
			this.Tags = new List<string>();
		}

		[JsonProperty( "tags", Order = 31 )]
		public IList<string> Tags { get; set; }

	}
}
