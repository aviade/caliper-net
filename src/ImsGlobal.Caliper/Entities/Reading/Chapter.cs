namespace ImsGlobal.Caliper.Entities.Reading {

	public class Chapter : DigitalResource {

		public Chapter (string id, ICaliperContext caliperContext = null)
			: base(id, caliperContext) {
			this.Type = EntityType.Chapter;
		}

	}
}