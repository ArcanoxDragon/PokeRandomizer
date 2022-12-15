namespace PokeRandomizer.Common.Structures.RomFS.Gen7
{
	public class Maison
	{
		public class Trainer : Gen6.Maison.Trainer
		{
			public Trainer() { }
			public Trainer(byte[] data) : base(data) { }
		}

		public class Pokemon : Gen6.Maison.Pokemon
		{
			public Pokemon(byte[] data) : base(data) { }
		}
	}
}