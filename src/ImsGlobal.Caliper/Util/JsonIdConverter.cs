using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ImsGlobal.Caliper.Util {
    internal sealed class JsonIdConverter<T> : JsonConverter
		where T : IJsonId {

		public override bool CanRead {
			get { return false; }
		}

		public override bool CanWrite {
			get { return true; }
		}

		public override bool CanConvert( Type objectType ) {
			return objectType == typeof( T );
		}

		public override object ReadJson(
			JsonReader reader,
			Type objectType,
			object existingValue,
			JsonSerializer serializer ) {

			throw new NotImplementedException();
		}

		public override void WriteJson(
			JsonWriter writer,
			object value,
			JsonSerializer serializer ) {

			var obj = (T)value;
			JToken.FromObject( obj.Id ).WriteTo( writer );
		}

	}
}
