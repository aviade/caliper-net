using System;

namespace ImsGlobal.Caliper {

	/// <summary>
	/// Options for registering a Caliper endpoint with the Caliper sensor.
	/// </summary>
	public class CaliperEndpointOptions {

		private const int DEFAULT_TIMEOUT = 10000;

		public CaliperEndpointOptions( Uri host, string apiKey = null, int timeout = DEFAULT_TIMEOUT ) {
			if( host == null ) {
				throw new ArgumentNullException( "host" );
			}
			if( timeout < 1000 ) {
				throw new ArgumentOutOfRangeException( "timeout", "timeout must be at least 1000 milliseconds." );
			}

			this.Host = host;
			this.ApiKey = apiKey;
			this.Timeout = timeout;
		}

		/// <summary>
		/// The Caliper web API endpoint.
		/// </summary>
		public Uri Host { get; set; }

		/// <summary>
		/// The Caliper web API key.
		/// </summary>
		public string ApiKey { get; set; }

		/// <summary>
		/// Connection timeout in milliseconds.
		/// </summary>
		public int Timeout { get; set; }

        /// <summary>
        /// Reference to a Caliper JSON-LD context. If not supplied, the most recent version of the Caliper context will be used.
        /// </summary>
        public ICaliperContext CaliperContext { get; set; }
	}

}
