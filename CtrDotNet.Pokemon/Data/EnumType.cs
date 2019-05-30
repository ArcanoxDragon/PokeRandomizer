namespace CtrDotNet.Pokemon.Data
{
	public abstract class EnumType
	{
		protected EnumType( int id, string name )
		{
			this.Id   = id;
			this.Name = name;
		}

		public int    Id   { get; }
		public string Name { get; }

		#region Equality

		protected bool Equals( EnumType other )
		{
			return this.Id == other.Id;
		}

		public override bool Equals( object obj )
		{
			if ( object.ReferenceEquals( null, obj ) )
				return false;
			if ( object.ReferenceEquals( this, obj ) )
				return true;

			return obj.GetType() == this.GetType() && this.Equals( (EnumType) obj );
		}

		public override int GetHashCode()
		{
			return this.Id;
		}

		public static bool operator ==( EnumType left, EnumType right )
		{
			return object.Equals( left, right );
		}

		public static bool operator !=( EnumType left, EnumType right )
		{
			return !object.Equals( left, right );
		}

		#endregion

		#region object overrides

		public override string ToString() => $"{this.Id}:{this.Name}";

		#endregion
	}
}