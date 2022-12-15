using System;
using System.IO;
using System.Linq;

namespace PokeRandomizer.Common.Utility
{
	public class ArrayStream<T>
	{
		private readonly T[] array;

		public ArrayStream(T[] array)
		{
			this.array = array;
		}

		public int Position { get; private set; }
		public int Length   => this.array.Length;

		public void Seek(int position, SeekOrigin origin)
		{
			int newPosition = -1;

			switch (origin)
			{
				case SeekOrigin.Begin:
					newPosition = position;
					break;
				case SeekOrigin.Current:
					newPosition = Position + position;
					break;
				case SeekOrigin.End:
					newPosition = this.array.Length + position;
					break;
			}

			AssertStart(newPosition, "seek");
			AssertEnd(newPosition, "seek");

			Position = newPosition;
		}

		public T[] Read(int count)
		{
			AssertEnd(Position + count - 1, "read");
			T[] ret = this.array.Skip(Position).Take(count).ToArray();
			Position += count;

			return ret;
		}

		public void Write(T[] data)
		{
			AssertEnd(Position + data.Length - 1, "write");

			data.CopyTo(this.array, Position);

			Position += data.Length;
		}

		private void AssertStart(int position, string verb)
		{
			if (position < 0)
				// ReSharper disable once NotResolvedInText
				throw new ArgumentOutOfRangeException(nameof(position), $"Cannot {verb} before the start of the array");
		}

		private void AssertEnd(int position, string verb)
		{
			if (position >= this.array.Length)
				throw new ArgumentOutOfRangeException(nameof(position), $"Cannot {verb} past the end of the array");
		}
	}
}