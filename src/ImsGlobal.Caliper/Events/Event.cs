using System;

using Newtonsoft.Json;
using NodaTime;

namespace ImsGlobal.Caliper.Events {
	using ImsGlobal.Caliper.Entities;
	using ImsGlobal.Caliper.Entities.Agent;
	using ImsGlobal.Caliper.Entities.Lis;
	using ImsGlobal.Caliper.Util;

    /// <summary>
	/// Default base class for Caliper events.
	/// </summary>
	public class Event {

        public Event( string id ) {
			this.Id = id;
            this.Context = CaliperContext.Context;
            this.Type = EventType.Event;
        }

        /// <summary>
		/// Required - JSON-LD context for the CaliperEvent
		/// </summary>
		[JsonProperty("@context", Order = 1)]
        public CaliperContext Context { get; set; }

		/// <summary>
		/// Required - id of the CaliperEvent
		/// </summary>
		[JsonProperty("id", Order = 2)]
		public String Id { get; set; }

        /// <summary>
        /// Required - Type of the CaliperEvent
        /// </summary>
        [JsonProperty("type", Order = 3)]
        public EventType Type { get; set; }

        /// <summary>
        /// Required - Agent (User, System) that performed the action
        /// </summary>
        [JsonProperty("actor", Order = 4)]
        public Agent Actor { get; set; }

        /// <summary>
        /// Required - Action performed by the agent - from Metric Profile
        /// </summary>
        [JsonProperty("action", Order = 5)]
        public Action Action { get; set; }

        /// <summary>
        /// Required - "Activity Context" - from Metric Profile
        /// </summary>
        [JsonProperty("object", Order = 6)]
        public dynamic Object { get; set; }

        /// <summary>
        /// Optional - "target" - from Metric Profile
        /// </summary>
        [JsonProperty( "target", Order = 7 )]
		public dynamic Target { get; set; }

		/// <summary>
		/// Optional - entity "generated" as result of action - from Metric Profile
		/// </summary>
		[JsonProperty( "generated", Order = 8 )]
		public dynamic Generated { get; set; }

        /// <summary>
		/// Required - time that the event was started at
		/// </summary>
		[JsonProperty("eventTime", Order = 9)]
        public Instant? EventTime { get; set; }

        /// <summary>
        /// EdApp context
        /// </summary>
        [JsonProperty( "edApp", Order = 10 )]
        [JsonConverter(typeof(JsonSoftwareApplicationFromIdConverter))]
		public SoftwareApplication EdApp { get; set; }

		/// <summary>
		/// Group context
		/// </summary>
		[JsonProperty( "group", Order = 11 )]
		public dynamic Group { get; set; }

		/// <summary>
		/// Group context
		/// </summary>
		[JsonProperty( "membership", Order = 12 )]
		public Membership Membership { get; set; }

		/// <summary>
		/// Group context
		/// </summary>
		[JsonProperty( "federatedSession", Order = 13 )]
		//[JsonConverter( typeof( JsonIdConverter<Entities.Session.LtiSession> ) )]
		public Entities.Session.LtiSession FederatedSession { get; set; }

        /// <summary>
		/// Group context
		/// </summary>
		[JsonProperty("session", Order = 14)]
        //[JsonConverter(typeof(JsonIdConverter<Entities.Session.Session>))]
        public Entities.Session.Session Session { get; set; }

        [JsonProperty("extensions", Order = 15)]
        public Object Extensions { get; set; }

        [JsonProperty("referrer", Order = 16)]
        public Entity Referrer { get; set; }
    }
}
