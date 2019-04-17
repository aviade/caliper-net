namespace ImsGlobal.Caliper.Entities.Feedback
{
    public class Feedback : Entity
    {
        public Feedback(string id, ICaliperContext caliperContext = null)
            : base(id, caliperContext)
        {
            if (caliperContext == null)
                Context = CaliperContext.FeedbackProfileExtensionV1p1.Value;

            this.Type = EntityType.Feedback;
        }
    }
}