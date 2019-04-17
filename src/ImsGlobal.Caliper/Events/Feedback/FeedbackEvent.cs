using System.Collections.Generic;

namespace ImsGlobal.Caliper.Events.Annotation {
	using ImsGlobal.Caliper.Entities;
    using ImsGlobal.Caliper.Entities.Feedback;

    /// <summary>
    /// Event raised when an actor annotates a resource.
    /// </summary>
    public class FeedbackEvent : Event {

		private static readonly Dictionary<IType, Action> _EntityTypeToAction = new Dictionary<IType, Action> {
			{ EntityType.Comment, Action.Commented },
			{ EntityType.Rating, Action.Ranked }
		};

		public FeedbackEvent(string id, Feedback entity, ICaliperContext caliperContext = null) 
			: base(id, caliperContext) {

            if (caliperContext == null)
                this.Context = CaliperContext.FeedbackProfileExtensionV1p1;

            this.Type = EventType.Feedback;
            this.Action = MapEntityToAction(entity);
            this.Generated = entity;
        }

        private static Action MapEntityToAction(Feedback entity)
        {
            _EntityTypeToAction.TryGetValue(entity.Type, out Action action);
            return action;
        }
    }

}
