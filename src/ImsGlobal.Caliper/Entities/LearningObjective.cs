namespace ImsGlobal.Caliper.Entities {

	/// <summary>
	/// Represents a learning objective.
	/// </summary>
	public class LearningObjective : Entity {

		public LearningObjective(string id, ICaliperContext caliperContext = null)
			: base(id, caliperContext) {
			this.Type = EntityType.LearningObjective;
		}

	}

}
