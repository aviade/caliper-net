using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities
{
	using ImsGlobal.Caliper.Util;

	[JsonConverter(typeof(JsonValueConverter<MetricUnitType>))]
	public class MetricUnitType : IType, IJsonValue
	{
		public static readonly MetricUnitType AssessmentsSubmitted = new MetricUnitType("AssessmentsSubmitted");
        public static readonly MetricUnitType AssessmentsPassed = new MetricUnitType("AssessmentsPassed");
        public static readonly MetricUnitType MinutesOnTask = new MetricUnitType("MinutesOnTask");
        public static readonly MetricUnitType SkillsMastered = new MetricUnitType("SkillsMastered");
        public static readonly MetricUnitType StandardsMastered = new MetricUnitType("StandardsMastered");
        public static readonly MetricUnitType UnitsCompleted = new MetricUnitType("UnitsCompleted");
        public static readonly MetricUnitType UnitsPassed = new MetricUnitType("UnitsPassed");
        public static readonly MetricUnitType WordsRead = new MetricUnitType("WordsRead");

        public MetricUnitType() { }

		public MetricUnitType(string value)
		{
			this.Value = value;
		}

		public string Value { get; set; }

	}

}