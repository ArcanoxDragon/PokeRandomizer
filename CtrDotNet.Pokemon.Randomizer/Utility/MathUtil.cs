﻿using System;

namespace CtrDotNet.Pokemon.Randomization.Utility
{
    public class MathUtil
    {
	    public static int Clamp( int value, int min, int max )
	    {
		    return Math.Min( max, Math.Max( min, value ) );
	    }
    }
}