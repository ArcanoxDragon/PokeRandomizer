using Microsoft.Extensions.Configuration;

namespace PokemonDataDump
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

		public static string RomPath    => Instance.GetValue<string>( "RomPath" );
		public static string OutputPath => Instance.GetValue<string>( "OutputPath" );
		public static string GameType   => Instance.GetValue<string>( "GameType" );
	}
}