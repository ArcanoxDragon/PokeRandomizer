using Microsoft.Extensions.Configuration;

namespace PokeRandomizer.Tests
{
	public static class Settings
	{
		static Settings()
		{
			Instance = new ConfigurationBuilder()
					   .AddJsonFile( "appsettings.json",       optional: false )
					   .AddJsonFile( "appsettings.Local.json", optional: true )
					   .Build();
		}

		public static IConfiguration Instance { get; }

		public static string RomPathOras => Instance.GetValue<string>( "RomPathOras" );
	}
}