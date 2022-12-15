/*----------------------------------------------------------------------------*/
/*--  blz.c - Bottom LZ coding for Nintendo GBA/DS                          --*/
/*--  Copyright (C) 2011 CUE                                                --*/
/*--                                                                        --*/
/*--  Ported to C# by Andi Badra, tweaks by Kaphotics                       --*/
/*--                                                                        --*/
/*--  This program is free software: you can redistribute it and/or modify  --*/
/*--  it under the terms of the GNU General Public License as published by  --*/
/*--  the Free Software Foundation, either version 3 of the License, or     --*/
/*--  (at your option) any later version.                                   --*/
/*--                                                                        --*/
/*--  This program is distributed in the hope that it will be useful,       --*/
/*--  but WITHOUT ANY WARRANTY; without even the implied warranty of        --*/
/*--  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the          --*/
/*--  GNU General Public License for more details.                          --*/
/*--                                                                        --*/
/*--  You should have received a copy of the GNU General Public License     --*/
/*--  along with this program. If not, see <http://www.gnu.org/licenses/>.  --*/
/*----------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CtrDotNet.CTR
{
	public class BlzCoder
	{
		private const int CmdDecode = 0;
		private const int CmdEncode = 1;

		private const int BlzNormal    = 0;
		private const int BlzBest      = 1;
		private const int BlzShift     = 1;
		private const int BlzMask      = 0x80;
		private const int BlzThreshold = 2;
		private const int BlzN         = 0x1002;
		private const int BlzF         = 0x12;
		private const int BlzMaxim     = 0x01400000;
		private const int RawMaxim     = 0x00FFFFFF;

		private readonly bool arm9;
		private          int  newLen;

		public BlzCoder(IReadOnlyList<string> args)
		{
			int cmd, mode = 0;

			// Title();

			if (args == null || ( args.Count != 2 ))
				throw new Exception("No arguments supplied to BLZ");

			if (args[0].Equals("-d"))
				cmd = CmdDecode;

			else if (args[0].Equals("-en") || args[0].Equals("-en9"))
			{
				cmd = CmdEncode;
				mode = BlzNormal;
			}
			else if (args[0].Equals("-eo") || args[0].Equals("-eo9"))
			{
				cmd = CmdEncode;
				mode = BlzBest;
			}
			else
			{
				Console.Write("Command not supported" + Environment.NewLine);
				return;
			}

			if (args.Count < 2)
				Console.Write("Filename not specified" + Environment.NewLine);
			else
			{
				int arg;
				switch (cmd)
				{
					case 0:
						for (arg = 1; arg < args.Count; arg++)
							BLZ_Decode(args[arg]);
						break;
					case 1:
						this.arm9 = args[0].Length > 3 && args[0][3] == '9';
						for (arg = 1; arg < args.Count; arg++)
							BLZ_Encode(args[arg], mode);
						break;
				}
			}

			Console.Write(Environment.NewLine + "Done" + Environment.NewLine);
		}

		private static void Save(string filename, byte[] buffer, int length)
		{
			Array.Resize(ref buffer, length);
			try
			{
				File.WriteAllBytes(filename, buffer);
			}
			catch (IOException e)
			{
				Console.Write(Environment.NewLine + "File write error" + Environment.NewLine + e + Environment.NewLine);
				File.WriteAllBytes("blz.bin", buffer);
				Console.Write(Environment.NewLine + "Wrote to 'blz.bin' instead." + Environment.NewLine);
			}
		}

		private static void BLZ_Decode(string filename)
		{
			try
			{
				Console.Write($"- decoding '{filename}'");
				long startTime = DateTime.Now.Millisecond;
				byte[] buf = File.ReadAllBytes(filename);
				BlzResult result = BLZ_Decode(buf);
				if (result != null)
					Save(filename, result.buffer, result.length);
				Console.Write(" - done, time="
							  + ( DateTime.Now.Millisecond - startTime ) + "ms");
				Console.Write(Environment.NewLine + "");
			}
			catch (IOException e)
			{
				Console.Write(Environment.NewLine + "File read error" + Environment.NewLine + e);
			}
		}

		private static BlzResult BLZ_Decode(byte[] data)
		{
			int rawLen, len;
			int encLen, decLen;
			int flags = 0;

			byte[] pakBuffer = PrepareData(data);
			int pakLen = pakBuffer.Length - 3;

			int incLen = BitConverter.ToInt32(pakBuffer, pakLen - 4);
			if (incLen < 1)
			{
				Console.Write(", WARNING: not coded file!");
				encLen = 0;
				decLen = pakLen;
				pakLen = 0;
				rawLen = decLen;
			}
			else
			{
				if (pakLen < 8)
				{
					Console.Write(Environment.NewLine + "File has a bad header" + Environment.NewLine);
					return null;
				}

				int hdrLen = pakBuffer[pakLen - 5];
				if (hdrLen < 8 || hdrLen > 0xB)
				{
					Console.Write(Environment.NewLine + "Bad header length" + Environment.NewLine);
					return null;
				}

				if (pakLen <= hdrLen)
				{
					Console.Write(Environment.NewLine + "Bad length" + Environment.NewLine);
					return null;
				}

				encLen = (int) ( BitConverter.ToUInt32(pakBuffer, pakLen - 8) & 0x00FFFFFF );
				decLen = pakLen - encLen;
				pakLen = encLen - hdrLen;
				rawLen = decLen + encLen + incLen;
				if (rawLen > RawMaxim)
				{
					Console.Write(Environment.NewLine + "Bad decoded length" + Environment.NewLine);
					return null;
				}
			}

			byte[] rawBuffer = new byte[rawLen];

			int pak = 0;
			int raw = 0;
			int pakEnd = decLen + pakLen;
			int rawEnd = rawLen;

			for (len = 0; len < decLen; len++)
				rawBuffer[raw++] = pakBuffer[pak++];

			BLZ_Invert(pakBuffer, decLen, pakLen);

			int mask = 0;

			while (raw < rawEnd)
			{
				if (( mask = (int) ( (uint) mask >> BlzShift ) ) == 0)
				{
					if (pak == pakEnd)
						break;

					flags = pakBuffer[pak++];
					mask = BlzMask;
				}

				if (( flags & mask ) == 0)
				{
					if (pak == pakEnd)
						break;

					rawBuffer[raw++] = pakBuffer[pak++];
				}
				else
				{
					if (pak + 1 >= pakEnd)
						break;

					int pos = pakBuffer[pak++] << 8;
					pos |= pakBuffer[pak++];
					len = (int) ( (uint) pos >> 12 ) + BlzThreshold + 1;
					if (raw + len > rawEnd)
					{
						Console.Write(", WARNING: wrong decoded length!");
						len = rawEnd - raw;
					}

					pos = ( pos & 0xFFF ) + 3;
					while (len-- > 0)
					{
						int charHere = rawBuffer[raw - pos];
						rawBuffer[raw++] = (byte) charHere;
					}
				}
			}

			BLZ_Invert(rawBuffer, decLen, rawLen - decLen);

			rawLen = raw;

			if (raw != rawEnd)
				Console.Write(", WARNING: unexpected end of encoded file!");

			return new BlzResult(rawBuffer, rawLen);
		}

		private BlzResult BLZ_Encode(byte[] data, int mode)
		{
			this.newLen = 0;

			byte[] rawBuffer = PrepareData(data);
			int rawLen = rawBuffer.Length - 3;

			byte[] pakBuffer = null;
			int pakLen = BlzMaxim + 1;

			byte[] newBuffer = BLZ_Code(rawBuffer, rawLen, mode);

			if (this.newLen < pakLen)
			{
				pakBuffer = newBuffer;
				pakLen = this.newLen;
			}

			return new BlzResult(pakBuffer, pakLen);
		}

		private static byte[] PrepareData(byte[] data)
		{
			int fs = data.Length;
			byte[] fb = new byte[fs + 3];
			for (int i = 0; i < fs; i++)
				fb[i] = data[i];

			return fb;
		}

		private static void WriteUnsigned(byte[] buffer, int offset, int value)
		{
			buffer[offset] = (byte) ( value & 0xFF );
			buffer[offset + 1] = (byte) ( ( value >> 8 ) & 0xFF );
			buffer[offset + 2] = (byte) ( ( value >> 16 ) & 0xFF );
			buffer[offset + 3] = (byte) ( ( value >> 24 ) & 0x7F );
		}

		private void BLZ_Encode(string filename, int mode)
		{
			try
			{
				Console.Write("Now encoding {0}", filename);
				var stopwatch = new Stopwatch();
				stopwatch.Start();

				byte[] buf = File.ReadAllBytes(filename);
				BlzResult result = BLZ_Encode(buf, mode);
				if (result != null)
					Save(filename, result.buffer, result.length);

				stopwatch.Stop();
				Console.Write(Environment.NewLine + "Done, time elapsed = " + stopwatch.ElapsedMilliseconds + "ms" + Environment.NewLine);
			}
			catch (IOException e)
			{
				Console.Write(Environment.NewLine + "File read error" + Environment.NewLine + e + Environment.NewLine);
			}
		}

		private byte[] BLZ_Code(byte[] rawBuffer, int rawLen, int best)
		{
			int flg = 0;
			int posBest = 0;
			int posNext = 0;
			int posPost = 0;

			int pakTmp = 0;
			int rawTmp = rawLen;

			int pakLen = rawLen + ( rawLen + 7 ) / 8 + 11;
			byte[] pakBuffer = new byte[pakLen];

			int rawNew = rawLen;

			// We don't do any of the checks here
			// Presume that we actually are using an arm9
			if (this.arm9)
				rawNew -= 0x4000;

			BLZ_Invert(rawBuffer, 0, rawLen);

			int pak = 0;
			int raw = 0;
			int rawEnd = rawNew;

			int mask = 0;
			while (raw < rawEnd)
			{
				if (( mask = (int) ( (uint) mask >> BlzShift ) ) == 0)
				{
					pakBuffer[flg = pak++] = 0;
					mask = BlzMask;
				}

				SearchPair sl1 = Search(posBest, rawBuffer, raw, rawEnd);
				int lenBest = sl1.l;
				posBest = sl1.p;

				// LZ-CUE optimization start
				if (best == BlzBest)
				{
					if (lenBest > BlzThreshold)
					{
						if (raw + lenBest < rawEnd)
						{
							raw += lenBest;
							SearchPair sl2 = Search(posNext, rawBuffer, raw,
															 rawEnd);
							int lenNext = sl2.l;
							posNext = sl2.p;
							raw -= lenBest - 1;
							SearchPair sl3 = Search(posPost, rawBuffer, raw,
															 rawEnd);
							int lenPost = sl3.l;
							posPost = sl3.p;
							raw--;

							if (lenNext <= BlzThreshold)
								lenNext = 1;
							if (lenPost <= BlzThreshold)
								lenPost = 1;
							if (lenBest + lenNext <= 1 + lenPost)
								lenBest = 1;
						}
					}
				}

				// LZ-CUE optimization end
				pakBuffer[flg] = (byte) ( pakBuffer[flg] << 1 );
				if (lenBest > BlzThreshold)
				{
					raw += lenBest;
					pakBuffer[flg] |= 1;
					pakBuffer[pak++] = (byte) ( (byte) ( ( lenBest - ( BlzThreshold + 1 ) ) << 4 ) | ( (uint) ( posBest - 3 ) >> 8 ) );
					pakBuffer[pak++] = (byte) ( posBest - 3 );
				}
				else
					pakBuffer[pak++] = rawBuffer[raw++];

				if (pak + rawLen - raw >= pakTmp + rawTmp)
					continue;

				pakTmp = pak;
				rawTmp = rawLen - raw;
			}

			while (( mask > 0 ) && ( mask != 1 ))
			{
				mask = (int) ( (uint) mask >> BlzShift );
				pakBuffer[flg] = (byte) ( pakBuffer[flg] << 1 );
			}

			pakLen = pak;

			BLZ_Invert(rawBuffer, 0, rawLen);
			BLZ_Invert(pakBuffer, 0, pakLen);

			if (pakTmp == 0 || ( rawLen + 4 < ( ( pakTmp + rawTmp + 3 ) & 0xFFFFFFFC ) + 8 ))
			{
				pak = 0;
				raw = 0;
				rawEnd = rawLen;

				while (raw < rawEnd)
					pakBuffer[pak++] = rawBuffer[raw++];

				while (( pak & 3 ) > 0)
					pakBuffer[pak++] = 0;

				pakBuffer[pak++] = 0;
				pakBuffer[pak++] = 0;
				pakBuffer[pak++] = 0;
				pakBuffer[pak++] = 0;
			}
			else
			{
				byte[] tmp = new byte[rawTmp + pakTmp + 11];

				int len;
				for (len = 0; len < rawTmp; len++)
					tmp[len] = rawBuffer[len];

				for (len = 0; len < pakTmp; len++)
					tmp[rawTmp + len] = pakBuffer[len + pakLen - pakTmp];

				pakBuffer = tmp;
				pak = rawTmp + pakTmp;

				int encLen = pakTmp;
				int hdrLen = 8;
				int incLen = rawLen - pakTmp - rawTmp;

				while (( pak & 3 ) > 0)
				{
					pakBuffer[pak++] = 0xFF;
					hdrLen++;
				}

				WriteUnsigned(pakBuffer, pak, encLen + hdrLen);
				pak += 3;
				pakBuffer[pak++] = (byte) hdrLen;
				WriteUnsigned(pakBuffer, pak, incLen - hdrLen);
				pak += 4;
			}

			this.newLen = pak;
			return pakBuffer;
		}

		private class SearchPair
		{
			public readonly int l;
			public readonly int p;

			public SearchPair(int l, int p)
			{
				this.l = l;
				this.p = p;
			}
		}

		private static SearchPair Search(int p, IList<byte> rawBuffer, int raw, int rawEnd)
		{
			int l = BlzThreshold;
			int max = raw >= BlzN
						  ? BlzN
						  : raw;
			for (int pos = 3; pos <= max; pos++)
			{
				int len;
				for (len = 0; len < BlzF; len++)
				{
					if (raw + len == rawEnd)
						break;

					if (len >= pos)
						break;

					if (rawBuffer[raw + len] != rawBuffer[raw + len - pos])
						break;
				}

				if (len <= l)
					continue;
				p = pos;
				if (( l = len ) == BlzF)
					break;
			}

			return new SearchPair(l, p);
		}

		private class BlzResult
		{
			public BlzResult(byte[] rawBuffer, int rawLen)
			{
				this.buffer = rawBuffer;
				this.length = rawLen;
			}

			public readonly byte[] buffer;
			public readonly int    length;
		}

		private static void BLZ_Invert(byte[] buffer, int offset, int length)
		{
			int bottom = offset + length - 1;

			while (offset < bottom)
			{
				int ch = buffer[offset];
				buffer[offset++] = buffer[bottom];
				buffer[bottom--] = (byte) ch;
			}
		}
	}
}