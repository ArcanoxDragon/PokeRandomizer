using System;

namespace CtrDotNet.Pokemon.Randomization.Utility
{
	public class MathUtil
	{
		public static int Clamp( int value, int min, int max )
			=> Math.Min( max, Math.Max( min, value ) );

		public static double Clamp( double value, double min, double max )
			=> Math.Min( max, Math.Max( min, value ) );
	}
}