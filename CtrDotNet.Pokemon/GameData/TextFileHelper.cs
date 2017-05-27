using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CtrDotNet.Pokemon.GameData
{
	public class TextFileHelper
	{
		#region Text Formatting Config

		internal const ushort KeyBase = 0x7C89;
		internal const ushort KeyAdvance = 0x2983;
		internal const ushort KeyVariable = 0x0010;
		internal const ushort KeyTerminator = 0x0000;
		internal const ushort KeyTextReturn = 0xBE00;
		internal const ushort KeyTextClear = 0xBE01;
		internal const ushort KeyTextWait = 0xBE02;
		internal const ushort KeyTextNull = 0xBDFF;
		internal const bool SetEmptyText = false;

		#endregion

		internal static string[] DecryptLines( TextFile textFile, byte[][] encryptedLines )
		{
			ushort key = KeyBase;
			string[] lines = new string[ encryptedLines.Length ];

			for ( int line = 0; line < encryptedLines.Length; line++ )
			{
				byte[] lineData = TextFileHelper.CryptLineData( encryptedLines[ line ], key );
				lines[ line ] = TextFileHelper.GetLineString( textFile, lineData );
				key += KeyAdvance;
			}

			return lines;
		}

		internal static byte[][] EncryptLines( TextFile textFile, string[] lines )
		{
			ushort key = KeyBase;
			byte[][] lineData = new byte[ lines.Length ][];

			for ( int i = 0; i < lines.Length; i++ )
			{
				string text = ( lines[ i ] ?? "" ).Trim();

				// ReSharper disable once ConditionIsAlwaysTrueOrFalse
				if ( text.Length == 0 && TextFileHelper.SetEmptyText )
					text = $"[~ {i}]";

				byte[] decryptedLineData = TextFileHelper.GetLineData( textFile, text );
				lineData[ i ] = TextFileHelper.CryptLineData( decryptedLineData, key );

				if ( lineData[ i ].Length % 4 == 2 )
					Array.Resize( ref lineData[ i ], lineData[ i ].Length + 2 );

				key += TextFileHelper.KeyAdvance;
			}

			return lineData;
		}

		internal static byte[] CryptLineData( byte[] data, ushort key )
		{
			byte[] result = new byte[ data.Length ];
			for ( int i = 0; i < result.Length; i += 2 )
			{
				BitConverter.GetBytes( (ushort) ( BitConverter.ToUInt16( data, i ) ^ key ) ).CopyTo( result, i );
				key = (ushort) ( key << 3 | key >> 13 );
			}
			return result;
		}

		internal static byte[] GetLineData( TextFile textFile, string line )
		{
			if ( line == null )
				return new byte[ 2 ];

			using ( var ms = new MemoryStream() )
			using ( var bw = new BinaryWriter( ms ) )
			{
				int i = 0;
				while ( i < line.Length )
				{
					ushort val = line[ i++ ];

					switch ( val )
					{
						case 0x202F:
							val = 0xE07F; // nbsp
							break;
						case 0x2026:
							val = 0xE08D; // …
							break;
						case 0x2642:
							val = 0xE08E; // ♂
							break;
						case 0x2640:
							val = 0xE08F; // ♀
							break;
					}

					switch ( val )
					{
						case '[':
							// grab the string
							int bracket = line.IndexOf( "]", i, StringComparison.Ordinal );
							if ( bracket < 0 )
								throw new ArgumentException( "Variable text is not capped properly." );
							string varText = line.Substring( i, bracket - i );
							var varValues = textFile.GetVariableValues( varText );
							foreach ( ushort v in varValues )
								bw.Write( v );
							i += 1 + varText.Length;
							break;
						case '\\':
							var escapeValues = TextFileHelper.GetEscapeValues( line[ i++ ] );
							foreach ( ushort v in escapeValues )
								bw.Write( v );
							break;
						default:
							bw.Write( val );
							break;
					}
				}
				bw.Write( KeyTerminator ); // cap the line off
				return ms.ToArray();
			}
		}

		internal static string GetLineString( TextFile textFile, byte[] data )
		{
			if ( data == null )
				return null;

			string s = "";
			int i = 0;
			while ( i < data.Length )
			{
				ushort val = BitConverter.ToUInt16( data, i );
				if ( val == KeyTerminator )
					break;
				i += 2;

				switch ( val )
				{
					case KeyTerminator: return s;
					case KeyVariable:
						s += TextFileHelper.GetVariableString( textFile, data, ref i );
						break;
					case '\n':
						s += @"\n";
						break;
					case '\\':
						s += @"\\";
						break;
					case '[':
						s += @"\[";
						break;
					case 0xE07F:
						s += (char) 0x202F;
						break; // nbsp
					case 0xE08D:
						s += (char) 0x2026;
						break; // …
					case 0xE08E:
						s += (char) 0x2642;
						break; // ♂
					case 0xE08F:
						s += (char) 0x2640;
						break; // ♀
					default:
						s += (char) val;
						break;
				}
			}
			return s; // Shouldn't get hit if the string is properly terminated.
		}

		internal static string GetVariableString( TextFile textFile, byte[] data, ref int i )
		{
			string s = "";
			ushort count = BitConverter.ToUInt16( data, i );
			i += 2;
			ushort variable = BitConverter.ToUInt16( data, i );
			i += 2;

			switch ( variable )
			{
				case KeyTextReturn: // "Waitbutton then scroll text; \r"
					return "\\r";
				case KeyTextClear: // "Waitbutton then clear text;; \c"
					return "\\c";
				case KeyTextWait: // Dramatic pause for a text line. New!
					ushort time = BitConverter.ToUInt16( data, i );
					i += 2;
					return $"[WAIT {time}]";
				case KeyTextNull: // Empty Text line? Includes linenum so maybe for betatest finding used-unused lines?
					ushort line = BitConverter.ToUInt16( data, i );
					i += 2;
					return $"[~ {line}]";
			}

			string varName = textFile.GetVariableString( variable );

			s += "[VAR" + " " + varName;
			if ( count > 1 )
			{
				s += '(';
				while ( count > 1 )
				{
					ushort arg = BitConverter.ToUInt16( data, i );
					i += 2;
					s += arg.ToString( "X4" );
					if ( --count == 1 )
						break;
					s += ",";
				}
				s += ')';
			}
			s += "]";
			return s;
		}

		internal static IEnumerable<ushort> GetEscapeValues( char esc )
		{
			var vals = new List<ushort>();
			switch ( esc )
			{
				case 'n':
					vals.Add( '\n' );
					return vals;
				case '\\':
					vals.Add( '\\' );
					return vals;
				case 'r':
					vals.AddRange( new ushort[] { KeyVariable, 1, KeyTextReturn } );
					return vals;
				case 'c':
					vals.AddRange( new ushort[] { KeyVariable, 1, KeyTextClear } );
					return vals;
				default: throw new Exception( "Invalid terminated line: \"\\" + esc + "\"" );
			}
		}

		// Exposed Methods
		internal static string[] GetStrings( byte[] data )
		{
			TextFile t;
			try
			{
				t = new TextFile( data );
			}
			catch
			{
				return null;
			}
			return t.Lines.ToArray();
		}

		internal static byte[] GetBytes( string[] lines )
		{
			TextFile textFile = new TextFile();

			textFile.SetLines( lines );

			return textFile.Write();
		}
	}
}