using System;
using System.IO;
using System.Linq;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Common.Utility;

namespace PokeRandomizer.Common.Structures.RomFS.Gen6
{
	public class TrainerData : BaseDataStructure
	{
		public const int BattleTypeSingle = 0;
		public const int BattleTypeDouble = 1;

		public TrainerData(GameVersion gameVersion) : base(gameVersion) { }

		public bool      PartialEntry { get; private set; }
		public int       Format       { get; set; }
		public int       Class        { get; set; }
		public bool      Item         { get; set; }
		public bool      Moves        { get; set; }
		public byte      BattleType   { get; set; }
		public byte      NumPokemon   { get; set; }
		public byte      AI           { get; set; }
		public ushort[]  Items        { get; private set; }
		public byte      Unused1      { get; set; }
		public byte      Unused2      { get; set; }
		public byte      Unused3      { get; set; }
		public ushort    UnusedORAS   { get; set; }
		public bool      Healer       { get; set; }
		public byte      Money        { get; set; }
		public ushort    Prize        { get; set; }
		public Pokemon[] Team         { get; set; }

		public void ReadTeam(byte[] trainerPokeData)
		{
			// Fetch Team
			Team = new Pokemon[NumPokemon];

			if (NumPokemon == 0)
				return;

			byte[][] teamData = trainerPokeData.ToArray().Partition(trainerPokeData.Length / NumPokemon);

			for (int i = 0; i < NumPokemon; i++)
			{
				Pokemon pkm = new Pokemon(GameVersion, Item, Moves);
				pkm.Read(teamData[i]);
				Team[i] = pkm;
			}
		}

		public byte[] WriteTeam() => Team.Length == 0
										 ? new byte[6]
										 : Team.Aggregate(Array.Empty<byte>().AsEnumerable(), (data, pkm) => {
											 pkm.HasItem = Item;
											 pkm.HasMoves = Moves;
											 return data.Concat(pkm.Write());
										 }).ToArray();

		protected override void ReadData(BinaryReader br)
		{
			var oras = GameVersion.IsORAS();

			if (oras)
				PartialEntry = br.BaseStream.Length < 24;
			else
				PartialEntry = br.BaseStream.Length < 20;

			Items = new ushort[4];

			if (oras)
			{
				Format = br.ReadUInt16();
				Class = br.ReadUInt16();
				UnusedORAS = br.ReadUInt16();
			}
			else
			{
				Format = br.ReadByte();
				Class = br.ReadByte();
			}

			Item = ( ( Format >> 1 ) & 1 ) == 1;
			Moves = ( Format & 1 ) == 1;
			BattleType = br.ReadByte();
			NumPokemon = br.ReadByte();

			for (int i = 0; i < 4; i++)
				Items[i] = br.ReadUInt16();

			if (PartialEntry)
				return;

			AI = br.ReadByte();
			Unused1 = br.ReadByte();
			Unused2 = br.ReadByte();
			Unused3 = br.ReadByte();
			Healer = br.ReadByte() != 0;
			Money = br.ReadByte();
			Prize = br.ReadUInt16();
		}

		protected override void WriteData(BinaryWriter bw)
		{
			Format = Convert.ToByte(Moves) + ( Convert.ToByte(Item) << 1 );

			if (GameVersion.IsORAS())
			{
				bw.Write((ushort) Format);
				bw.Write((ushort) Class);
				bw.Write(UnusedORAS);
			}
			else
			{
				bw.Write((byte) Format);
				bw.Write((byte) Class);
			}

			bw.Write(BattleType);
			bw.Write(NumPokemon);
			Items.ForEach(bw.Write);

			if (PartialEntry)
				return;

			bw.Write(AI);
			bw.Write(Unused1);
			bw.Write(Unused2);
			bw.Write(Unused3);
			bw.Write(Convert.ToByte(Healer));
			bw.Write(Money);
			bw.Write(Prize);
		}

		public class Pokemon : BaseDataStructure
		{
			public Pokemon(GameVersion gameVersion, bool hasItem, bool hasMoves) : base(gameVersion)
			{
				HasItem = hasItem;
				HasMoves = hasMoves;
			}

			public bool HasItem  { get; set; }
			public bool HasMoves { get; set; }

			// ReSharper disable once InconsistentNaming
			public byte IVs { get; set; }

			public byte     Pid     { get; set; }
			public ushort   Level   { get; set; }
			public ushort   Species { get; set; }
			public ushort   Form    { get; set; }
			public int      Ability { get; set; }
			public int      Gender  { get; set; }
			public int      UBit    { get; set; }
			public ushort   Item    { get; set; }
			public ushort[] Moves   { get; set; }

			protected override void ReadData(BinaryReader br)
			{
				Moves = new ushort[4];

				IVs = br.ReadByte();
				Pid = br.ReadByte();
				Level = br.ReadUInt16();
				Species = br.ReadUInt16();
				Form = br.ReadUInt16();

				Ability = Pid >> 4;
				Gender = Pid & 3;
				UBit = ( Pid >> 3 ) & 1;

				if (HasItem)
					Item = br.ReadUInt16();

				if (HasMoves)
					for (int i = 0; i < 4; i++)
						Moves[i] = br.ReadUInt16();
			}

			protected override void WriteData(BinaryWriter bw)
			{
				Pid = (byte) ( ( ( Ability & 0b1111 ) << 4 ) |
								    ( ( UBit & 0b1 ) << 3 ) |
								    ( Gender & 0b11 ) );

				bw.Write(IVs);
				bw.Write(Pid);
				bw.Write(Level);
				bw.Write(Species);
				bw.Write(Form);

				if (HasItem)
					bw.Write(Item);

				if (HasMoves)
					foreach (ushort move in Moves)
						bw.Write(move);
			}
		}
	}
}