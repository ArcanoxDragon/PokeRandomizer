using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CtrDotNet.Pokemon.Structures.RomFS.Common;

namespace CtrDotNet.Pokemon.GameData
{
	public class TextFile : IDataStructure
	{
		#region Static

		private class LineInfo
		{
			public int Offset { get; set; }
			public short Length { get; set; }
			public short Unused { get; set; }
		}

		private const ushort LineInfoSize = 8; // Int32(4) Offset + Int16(2) Length + Int16(2) Unknown
		private const byte BytesPerCharacter = 2;
		private static readonly byte[] EmptyTextFile = { 0x01, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00 };

		#endregion

		private string[] lineArray;
		private byte[][] lineData;
		private readonly TextVariableCode[] variables;

		public TextFile( byte[] data = null, TextVariableCode[] variables = null )
		{
			variables = variables ?? new TextVariableCode[ 0 ];
			data = data ?? EmptyTextFile;

			this.variables = variables;

			this.Read( data );

			if ( this.InitialKey != 0 )
				throw new Exception( $"Invalid initial key: {this.InitialKey} (expected: 0)" );

			if ( this.SectionDataOffset + this.TotalLength != data.Length || this.TextSections != 1 )
				throw new Exception( "Invalid Text File" );

			if ( this.SectionLength != this.TotalLength )
				throw new Exception( "Section size and overall size do not match." );
		}

		public void Read( byte[] data )
		{
			using ( var ms = new MemoryStream( data ) )
			using ( var br = new BinaryReader( ms ) )
			{
				this.TextSections = br.ReadUInt16();
				ushort lineCount = br.ReadUInt16();
				this.TotalLength = br.ReadUInt32();
				this.InitialKey = br.ReadUInt32();
				this.SectionDataOffset = br.ReadUInt32();

				ms.Seek( this.SectionDataOffset, SeekOrigin.Begin );

				this.SectionLength = br.ReadUInt32();
				this.LineOffsets = new LineInfo[ lineCount ];

				for ( int i = 0; i < this.LineOffsets.Length; i++ )
				{
					this.LineOffsets[ i ] = new LineInfo {
						Offset = br.ReadInt32(),
						Length = br.ReadInt16(),
						Unused = br.ReadInt16() // something
					};
				}

				this.lineData = new byte[ this.LineOffsets.Length ][];

				for ( int i = 0; i < this.LineOffsets.Length; i++ )
				{
					this.lineData[ i ] = new byte[ this.LineOffsets[ i ].Length * BytesPerCharacter ];
					Array.Copy( data, this.LineOffsets[ i ].Offset + this.SectionDataOffset, this.lineData[ i ], 0, this.lineData[ i ].Length );
				}

				this.lineArray = TextFileHelper.DecryptLines( this, this.lineData );
			}
		}

		public byte[] Write()
		{
			using ( var ms = new MemoryStream( new byte[ this.DataLength ] ) )
			using ( var bw = new BinaryWriter( ms ) )
			{
				bw.Write( this.TextSections );
				bw.Write( this.LineCount );
				bw.Write( this.TotalLength );
				bw.Write( this.InitialKey );
				bw.Write( this.SectionDataOffset );

				ms.Seek( this.SectionDataOffset, SeekOrigin.Begin );

				bw.Write( this.SectionLength );

				foreach ( LineInfo lineInfo in this.LineOffsets )
				{
					bw.Write( (int) ( lineInfo.Offset - this.SectionDataOffset ) );
					bw.Write( lineInfo.Length );
					bw.Write( lineInfo.Unused );
				}

				ms.Seek( this.DataLength - this.RawTextLength, SeekOrigin.Begin );
				byte[] aggregateLineData = this.lineData.SelectMany( i => i ).ToArray();
				ms.Write( aggregateLineData, 0, aggregateLineData.Length );

				return ms.ToArray();
			}
		}

		private int DataLength => (int) ( this.SectionDataOffset + this.TotalLength );
		private int LineOffsetsSectionLength => this.LineOffsets.Length * LineInfoSize + 4; // + 4 because of UInt32(4) SectionLength
		private int RawTextLength => this.lineData.Sum( ld => ld.Length );
		private ushort LineCount => (ushort) this.Lines.Count;
		private uint TotalLength { get; set; }
		private uint SectionLength { get; set; }
		private LineInfo[] LineOffsets { get; set; }

		/// Always 0x0001
		private ushort TextSections { get; set; }

		/// Always 0x0010
		private uint SectionDataOffset { get; set; }

		/// Always 0x00000000
		private uint InitialKey { get; set; }

		public IReadOnlyList<string> Lines => this.lineArray;

		public void SetLines( string[] lines )
		{
			lines = lines ?? new string[ 0 ];

			this.lineData = TextFileHelper.EncryptLines( this, lines );
			this.LineOffsets = new LineInfo[ lines.Length ];

			for ( var (i, curOffset) = (0, 0); i < this.LineOffsets.Length; i++ )
			{
				this.LineOffsets[ i ] = new LineInfo {
					Offset = this.LineOffsetsSectionLength + curOffset,
					Length = (short) ( this.lineData[ i ].Length / BytesPerCharacter )
				};
				curOffset += this.lineData[ i ].Length;
			}

			this.TotalLength = this.SectionLength = (uint) ( this.LineOffsetsSectionLength + this.RawTextLength );
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