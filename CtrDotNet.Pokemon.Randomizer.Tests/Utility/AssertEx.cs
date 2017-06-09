using System;
using System.Reflection;
using NUnit.Framework;

namespace CtrDotNet.Pokemon.Randomizer.Tests.Utility
{
	public static class AssertEx
	{
		public static void PropertiesAreEqual( object expected, object actual, string format, params object[] args )
			=> Do_PropertiesAreEqual( expected, actual, false, format, args );

		public static void DeepPropertiesAreEqual( object expected, object actual, string format, params object[] args )
			=> Do_PropertiesAreEqual( expected, actual, true, format, args );

		private static void Do_PropertiesAreEqual( object expected, object actual, bool deep, string format, params object[] args )
		{
			const BindingFlags SearchFlags = BindingFlags.Instance | BindingFlags.Public;

			if ( expected == null )
				throw new ArgumentNullException( nameof(expected), "Expected value cannot be null" );

			if ( actual == null )
				throw new AssertionException( string.Format( format, args ), new AssertionException( "Value is null" ) );

			if ( expected.GetType() != actual.GetType() )
				throw new ArgumentException( "Expected and actual objects must be of the same type" );

			foreach ( var prop in expected.GetType().GetProperties( SearchFlags ) )
			{
				object expectedValue = prop.GetValue( expected );
				object actualValue = prop.GetValue( actual );

				if ( deep && !expectedValue.GetType().IsPrimitive )
				{
					AssertEx.Do_PropertiesAreEqual( expectedValue, actualValue, true, format, args );
				}
				else
				{
					try
					{
						Assert.AreEqual( expectedValue, actualValue, $"Property {expected.GetType().Name}.{prop.Name} was not equal between the expected and actual objects" );
					}
					catch ( AssertionException e )
					{
						throw new AssertionException( string.Format( format, args ), e );
					}
				}
			}
		}
	}
}