using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities
{
	using ImsGlobal.Caliper.Util;

	[JsonConverter(typeof(JsonValueConverter<ProfileType>))]
	public class ProfileType : IType, IJsonValue
	{
        public static readonly ProfileType Annotation = new ProfileType("AnnotationProfile");
        public static readonly ProfileType Assessment = new ProfileType("AssessmentProfile");
        public static readonly ProfileType Assignable = new ProfileType("AssignableProfile");
        public static readonly ProfileType Forum = new ProfileType("ForumProfile");
        public static readonly ProfileType General = new ProfileType("GeneralProfile");
        public static readonly ProfileType Grading = new ProfileType("GradingProfile");
        public static readonly ProfileType Media = new ProfileType("MediaProfile");
        public static readonly ProfileType Reading = new ProfileType("ReadingProfile");
        public static readonly ProfileType ToolLaunch = new ProfileType("ToolLaunchProfile");
        public static readonly ProfileType ToolUse = new ProfileType("ToolUseProfile");
        public static readonly ProfileType Session = new ProfileType("SessionProfile");

        public ProfileType() { }

		public ProfileType(string value)
		{
			this.Value = value;
		}

		public string Value { get; set; }

	}

}