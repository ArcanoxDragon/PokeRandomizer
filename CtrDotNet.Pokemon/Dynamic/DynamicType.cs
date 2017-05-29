namespace CtrDotNet.Pokemon.Dynamic
{
	public abstract class DynamicType
	{
		protected DynamicType( int id, string name )
		{
			this.Id = id;
			this.Name = name;
		}

		public int Id { get; }
		public string Name { get; }
	}
}