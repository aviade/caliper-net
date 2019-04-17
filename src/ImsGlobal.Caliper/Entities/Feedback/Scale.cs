using Newtonsoft.Json;

namespace ImsGlobal.Caliper.Entities.Feedback
{
    public class Scale : Entity
    {
        public Scale(string id, ICaliperContext caliperContext = null)
            : base(id, caliperContext)
        {
            if (caliperContext == null)
                Context = CaliperContext.FeedbackProfileExtensionV1p1.Value;

            this.Type = EntityType.Scale;
        }
    }
}