namespace ImsGlobal.Caliper.Events.Tool {

	public class ToolUseEvent : Event {

		public ToolUseEvent(string id, Action action, ICaliperContext caliperContext = null) 
			:base(id, caliperContext){
			this.Type = EventType.ToolUse;
			this.Action = action;
		}

    }

}
