using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.WindowsAPICodePack.Dialogs;
using PokeRandomizer.Common.Game;
using MessageBox = System.Windows.MessageBox;

namespace PokeRandomizer.UI
{
	public partial class StartupWindow
	{
		public StartupWindow()
		{
			this.InitializeComponent();
		}

		private void BrowseButton_Click( object sender, RoutedEventArgs e )
		{
			var selLanguage = this.ListLanguage.SelectedItem as ComboBoxItem;

			if ( selLanguage == null )
			{
				MessageBox.Show( this,
								 "The language selected is not valid",
								 "Invalid Language",
								 MessageBoxButton.OK,
								 MessageBoxImage.Error );
				return;
			}

			var language = (Language) selLanguage.Tag;
			var dialog = new CommonOpenFileDialog {
				IsFolderPicker   = true,
				EnsurePathExists = true,
				Title            = "Pick game folder",
				CookieIdentifier = App.FileDialogCookieRomPath
			};

			var result = dialog.ShowDialog( this );

			if ( result == CommonFileDialogResult.Ok )
			{
				var fullPath = Path.GetFullPath( dialog.FileName );
				var romFS    = Path.Combine( fullPath, "RomFS" );
				var exeFS    = Path.Combine( fullPath, "ExeFS" );

				if ( !Directory.Exists( romFS ) || !Directory.Exists( exeFS ) )
				{
					MessageBox.Show( this,
									 "Game directory must contain both an ExeFS and a RomFS folder",
									 "Invalid Game Folder",
									 MessageBoxButton.OK,
									 MessageBoxImage.Error );
					return;
				}

				var window = new MainWindow( language, fullPath );
				this.Hide();
				window.Show();
			}
		}

		private void Hyperlink_OnRequestNavigate( object sender, RequestNavigateEventArgs e )
		{
			var startInfo = new ProcessStartInfo( e.Uri.AbsoluteUri );

			Process.Start( startInfo );
			e.Handled = true;
		}
	}
}