namespace ImsGlobal.Caliper.Entities.Agent {
	public class Person : Agent {

		public Person( string id )
			: base( id ) {
			this.Type = EntityType.Person;
		}

	}
}