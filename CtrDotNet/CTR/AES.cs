using System;
using System.Linq;
using System.Security.Cryptography;

namespace CtrDotNet.CTR
{
	public class AesCtr
	{
		private readonly AesManaged aes;
		private readonly ICryptoTransform encryptor;
		private readonly AesCounter counter;

		private AesCtr( byte[] key )
		{
			this.aes = new AesManaged {
				Key = key,
				Mode = CipherMode.ECB,
				Padding = PaddingMode.None
			};
			this.encryptor = this.aes.CreateEncryptor();
		}

		public AesCtr( byte[] key, byte[] iv ) : this( key )
		{
			this.counter = new AesCounter( iv );
		}

		public AesCtr( byte[] key, ulong partitionID, ulong initialCount ) : this( key )
		{
			this.counter = new AesCounter( partitionID, initialCount );
		}

		public int TransformBlock( byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset )
		{
			int blockLength;
			for ( int i = 0; i < inputCount; i += blockLength )
			{
				blockLength = inputCount - i > AesCounter.BufferSize
								  ? AesCounter.BufferSize
								  : inputCount - i;
				this.encryptor.TransformBlock( this.counter.ManageBufferCounters( blockLength ), 0, blockLength, outputBuffer, outputOffset + i );
				for ( int blockWalker = i; blockWalker < i + blockLength; blockWalker += 8 )
				{
					Array.Copy( BitConverter.GetBytes( BitConverter.ToInt64( outputBuffer, outputOffset + blockWalker ) ^ BitConverter.ToInt64( inputBuffer, inputOffset + blockWalker ) ), 0, outputBuffer, outputOffset + blockWalker, 8 );
				}
			}
			return inputCount;
		}
	}

	public class AesCounter
	{
		public const int BufferSize = 0x400000; //4 MB Buffer
		private readonly byte[] counter = new byte[ 0x10 ];
		private readonly byte[] buffer = new byte[ BufferSize ];

		public AesCounter( ulong high, ulong low )
		{
			Array.Copy( BitConverter.GetBytes( high ).Reverse().ToArray(), this.counter, 0x8 );
			Array.Copy( BitConverter.GetBytes( low ).Reverse().ToArray(), 0, this.counter, 0x8, 0x8 );
		}

		public AesCounter( byte[] iv )
		{
			Array.Copy( BitConverter.GetBytes( BitConverter.ToUInt64( iv, 0 ) ).Reverse().ToArray(), this.counter, 0x10 );
		}

		public void Increment()
		{
			for ( int i = this.counter.Length - 1; i >= 0; i-- )
			{
				if ( ++this.counter[ i ] != 0 )
					return;
			}
		}

		public byte[] ManageBufferCounters( int size )
		{
			for ( int i = 0; i < size; i += 0x10 )
			{
				Array.Copy( this.counter, 0, this.buffer, i, 0x10 );
				this.Increment();
			}
			return this.buffer;
		}

		public ulong SwapBytes( ulong value )
		{
			ulong uvalue = value;
			ulong swapped =
				0x00000000000000FF & ( uvalue >> 56 )
				| 0x000000000000FF00 & ( uvalue >> 40 )
				| 0x0000000000FF0000 & ( uvalue >> 24 )
				| 0x00000000FF000000 & ( uvalue >> 8 )
				| 0x000000FF00000000 & ( uvalue << 8 )
				| 0x0000FF0000000000 & ( uvalue << 24 )
				| 0x00FF000000000000 & ( uvalue << 40 )
				| 0xFF00000000000000 & ( uvalue << 56 );
			return swapped;
		}
	}
}