namespace PokeRandomizer.Common.Data
{
	public abstract class EnumType
	{
		protected EnumType(int id, string name)
		{
			Id = id;
			Name = name;
		}

		public int    Id   { get; }
		public string Name { get; }

		#region Equality

		protected bool Equals(EnumType other)
		{
			return Id == other.Id;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			if (ReferenceEquals(this, obj))
				return true;

			return obj.GetType() == GetType() && Equals((EnumType) obj);
		}

		public override int GetHashCode()
		{
			return Id;
		}

		public static bool operator ==(EnumType left, EnumType right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(EnumType left, EnumType right)
		{
			return !Equals(left, right);
		}

		#endregion

		#region object overrides

		public override string ToString() => $"{Id}:{Name}";

		#endregion
	}
}