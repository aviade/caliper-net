using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Annotation {
	using ImsGlobal.Caliper.Util;

	[JsonConverter(typeof(JsonValueConverter<SelectorType>))]
	public sealed class SelectorType : EntityType {

		public static readonly SelectorType Text = new SelectorType("TextPositionSelector");

		public SelectorType() { }

		public SelectorType(string uri) : base(uri)
        {
		}
	}

}