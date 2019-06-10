using System;
using System.Reflection;
using PokeRandomizer.Config.Abstract;
using PokeRandomizer.Utility;

namespace PokeRandomizer.Config
{
	[ AttributeUsage( AttributeTargets.Property ) ]
	class MinValueAttribute : Attribute
	{
		public MinValueAttribute( double minValue )
		{
			this.MinValue = minValue;
		}

		public double MinValue { get; }
	}

	[ AttributeUsage( AttributeTargets.Property ) ]
	class MaxValueAttribute : Attribute
	{
		public MaxValueAttribute( double maxValue )
		{
			this.MaxValue = maxValue;
		}

		public double MaxValue { get; }
	}

	static class Validator
	{
		public static void ValidateConfig( IConfig config ) => Validator.ValidateObject( config );

		private static void ValidateObject( object obj )
		{
			const BindingFlags SearchFlags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance;
			var objType = obj.GetType();

			foreach ( var prop in objType.GetProperties( SearchFlags ) )
			{
				string propStr = $"{prop.DeclaringType?.Name}.{prop.Name}";
				var val = prop.GetValue( obj );

				if ( val == null )
					throw new ArgumentNullException( $"Property {propStr} cannot have a null value" );

				var minAtt = prop.GetImplementedCustomAttribute<MinValueAttribute>();
				var maxAtt = prop.GetImplementedCustomAttribute<MaxValueAttribute>();

				if ( minAtt != null || maxAtt != null )
				{
					if ( !typeof( IConvertible ).IsAssignableFrom( prop.PropertyType ) )
						throw new InvalidOperationException( $"Property {propStr} has MinValueAttribute or MaxValueAttribute but does not implement IConvertible" );

					double doubleVal;

					try
					{
						doubleVal = Convert.ToDouble( val );
					}
					catch
					{
						throw new InvalidOperationException( $"Could not convert property {propStr}'s value to a double for min/max comparison" );
					}

					if ( minAtt != null && doubleVal < minAtt.MinValue )
						throw new ArgumentOutOfRangeException( prop.Name, $"Property {propStr}'s value ({val}) is below the specified minimum value of {minAtt.MinValue}" );

					if ( maxAtt != null && doubleVal > maxAtt.MaxValue )
						throw new ArgumentOutOfRangeException( prop.Name, $"Property {propStr}'s value ({val}) is above the specified maximum value of {maxAtt.MaxValue}" );
				}

				if ( val.GetType().IsClass )
					Validator.ValidateObject( val );
			}
		}
	}
}