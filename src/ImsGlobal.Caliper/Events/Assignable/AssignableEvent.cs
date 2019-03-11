namespace ImsGlobal.Caliper.Events.Assignable {

	/// <summary>
	/// Event raised when an actor interacts with an assignable resource.
	/// </summary>
	public class AssignableEvent : Event {

		public AssignableEvent(string id, Action action, ICaliperContext caliperContext = null) 
			:base (id, caliperContext) {
			this.Type = EventType.Assignable;
			this.Action = action;
		}

	}

}
