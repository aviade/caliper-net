using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Lis {
	using ImsGlobal.Caliper.Entities.W3c;
	using ImsGlobal.Caliper.Util;

	[JsonConverter( typeof( JsonValueConverter<Status> ) )]
	public sealed class Status : EntityType, IStatus
    {

		public static readonly Status Active = new Status("Active");
		public static readonly Status Inactive = new Status("Inactive");

		public Status() {}

		public Status( string value ): base(value)
        {
		}
	}
}
