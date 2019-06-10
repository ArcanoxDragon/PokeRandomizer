using System.IO;
using System.Text;

namespace CtrDotNet.CTR
{
	// System Menu Data Header
	public class Smdh
	{
		public uint Magic;
		public ushort Version;
		public ushort Reserved2;
		public readonly ApplicationInfo[] AppInfo = new ApplicationInfo[ 16 ];
		public ApplicationSettings AppSettings;
		public ulong Reserved8;
		public SmallIcon SmallIcon;
		public LargeIcon LargeIcon;

		public Smdh( byte[] data )
		{
			this.Read( new BinaryReader( new MemoryStream( data ) ) );
		}

		public Smdh( string path )
		{
			this.Read( new BinaryReader( File.OpenRead( path ) ) );
		}

		public void Read( BinaryReader br )
		{
			// Check to see if the first 4 bytes (magic) is valid.
			if ( br.BaseStream.Length != 0x36C0 || ( this.Magic = br.ReadUInt32() ) != 0x48444D53 )
				return; // Abort

			this.Version = br.ReadUInt16();
			this.Reserved2 = br.ReadUInt16();

			for ( int i = 0; i < 16; i++ )
				this.AppInfo[ i ] = new ApplicationInfo( br );

			this.AppSettings = new ApplicationSettings( br );
			this.Reserved8 = br.ReadUInt64();
			this.SmallIcon = new SmallIcon( br );
			this.LargeIcon = new LargeIcon( br );
		}

		public byte[] Write()
		{
			using ( var ms = new MemoryStream() )
			using ( var bw = new BinaryWriter( ms ) )
			{
				bw.Write( this.Magic );
				bw.Write( this.Version );
				bw.Write( this.Reserved2 );
				for ( int i = 0; i < 16; i++ )
					this.AppInfo[ i ].Write( bw );
				this.AppSettings.Write( bw );
				bw.Write( this.Reserved8 );
				this.SmallIcon.Write( bw );
				this.LargeIcon.Write( bw );
				return ms.ToArray();
			}
		}
	}

	// Thanks to Gericom for EveryFileExplorer's SMDH.cs as a basis for the object code (and AppSettings enumeration)
	public class ApplicationInfo
	{
		public readonly string ShortDescription; //0x80
		public readonly string LongDescription; //0x100
		public readonly string Publisher; //0x80

		public ApplicationInfo( BinaryReader br )
		{
			this.ShortDescription = Encoding.Unicode.GetString( br.ReadBytes( 0x80 ) ).TrimEnd( '\0' );
			this.LongDescription = Encoding.Unicode.GetString( br.ReadBytes( 0x100 ) ).TrimEnd( '\0' );
			this.Publisher = Encoding.Unicode.GetString( br.ReadBytes( 0x80 ) ).TrimEnd( '\0' );
		}

		public void Write( BinaryWriter bw )
		{
			bw.Write( Encoding.Unicode.GetBytes( this.ShortDescription.PadRight( 0x80 / 2, '\0' ) ) );
			bw.Write( Encoding.Unicode.GetBytes( this.LongDescription.PadRight( 0x100 / 2, '\0' ) ) );
			bw.Write( Encoding.Unicode.GetBytes( this.Publisher.PadRight( 0x80 / 2, '\0' ) ) );
		}
	}

	public class ApplicationSettings
	{
		public readonly byte[] GameRatings; //0x10
		public readonly RegionLockoutFlags RegionLockout;
		public readonly uint MatchMakerID;
		public readonly ulong MatchMakerBITID;
		public readonly AppSettingsFlags Flags;
		public readonly ushort EULAVersion;
		public readonly ushort Reserved;
		public readonly float AnimationDefaultFrame;
		public readonly uint StreetPassID;

		public enum RegionLockoutFlags : uint
		{
			Japan = 0x01,
			NorthAmerica = 0x02,
			Europe = 0x04,
			Australia = 0x08,
			China = 0x10,
			Korea = 0x20,
			Taiwan = 0x40
		}

		public enum AppSettingsFlags : uint
		{
			Visible = 1,
			AutoBoot = 2,
			Allow3D = 4,
			ReqAcceptEula = 8,
			AutoSaveOnExit = 16,
			UsesExtendedBanner = 32,
			ReqRegionGameRating = 64,
			UsesSaveData = 128,
			RecordUsage = 256,
			DisableSDSaveBackup = 512
		}

		public ApplicationSettings( BinaryReader br )
		{
			this.GameRatings = br.ReadBytes( 0x10 );
			this.RegionLockout = (RegionLockoutFlags) br.ReadUInt32();
			this.MatchMakerID = br.ReadUInt32();
			this.MatchMakerBITID = br.ReadUInt64();
			this.Flags = (AppSettingsFlags) br.ReadUInt32();
			this.EULAVersion = br.ReadUInt16();
			this.Reserved = br.ReadUInt16();
			this.AnimationDefaultFrame = br.ReadSingle();
			this.StreetPassID = br.ReadUInt32();
		}

		public void Write( BinaryWriter bw )
		{
			bw.Write( this.GameRatings, 0, 0x10 );
			bw.Write( (uint) this.RegionLockout );
			bw.Write( this.MatchMakerID );
			bw.Write( this.MatchMakerBITID );
			bw.Write( (uint) this.Flags );
			bw.Write( this.EULAVersion );
			bw.Write( this.Reserved );
			bw.Write( this.AnimationDefaultFrame );
			bw.Write( this.StreetPassID );
		}
	}

	public abstract class Icon
	{
		public byte[] Bytes { get; set; }
		public int Size { get; }

		protected Icon( int size, BinaryReader br )
		{
			this.Size = size;
			this.Bytes = br.ReadBytes( size * size * 2 );
		}

		public void Write( BinaryWriter bw )
		{
			bw.Write( this.Bytes );
		}
	}

	public class SmallIcon : Icon // 24x24
	{
		public SmallIcon( BinaryReader br ) : base( 24, br ) { }
	}

	public class LargeIcon : Icon // 48x48
	{
		public LargeIcon( BinaryReader br ) : base( 48, br ) { }
	}
}