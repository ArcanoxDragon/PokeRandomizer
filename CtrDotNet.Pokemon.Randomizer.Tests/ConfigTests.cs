using System;
using CtrDotNet.Pokemon.Randomization.Config;
using NUnit.Framework;

namespace CtrDotNet.Pokemon.Randomizer.Tests
{
	[ TestFixture ]
	public class ConfigTests
	{
		private readonly RandomizerConfig config;

		public ConfigTests()
		{
			this.config = new RandomizerConfig();
		}

		[ Test ]
		public void TestNullValue()
		{
			this.config.Abilities = null;

			Assert.Catch<ArgumentNullException>( () => Validator.ValidateConfig( this.config ), "Null value was not caught by the validator" );

			this.config.Abilities = new AbilitiesConfig();
		}

		[ Test ]
		public void TestValidate_Encounters_LevelMultiplier()
		{
			this.config.Encounters.LevelMultiplier = 0.4m;

			Assert.Catch<ArgumentOutOfRangeException>( () => Validator.ValidateConfig( this.config ), "Invalid value was not caught by validator" );

			this.config.Encounters.LevelMultiplier = RandomizerConfig.Default.Encounters.LevelMultiplier;
		}

		[ Test ]
		public void TestValidate_Trainers_LevelMultiplier()
		{
			this.config.Trainers.LevelMultiplier = 0.4m;

			Assert.Catch<ArgumentOutOfRangeException>( () => Validator.ValidateConfig( this.config ), "Invalid value was not caught by validator" );

			this.config.Trainers.LevelMultiplier = RandomizerConfig.Default.Encounters.LevelMultiplier;
		}

		[ Test ]
		public void TestValidate_Learnsets_LearnAllMovesBy()
		{
			this.config.Learnsets.LearnAllMovesBy = 5;

			Assert.Catch<ArgumentOutOfRangeException>( () => Validator.ValidateConfig( this.config ), "Invalid value was not caught by validator" );

			this.config.Learnsets.LearnAllMovesBy = 105;

			Assert.Catch<ArgumentOutOfRangeException>( () => Validator.ValidateConfig( this.config ), "Invalid value was not caught by validator" );

			this.config.Learnsets.LearnAllMovesBy = RandomizerConfig.Default.Learnsets.LearnAllMovesBy;
		}
	}
}