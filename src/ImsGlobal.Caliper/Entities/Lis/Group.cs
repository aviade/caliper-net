
using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Lis {
	using System.Collections.Generic;
	using ImsGlobal.Caliper.Entities.Agent;
	using ImsGlobal.Caliper.Entities.W3c;

	/// <summary>
	/// A Caliper LIS Group represents a Course substructure that a Person
	/// is able to join as a member.
	/// </summary>
	public class Group : Entity, IOrganization {

		public Group(string id, ICaliperContext caliperContext = null)
			: base(id, caliperContext) {
			this.Type = EntityType.Group;
		}

		[JsonProperty( "subOrganizationOf", Order = 22 )]
		public IOrganization SubOrganizationOf { get; set; }

		[JsonProperty("members", Order = 23)]
		public IList<Entity> Members { get; set; }

		}

}
