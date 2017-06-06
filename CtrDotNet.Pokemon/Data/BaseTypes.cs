using CtrDotNet.Pokemon.Dynamic;

namespace CtrDotNet.Pokemon.Data
{
	public abstract class BaseAbility : DynamicType
	{
		protected BaseAbility( int id, string name ) : base( id, name ) { }
	}

	public abstract class BaseItem : DynamicType
	{
		protected BaseItem( int id, string name ) : base( id, name ) { }
	}

	public abstract class BaseMove : DynamicType
	{
		protected BaseMove( int id, string name ) : base( id, name ) { }
	}

	public abstract class BaseSpeciesType : DynamicType
	{
		protected BaseSpeciesType( int id, string name ) : base( id, name ) { }
	}

	public abstract class BasePokemonType : DynamicType
	{
		protected BasePokemonType( int id, string name ) : base( id, name ) { }
	}
}