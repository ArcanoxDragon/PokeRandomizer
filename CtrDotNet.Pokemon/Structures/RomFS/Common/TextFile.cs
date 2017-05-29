using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Structures.ExeFS.Common;
using CtrDotNet.Pokemon.Utility;

namespace CtrDotNet.Pokemon.Structures.RomFS.Common
{
	public sealed class TextFile : BaseDataStructure
	{
		#region Static

		internal class LineInfo
		{
			public uint Offset { get; set; }
			public ushort Length { get; set; }
			public ushort Unused { get; set; }
		}

		private const ushort LineInfoSize = 8; // Int32(4) Offset + Int16(2) Length + Int16(2) Unknown
		private const byte BytesPerCharacter = 2;
		private static readonly byte[] EmptyTextFile = { 0x01, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00 };

		#endregion

		private string[] lineArray;
		private byte[][] lineData;
		private readonly TextVariableCode[] variables;

		public TextFile( GameVersion gameVersion, TextVariableCode[] variables = null ) : base( gameVersion )
		{
			variables = variables ?? new TextVariableCode[ 0 ];

			this.variables = variables;

			this.Read( EmptyTextFile );
		}

		private int DataLength => (int) ( this.SectionDataOffset + this.TotalLength );
		private int LineInfoSectionLength => this.LineInfos.Length * LineInfoSize + 4; // + 4 because of UInt32(4) SectionLength
		private int RawTextLength => this.lineData.Sum( ld => ld.Length );
		private ushort LineCount => (ushort) this.Lines.Count;
		private uint TotalLength => (uint) ( this.LineInfoSectionLength + this.RawTextLength );
		private uint SectionLength => this.TotalLength;
		internal LineInfo[] LineInfos { get; set; }

		/// Always 0x0001
		private ushort TextSections { get; set; }

		/// Always 0x0010
		private uint SectionDataOffset { get; set; }

		/// Always 0x00000000
		private uint InitialKey { get; set; }

		public IReadOnlyList<string> Lines => this.lineArray;

		public override void Read( byte[] data )
		{
			base.Read( data );

			if ( this.InitialKey != 0 )
				throw new Exception( $"Invalid initial key: {this.InitialKey} (expected: 0)" );
//
//			if ( this.SectionDataOffset + this.TotalLength != data.Length || this.TextSections != 1 )
//				throw new Exception( "Invalid Text File" );
		}

		protected override void ReadData( BinaryReader br )
		{
			this.TextSections = br.ReadUInt16();
			ushort lineCount = br.ReadUInt16();
			uint totalLength = br.ReadUInt32();
			this.InitialKey = br.ReadUInt32();
			this.SectionDataOffset = br.ReadUInt32();
			uint sectionLength = br.ReadUInt32();

			this.LineInfos = new LineInfo[ lineCount ];

			for ( int i = 0; i < this.LineInfos.Length; i++ )
			{
				this.LineInfos[ i ] = new LineInfo {
					Offset = br.ReadUInt32(),
					Length = br.ReadUInt16(),
					Unused = br.ReadUInt16() // something
				};
			}

			this.lineData = new byte[ lineCount ][];

			for ( int i = 0; i < this.LineInfos.Length; i++ )
			{
				this.lineData[ i ] = new byte[ this.LineInfos[ i ].Length * BytesPerCharacter ];
				br.BaseStream.Seek( this.LineInfos[ i ].Offset + this.SectionDataOffset, SeekOrigin.Begin );
				br.Read( this.lineData[ i ], 0, this.lineData[ i ].Length );
			}

			this.SetLines( TextFileHelper.DecryptLines( this, this.lineData ) );

//			if ( sectionLength != totalLength )
//				throw new InvalidDataException( "File-specified totalLength and sectionLength did not match" );
//
//			if ( this.TotalLength != totalLength )
//				throw new InvalidDataException( $"File specified total length should be {totalLength}, but it was {this.TotalLength}" );
		}

		protected override void WriteData( BinaryWriter bw )
		{
			this.lineData = TextFileHelper.EncryptLines( this, this.lineArray );

			bw.Write( this.TextSections );
			bw.Write( this.LineCount );
			bw.Write( this.TotalLength );
			bw.Write( this.InitialKey );
			bw.Write( this.SectionDataOffset );
			bw.Write( this.SectionLength );

			foreach ( LineInfo lineInfo in this.LineInfos )
			{
				bw.Write( lineInfo.Offset );
				bw.Write( lineInfo.Length );
				bw.Write( lineInfo.Unused );
			}

			byte[] aggregateLineData = this.lineData.SelectMany( i => i ).ToArray();
			bw.Write( aggregateLineData, 0, aggregateLineData.Length );
		}

		public void SetLines( string[] lines )
		{
			lines = lines ?? new string[ 0 ];

			this.lineData = TextFileHelper.EncryptLines( this, lines );
			this.LineInfos = new LineInfo[ lines.Length ];

			for ( var (i, curOffset) = (0, 0); i < this.LineInfos.Length; i++ )
			{
				this.LineInfos[ i ] = new LineInfo {
					Offset = (uint) ( this.LineInfoSectionLength + curOffset ),
					Length = (ushort) ( this.lineData[ i ].Length / BytesPerCharacter )
				};
				curOffset += this.lineData[ i ].Length;
			}

			this.lineArray = lines;
		}

		public void SetLine( int index, string value )
		{
			string[] lineArray = this.lineArray;

			if ( index < 0 || index >= lineArray.Length )
				throw new ArgumentOutOfRangeException( nameof(index) );

			lineArray[ index ] = value;

			this.SetLines( lineArray );
		}

		internal IEnumerable<ushort> GetVariableValues( string variable )
		{
			string[] split = variable.Split( ' ' );
			if ( split.Length < 2 )
				throw new ArgumentException( "Incorrectly formatted variable text!" );

			var vals = new List<ushort> { TextFileHelper.KeyVariable };
			switch ( split[ 0 ] )
			{
				case "~": // Blank Text Line Variable (No text set - debug/quality testing variable?)
					vals.Add( 1 );
					vals.Add( TextFileHelper.KeyTextNull );
					vals.Add( Convert.ToUInt16( split[ 1 ] ) );
					break;
				case "WAIT": // Event pause Variable.
					vals.Add( 1 );
					vals.Add( TextFileHelper.KeyTextWait );
					vals.Add( Convert.ToUInt16( split[ 1 ] ) );
					break;
				case "VAR": // Text Variable
					vals.AddRange( this.GetVariableParameters( split[ 1 ] ) );
					break;
				default: throw new Exception( "Unknown variable method type!" );
			}
			return vals;
		}

		internal IEnumerable<ushort> GetVariableParameters( string text )
		{
			var vals = new List<ushort>();
			int bracket = text.IndexOf( '(' );
			bool noArgs = bracket < 0;
			string variable = noArgs ? text : text.Substring( 0, bracket );
			ushort varVal = this.GetVariableNumber( variable );

			if ( !noArgs )
			{
				string[] args = text.Substring( bracket + 1, text.Length - bracket - 2 ).Split( ',' );
				vals.Add( (ushort) ( 1 + args.Length ) );
				vals.Add( varVal );
				vals.AddRange( args.Select( t => Convert.ToUInt16( t, 16 ) ) );
			}
			else
			{
				vals.Add( 1 );
				vals.Add( varVal );
			}
			return vals;
		}

		internal ushort GetVariableNumber( string variable )
		{
			var var = this.variables.FirstOrDefault( v => v.Name == variable );

			if ( var != null )
				return (ushort) var.Code;

			try
			{
				return Convert.ToUInt16( variable, 16 );
			}
			catch
			{
				throw new ArgumentException( $"Variable \"{variable}\" parse error." );
			}
		}

		internal string GetVariableString( ushort variable )
		{
			var var = this.variables.FirstOrDefault( v => v.Code == variable );
			return var == null ? variable.ToString( "X4" ) : var.Name;
		}
	}
}