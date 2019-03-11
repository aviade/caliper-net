namespace ImsGlobal.Caliper.Events.Assessment {

	/// <summary>
	/// Event raised when an actor interacts with an assessment resource.
	/// </summary>
	public class AssessmentEvent : Event {

		public AssessmentEvent(string id,  Action action, ICaliperContext caliperContext = null) 
			: base(id, caliperContext) {
			this.Type = EventType.Assessment;
			this.Action = action;
		}

	}

}
