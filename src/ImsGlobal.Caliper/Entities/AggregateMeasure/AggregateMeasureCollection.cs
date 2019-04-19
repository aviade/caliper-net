namespace ImsGlobal.Caliper.Entities.AggregateMeasure
{
    public class AggregateMeasureCollection : Collection.Collection<AggregateMeasure>
    {
        public AggregateMeasureCollection(string id, ICaliperContext caliperContext = null)
            : base(id, caliperContext)
        {
            this.Type = EntityType.AggregateMeasureCollection;
        }
    }
}