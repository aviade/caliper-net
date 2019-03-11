using System.Collections.Generic;

using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Collection {

	/// <summary>
	/// Default base class for Caliper Digital Resource Collection.
	/// </summary>
	public class DigitalResourceCollection : DigitalResource, IDigitalResourceCollection {

		public DigitalResourceCollection(string id, ICaliperContext caliperContext = null) 
			: base(id, caliperContext) {            
			this.Type = EntityType.DigitalResourceCollection;
        }

        [JsonProperty("items", Order = 62)]
        public IList<DigitalResource> Items { get; set; }

    }

}
