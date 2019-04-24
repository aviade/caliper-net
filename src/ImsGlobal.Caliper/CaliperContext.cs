﻿using Newtonsoft.Json;

namespace ImsGlobal.Caliper {
	using ImsGlobal.Caliper.Util;

	[JsonConverter( typeof( JsonValueConverter<CaliperContext> ) )]
	public class CaliperContext : IJsonValue, ICaliperContext
    {
		public static CaliperContext Context = new CaliperContext("http://purl.imsglobal.org/ctx/caliper/v1p2");
        public static CaliperContext FeedbackProfileExtensionV1p1 = new CaliperContext("http://purl.imsglobal.org/ctx/caliper/v1p1/FeedbackProfile-extension");
        public static CaliperContext ResourceManagementProfileExtensionV1p1 = new CaliperContext("http://purl.imsglobal.org/ctx/caliper/v1p1/ResourceManagementProfile-extension");

        public CaliperContext() {}

		public CaliperContext( string value ) {
			this.Value = value;
		}

        public CaliperContext( dynamic value ) {
            this.Value = JsonConvert.SerializeObject(value);
        }

		public string Value { get; set; }
	}

}