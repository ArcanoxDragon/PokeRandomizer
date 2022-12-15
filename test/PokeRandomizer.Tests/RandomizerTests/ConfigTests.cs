using System;
using System.Reflection;
using NUnit.Framework;
using PokeRandomizer.Config;
using PokeRandomizer.Tests.Utility;

namespace PokeRandomizer.Tests.RandomizerTests
{
	[TestFixture]
	public class ConfigTests
	{
		private readonly RandomizerConfig config;

		public ConfigTests()
		{
			this.config = new RandomizerConfig();
		}

		[Test]
		public void TestNullValue()
		{
			this.config.PokemonInfo = null;

			Assert.Catch<ArgumentNullException>(() => Validator.ValidateConfig(this.config), "Null value was not caught by the validator");

			this.config.PokemonInfo = new PokemonInfoConfig();
		}

		[Test]
		public void TestValidate_Encounters_LevelMultiplier()
		{
			this.config.Encounters.LevelMultiplier = 0.4m;

			Assert.Catch<ArgumentOutOfRangeException>(() => Validator.ValidateConfig(this.config), "Invalid value was not caught by validator");

			this.config.Encounters.LevelMultiplier = RandomizerConfig.Default.Encounters.LevelMultiplier;
		}

		[Test]
		public void TestValidate_Trainers_LevelMultiplier()
		{
			this.config.Trainers.LevelMultiplier = 0.4m;

			Assert.Catch<ArgumentOutOfRangeException>(() => Validator.ValidateConfig(this.config), "Invalid value was not caught by validator");

			this.config.Trainers.LevelMultiplier = RandomizerConfig.Default.Encounters.LevelMultiplier;
		}

		[Test]
		public void TestValidate_Learnsets_LearnAllMovesBy()
		{
			this.config.Learnsets.LearnAllMovesBy = 5;

			Assert.Catch<ArgumentOutOfRangeException>(() => Validator.ValidateConfig(this.config), "Invalid value was not caught by validator");

			this.config.Learnsets.LearnAllMovesBy = 105;

			Assert.Catch<ArgumentOutOfRangeException>(() => Validator.ValidateConfig(this.config), "Invalid value was not caught by validator");

			this.config.Learnsets.LearnAllMovesBy = RandomizerConfig.Default.Learnsets.LearnAllMovesBy;
		}

		[Test]
		public void TestConfigCloning()
		{
			var original = new RandomizerConfig();

			// Change everything from the default
			void ChangeProps(object obj)
			{
				foreach (var prop in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly))
				{
					var value = prop.GetValue(obj);

					switch (value)
					{
						case bool b:
							prop.SetValue(obj, !b);
							break;
						case decimal d:
							prop.SetValue(obj, d + 0.1m);
							break;
						case var o when !value.GetType().IsPrimitive:
							ChangeProps(o);
							break;
					}
				}
			}

			ChangeProps(original);

			var cloned = original.AsEditable();

			Assert.AreNotSame(original, cloned, "Cloned config should not be the same instance as the source!");
			AssertEx.DeepPropertiesAreEqual(original, cloned, "Clone config's properties were not equal to source!");
		}
	}
}