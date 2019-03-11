using System.Collections.Generic;

namespace ImsGlobal.Caliper.Events.Annotation {
	using ImsGlobal.Caliper.Entities;
	using Annotation = ImsGlobal.Caliper.Entities.Annotation.Annotation;

	/// <summary>
	/// Event raised when an actor annotates a resource.
	/// </summary>
	public class AnnotationEvent : Event {

		private static readonly Dictionary<IType, Action> _EntityTypeToAction = new Dictionary<IType, Action> {
			{ EntityType.Bookmark, Action.Bookmarked },
			{ EntityType.Highlight, Action.Highlighted },
			{ EntityType.Share, Action.Shared },
			{ EntityType.Tag, Action.Tagged }
		};

		public AnnotationEvent(string id, Annotation annotation, ICaliperContext caliperContext = null) 
			: base(id, caliperContext) {
			this.Type = EventType.Annotation;
			this.Action = MapAnnotationEntityToAction( annotation );
			this.Generated = annotation;
		}

		private static Action MapAnnotationEntityToAction( Annotation annotation ) {
			Action action;
			_EntityTypeToAction.TryGetValue( annotation.Type, out action );
			return action;
		}

	}

}
