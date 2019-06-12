using ImsGlobal.Caliper.Util;
using NodaTime;

namespace ImsGlobal.Caliper.Entities {

	/// <summary>
	/// Provides an entity with its JSON-LD type identifier.
	/// </summary>
	public interface IEntity : IJsonId {
        
        string Context { get; set; }

	    EntityType Type { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        object Extensions { get; set; }

        Instant? DateCreated { get; set; }

        Instant? DateModified { get; set; }
    }

}