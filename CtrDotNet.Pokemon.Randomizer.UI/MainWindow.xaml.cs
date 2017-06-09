using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Randomization.Common;
using CtrDotNet.Pokemon.Randomization.Config;
using CtrDotNet.Pokemon.Randomization.UI.Pages;

namespace CtrDotNet.Pokemon.Randomization.UI
{
	public partial class MainWindow
	{
		private readonly Language gameLanguage;
		private readonly string gameDirectory;
		private readonly RandomizerConfig config;
		private GameConfig game;
		private IRandomizer randomizer;
		private RandomizerPage mainPage;

		public MainWindow( Language gameLanguage, string gameDirectory )
		{
			this.InitializeComponent();

			this.gameLanguage = gameLanguage;
			this.gameDirectory = gameDirectory;
			this.config = new RandomizerConfig();
		}

		private async void Window_Loaded( object sender, RoutedEventArgs e )
		{
			try
			{
				string garcPath = Path.Combine( this.gameDirectory, "RomFS", "a" );
				int garcCount = Directory.GetFiles( garcPath, "*", SearchOption.AllDirectories ).Length;
				this.game = new GameConfig( garcCount );
				this.randomizer = Randomizer.GetRandomizer( this.game, this.config );

				await this.game.Initialize( this.gameDirectory, this.gameLanguage );

				this.mainPage = new RandomizerPage( this.randomizer );
				this.Content = this.mainPage;
			}
			catch ( Exception ex )
			{
				MessageBox.Show( this,
								 $"An error occurred while loading the game:\n\n" +
								 $"{ex.Message}",
								 "Error Loading Game",
								 MessageBoxButton.OK,
								 MessageBoxImage.Error );
				this.Close();
				Application.Current.MainWindow.Show();
			}
		}

		private void Window_Closed( object sender, EventArgs e )
		{
			Application.Current.MainWindow.Show();
		}
	}
}