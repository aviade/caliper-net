namespace ImsGlobal.Caliper.Entities {

	public class WebPage : DigitalResource {

		public WebPage(string id, ICaliperContext caliperContext = null)
			: base(id, caliperContext) {
			this.Type = EntityType.WebPage;
		}

	}

}
