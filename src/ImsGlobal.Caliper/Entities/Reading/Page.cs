namespace ImsGlobal.Caliper.Entities.Reading {

	public class Page : DigitalResource {

		public Page(string id, ICaliperContext caliperContext = null) 
			: base(id, caliperContext) {
			this.Type = EntityType.Page;
		}

	}
}
