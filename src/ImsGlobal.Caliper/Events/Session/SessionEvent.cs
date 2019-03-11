namespace ImsGlobal.Caliper.Events.Session {

	public class SessionEvent : Event {

		public SessionEvent(string id, Action action, ICaliperContext caliperContext = null) 
			:base (id, caliperContext) {
			this.Type = EventType.Session;
			this.Action = action;
		}

	}

}
