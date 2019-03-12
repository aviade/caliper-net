using System.Collections.Generic;

using Newtonsoft.Json;
using NodaTime;

namespace ImsGlobal.Caliper.Entities {
	using ImsGlobal.Caliper.Entities.Agent;
	using ImsGlobal.Caliper.Entities.SchemaDotOrg;

	/// <summary>
	/// Caliper representation of a CreativeWork (https://schema.org/CreativeWork)
	/// </summary>
	public class DigitalResource : Entity, IResource, ICreativeWork {

		public DigitalResource(string id, ICaliperContext caliperContext = null) 
			: base(id, caliperContext) {
            this.Type = EntityType.DigitalResource;
			this.LearningObjectives = new List<LearningObjective>();
			this.Keywords = new List<string>();
            this.Creators = new List<Person>();
		}

        [JsonProperty("storageName", Order = 12)]
        public string StorageName { get; set; }

        /// <summary>
        /// List of learning objectives aligned with this resource.
        /// </summary>
        [JsonProperty( "learningObjectives", Order = 13 )]
		public IList<LearningObjective> LearningObjectives { get; set; }

		/// <summary>
		/// List of keywords that describe this resource.
		/// </summary>
		[JsonProperty( "keywords", Order = 14 )]
		public IList<string> Keywords { get; set; }

        /// <summary>
		/// List of creators that describe this resource.
		/// </summary>
		[JsonProperty("creators", Order = 15)]
        public IList<Person> Creators { get; set; }

        /// <summary>
		/// IANA media type
		/// </summary>
		[JsonProperty("mediaType", Order = 16)]
        public string MediaType { get; set; }

        /// <summary>
        /// A reference to the parent resource, if any.
        /// </summary>
        [JsonProperty( "isPartOf", Order = 62 )]
		public Entity IsPartOf { get; set; }

		/// <summary>
		/// The date the digital resource was published.
		/// </summary>
		[JsonProperty( "datePublished", Order = 63 )]
		public Instant? DatePublished { get; set; }

		/// <summary>
		/// The current version of the digital resource.
		/// </summary>
		[JsonProperty( "version", Order = 64 )]
		public string Version { get; set; }

	}
}
