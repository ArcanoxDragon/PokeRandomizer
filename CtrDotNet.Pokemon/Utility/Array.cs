using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CtrDotNet.Pokemon.Utility
{
	public static class ArrayExtensions
	{
		public static T[][] Partition<T>( this T[] array, int partitionSize )
		{
			if ( array.Length % partitionSize != 0 )
				throw new ArgumentOutOfRangeException( nameof(array), "Array size must be evenly divisible by partition size" );

			int numPartitions = array.Length / partitionSize;
			T[][] partitions = new T[ numPartitions ][];

			for ( int i = 0; i < numPartitions; i++ )
				partitions[ i ] = array.Skip( i * partitionSize ).Take( partitionSize ).ToArray();

			return partitions;
		}

		public static void Fill<T>( this T[] array, Func<T> factory )
		{
			for ( int i = 0; i < array.Length; i++ )
			{
				array[ i ] = factory();
			}
		}

		public static void Fill<T>( this T[] array, T value ) => array.Fill( () => value );

		public static IEnumerable<int> FindSequence<T>( this T[] collection, T[] pattern, int startIndex = 0 ) where T : IEquatable<T>
		{
			IEnumerable<int> index = Enumerable.Range( startIndex, collection.Length - startIndex - pattern.Length + 1 );

			return pattern.Select( ( t, i ) => i )
						  .Aggregate( index, ( current, i ) => current.Where( n => collection[ n + i ].Equals( pattern[ i ] ) ) );
		}

		public static void WithReader( this byte[] array, Action<BinaryReader> action )
		{
			using ( var ms = new MemoryStream( array ) )
			using ( var br = new BinaryReader( ms ) )
				action( br );
		}

		public static void WithWriter( this byte[] array, Action<BinaryWriter> action )
		{
			using ( var ms = new MemoryStream( array ) )
			using ( var bw = new BinaryWriter( ms ) )
				action( bw );
		}
	}
}