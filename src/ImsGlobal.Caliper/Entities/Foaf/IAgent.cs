namespace ImsGlobal.Caliper.Entities.Foaf {
	using ImsGlobal.Caliper.Util;

	/// <summary>
	/// An entity that can do things. Typical kinds of agents include people,
	/// organizations and software systems.
	/// See http://xmlns.com/foaf/spec/#term_Agent
	/// </summary>
	public interface IAgent : IJsonId {

	    EntityType Type { get; set; }

	}

}
