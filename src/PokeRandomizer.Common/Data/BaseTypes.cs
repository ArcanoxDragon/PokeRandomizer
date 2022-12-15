namespace PokeRandomizer.Common.Data
{
	public abstract class BaseAbility : EnumType
	{
		protected BaseAbility(int id, string name) : base(id, name) { }
	}

	public abstract class BaseItem : EnumType
	{
		protected BaseItem(int id, string name) : base(id, name) { }
	}

	public abstract class BaseMove : EnumType
	{
		protected BaseMove(int id, string name) : base(id, name) { }
	}

	public abstract class BaseSpeciesType : EnumType
	{
		protected BaseSpeciesType(int id, string name) : base(id, name) { }
	}

	public abstract class BasePokemonType : EnumType
	{
		protected BasePokemonType(int id, string name) : base(id, name) { }
	}
}