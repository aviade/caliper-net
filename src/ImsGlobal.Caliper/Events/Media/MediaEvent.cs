namespace ImsGlobal.Caliper.Events.Media {

	/// <summary>
	/// Event raised when an actor interacts with a media resource.
	/// </summary>
	public class MediaEvent : Event {

		public MediaEvent(string id, Action action, ICaliperContext caliperContext = null) 
			:base(id, caliperContext) {
			this.Type = EventType.Media;
			this.Action = action;
		}

	}

}
