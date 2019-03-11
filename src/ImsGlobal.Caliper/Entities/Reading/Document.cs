namespace ImsGlobal.Caliper.Entities.Reading {

	public class Document : DigitalResource {

		public Document (string id, ICaliperContext caliperContext = null)
			: base(id, caliperContext) {
			this.Type = EntityType.Document;
		}

	}
}