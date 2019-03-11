using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Response {

	public class TrueFalseResponse : Response {

		public TrueFalseResponse(string id, ICaliperContext caliperContext = null)
			: base(id, caliperContext) {
			this.Type = EntityType.TrueFalse;
		}

		[JsonProperty( "value", Order = 31 )]
		public string Value { get; set; }

	}

}