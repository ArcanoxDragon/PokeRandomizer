using System;
using System.IO;
using System.Linq;

namespace CtrDotNet.Pokemon.Utility
{
	public class ArrayStream<T>
	{
		private readonly T[] array;

		public ArrayStream( T[] array )
		{
			this.array = array;
		}

		public int Position { get; private set; }
		public int Length => this.array.Length;

		public void Seek( int position, SeekOrigin origin )
		{
			int newPosition = -1;

			switch ( origin )
			{
				case SeekOrigin.Begin:
					newPosition = position;
					break;
				case SeekOrigin.Current:
					newPosition = this.Position + position;
					break;
				case SeekOrigin.End:
					newPosition = this.array.Length + position;
					break;
			}

			this.AssertStart( newPosition, "seek" );
			this.AssertEnd( newPosition, "seek" );

			this.Position = newPosition;
		}

		public T[] Read( int count )
		{
			this.AssertEnd( this.Position + count, "read" );
			T[] ret = this.array.Skip( this.Position ).Take( count ).ToArray();
			this.Position += count;

			return ret;
		}

		public void Write( T[] data )
		{
			this.AssertEnd( this.Position + data.Length, "write" );

			data.CopyTo( this.array, this.Position );

			this.Position += data.Length;
		}

		private void AssertStart( int position, string verb )
		{
			if ( position < 0 )
				// ReSharper disable once NotResolvedInText
				throw new ArgumentOutOfRangeException( nameof(position), $"Cannot {verb} before the start of the array" );
		}

		private void AssertEnd( int position, string verb )
		{
			if ( position >= this.array.Length )
				throw new ArgumentOutOfRangeException( nameof(position), $"Cannot {verb} past the end of the array" );
		}
	}
}