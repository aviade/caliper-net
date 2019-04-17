using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NodaTime;
using NodaTime.Serialization.JsonNet;

namespace ImsGlobal.Caliper.Util {

	internal static class JsonSerializeUtils {

		internal static readonly JsonSerializerSettings serializerSettings;

		static JsonSerializeUtils() {
			serializerSettings = new JsonSerializerSettings();
			serializerSettings.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
			serializerSettings.NullValueHandling = NullValueHandling.Ignore;
			serializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;

		}

		/// <summary>
		/// Convert an object to a JObject via a string roundtrip using default serializer settings
		/// </summary>
		public static JObject toJobject(object obj, bool ignoreDefaultValues = true) {
            serializerSettings.DefaultValueHandling = ignoreDefaultValues ? DefaultValueHandling.Ignore : DefaultValueHandling.Include;

            var str = JsonConvert.SerializeObject(obj, JsonSerializeUtils.serializerSettings);
			return JObject.Parse(str);
		}

		/// <summary>
		/// Remove empty elements and multiple occurrences of the default context. 
		/// </summary>
		public static JObject clean(JObject json) {

			//remove empty elements
			IEnumerable<JToken> tokens = json.SelectTokens("..*").ToList();
			foreach (JToken tok in tokens) {
				if (IsNullOrEmpty(tok)) {
					tok.Parent.Remove();
				}	
			}	
			//remove dupe default contexts
			IEnumerable<JToken> contexts = json.SelectTokens("..@context").ToList();

			int seen = 0;
			foreach (JToken tok in contexts) {
				if (IsCaliperDefaultContext(tok)) {
					if (seen > 0) {
						tok.Parent.Remove();
					}
					seen++;
				}
			}

			return json;
		}

		private static bool IsNullOrEmpty(this JToken token) {
			return (token == null) ||
		   		(token.Type == JTokenType.Array && !token.HasValues) ||
		   		(token.Type == JTokenType.Object && !token.HasValues) ||
		   		(token.Type == JTokenType.String && token.ToString() == String.Empty) ||
				(token.Type == JTokenType.Null);
		}

		private static bool IsCaliperDefaultContext(this JToken token) {
			return token.ToString().Equals(CaliperContext.Context.Value);
		}	


	}
}