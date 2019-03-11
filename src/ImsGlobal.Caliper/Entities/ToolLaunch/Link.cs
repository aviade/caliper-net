namespace ImsGlobal.Caliper.Entities.ToolLaunch
{
    public class Link : Entity
    {
        public Link(string id, ICaliperContext caliperContext = null)
            : base(id, caliperContext)
        {
            this.Type = EntityType.Link;
        }
    }
}