namespace ImsGlobal.Caliper.Entities.Media {
	using ImsGlobal.Caliper.Entities.SchemaDotOrg;

	/// <summary>
	/// An image object embedded in a web page.
	/// </summary>
	public class ImageObject : MediaObject, IImageObject {

		public ImageObject(string id, ICaliperContext caliperContext = null)
			: base(id, EntityType.ImageObject, caliperContext) {
		}

	}

}
