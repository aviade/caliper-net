using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Annotation {

	public class BookmarkAnnotation : Annotation {

		public BookmarkAnnotation(string id, ICaliperContext caliperContext = null)
			: base(id, caliperContext) {
			this.Type = EntityType.Bookmark;
		}

		[JsonProperty( "bookmarkNotes", Order = 31 )]
		public string BookmarkNotes { get; set; }

	}
}
