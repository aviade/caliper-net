namespace ImsGlobal.Caliper.Events.Search
{
    public class ToolLaunchEvent : Event
    {

        public ToolLaunchEvent(string id, Action action, ICaliperContext caliperContext = null)
            : base(id, caliperContext)
        {
            this.Type = EventType.ToolLaunch;
            this.Action = action;
        }
    }
}