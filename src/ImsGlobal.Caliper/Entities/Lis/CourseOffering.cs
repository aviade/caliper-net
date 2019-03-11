
using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Lis {
	using ImsGlobal.Caliper.Entities.W3c;

	/// <summary>
	/// A CourseOffering is the occurrence of a course in a specific term,
	/// semester, etc. A Caliper CourseOffering provides a subset of the
	/// CourseOffering properties specified in the IMS LTI 2.0 specification,
	/// which in turn, draws inspiration from the IMS LIS 1.0 specification.
	/// </summary>
	public class CourseOffering : Entity, ICourse {

		public CourseOffering(string id, ICaliperContext caliperContext = null)
			: base(id, caliperContext) {
			this.Type = EntityType.CourseOffering;
		}

		[JsonProperty( "courseNumber", Order = 21 )]
		public string CourseNumber { get; set; }

		[JsonProperty( "academicSession", Order = 22 )]
		public string AcademicSession { get; set; }

		[JsonProperty( "subOrganizationOf", Order = 24 )]
		public IOrganization SubOrganizationOf { get; set; }

	}

}
