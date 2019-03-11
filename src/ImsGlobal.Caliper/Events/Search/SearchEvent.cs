namespace ImsGlobal.Caliper.Events.Search
{
    public class SearchEvent : Event
    {

        public SearchEvent(string id, Action action, ICaliperContext caliperContext = null)
            : base(id, caliperContext)
        {
            this.Type = EventType.Search;
            this.Action = action;
        }
    }
}
