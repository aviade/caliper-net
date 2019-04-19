using System.Collections.Generic;
using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Collection {

    /// <summary>
    /// A Caliper Collection represents an unordered collection of entities.
    /// </summary>
    public class Collection<TItems> : Entity where TItems: IEntity {
        public Collection(string id, ICaliperContext caliperContext = null)
            : base(id, caliperContext)
        {
            this.Type = EntityType.Collection;
        }

        [JsonProperty("items", Order = 62)]
        public TItems[] Items { get; set; }
    }

}
