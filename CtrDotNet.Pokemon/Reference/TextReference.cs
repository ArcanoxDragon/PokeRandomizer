namespace CtrDotNet.Pokemon.Reference
{
	public class TextReference
	{
		public readonly int Index;
		public readonly TextNames Name;

		internal TextReference( int index, TextNames name )
		{
			this.Index = index;
			this.Name = name;
		}
	}
}
