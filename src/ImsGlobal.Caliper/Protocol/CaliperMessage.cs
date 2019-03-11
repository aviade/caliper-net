using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using NodaTime;

namespace ImsGlobal.Caliper.Protocol {

	internal class CaliperMessage<T> {

		internal CaliperMessage(ICaliperContext caliperContext = null) {
			this.DataVersion = (caliperContext == null ? CaliperContext.Context.Value : caliperContext.Value);
		}	

		[JsonProperty( "sensor", Order = 2 )]
		public string SensorId { get; set; }

		[JsonProperty( "sendTime", Order = 3 )]
		public Instant? SendTime { get; set; }

		[JsonProperty("dataVersion", Order = 4)]
		public String DataVersion { get; set; }

		[JsonProperty( "data", Order = 5 )]
		public IEnumerable<T> Data { get; set; }

	}

}
