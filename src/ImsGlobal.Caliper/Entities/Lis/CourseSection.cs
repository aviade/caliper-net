
using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Lis {

	/// <summary>
	/// A CourseSection is a way to represent a group of people associated
	/// with a course or class. These groups may include everyone in the
	/// class or course, or may be subsets of that whole group.
	/// CourseSections may have sub-sections (these are created as
	/// separate Group objects linked using the relationship). Examples of
	/// a CourseSection are Lecture, Laboratory, Studio, Seminar, etc.
	/// There may be several instances of a type of CourseSection e.g.,
	/// multiple lectures.
	/// 
	/// A Caliper CourseSection provides a subset of the CourseSection
	/// properties specified in the IMS LTI 2.0 specification, which in
	/// turn, draws inspiration from the IMS LIS 1.0 specification.
	/// </summary>
	public class CourseSection : CourseOffering {

		public CourseSection(string id, ICaliperContext caliperContext = null)
			: base(id, caliperContext) {
			this.Type = EntityType.CourseSection;
		}

		[JsonProperty( "category", Order = 20 )]
		public string Category { get; set; }

	}

}
