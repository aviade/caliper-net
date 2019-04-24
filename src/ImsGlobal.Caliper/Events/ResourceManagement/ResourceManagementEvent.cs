namespace ImsGlobal.Caliper.Events.Assessment {

    /// <summary>
    /// Models a Person managing an Entity
    /// </summary>
    public class ResourceManagementEvent : Event {

		public ResourceManagementEvent(string id,  Action action, ICaliperContext caliperContext = null) 
			: base(id, caliperContext) {

            if (caliperContext == null)
                this.Context = new CaliperContextCollection(CaliperContext.ResourceManagementProfileExtensionV1p1);

            this.Type = EventType.ResourceManagement;
			this.Action = action;
		}

	}

}
