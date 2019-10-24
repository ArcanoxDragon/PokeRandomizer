using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using PokeRandomizer.Common;
using PokeRandomizer.Common.Utility;
using PokeRandomizer.Config;
using PokeRandomizer.Progress;
using PokeRandomizer.UI.Controls;
using PokeRandomizer.UI.Properties;
using PokeRandomizer.UI.Utility;
using PokeRandomizer.Utility;

namespace PokeRandomizer.UI.Pages
{
	public partial class RandomizerPage : INotifyPropertyChanged
	{
		private readonly List<UIElement> hintElements;

		private string outputPath;
		private string defaultHintText;
		private int    lastTab = -1;
		private int?   seed;

		public RandomizerPage( IRandomizer randomizer )
		{
			this.hintElements = new List<UIElement>();

			this.Randomizer  = randomizer;
			this.DataContext = this.Randomizer;

			this.InitializeComponent();
		}

		public IRandomizer      Randomizer { get; }
		public RandomizerConfig Config     => this.Randomizer.Config;

		public string OutputPath
		{
			get => this.outputPath;
			private set
			{
				this.outputPath = value;
				this.OnPropertyChanged();
				this.OnPropertyChanged( nameof(this.OutputPathDisplay) );
				this.OnPropertyChanged( nameof(this.CanRandomize) );
			}
		}

		public int? Seed
		{
			get => this.seed;
			private set
			{
				this.seed = value;
				this.OnPropertyChanged();
				this.OnPropertyChanged( nameof(this.SeedDisplay) );
			}
		}

		public string OutputPathDisplay => this.OutputPath ?? "None";
		public bool   CanRandomize      => !string.IsNullOrEmpty( this.OutputPath );
		public string SeedDisplay       => this.Seed?.ToString() ?? "Automatic";

		private void RefreshHintElements()
		{
			// Remove listeners for existing elements so we don't end up with duplicates
			foreach ( var element in this.hintElements )
			{
				element.MouseEnter -= this.OnControlMouseEnter;
				element.MouseLeave -= this.OnControlMouseLeave;
			}

			this.hintElements.Clear();

			// Find all children with hint text properties and hook their mouseover behavior
			foreach ( var child in from child in this.GetVisualChildren<UIElement>()
								   let hintText = child.GetValue( DependencyProperties.Properties.HintTextProperty ) as string
								   where !string.IsNullOrEmpty( hintText )
								   select child )
			{
				child.MouseEnter += this.OnControlMouseEnter;
				child.MouseLeave += this.OnControlMouseLeave;
				this.hintElements.Add( child );
			}
		}

		private void MainPage_Loaded( object sender, RoutedEventArgs e )
		{
			this.defaultHintText = this.HintBox.Text;
		}

		private void OnControlMouseEnter( object sender, MouseEventArgs e )
		{
			if ( !( sender is DependencyObject ) )
				return;

			var dep = (DependencyObject) sender;
			string hintText = dep.GetValue( DependencyProperties.Properties.HintTextProperty ) as string
							  ?? this.defaultHintText;

			hintText = Regex.Replace( hintText, @"\s*\\n\s*", "\n" );

			this.HintBox.Text = hintText;
		}

		private void OnControlMouseLeave( object sender, MouseEventArgs mouseEventArgs )
		{
			this.HintBox.Text = this.defaultHintText;
		}

		private void Tabs_LayoutUpdated( object sender, System.EventArgs e )
		{
			if ( this.Tabs.SelectedIndex != this.lastTab )
			{
				this.lastTab = this.Tabs.SelectedIndex;
				this.RefreshHintElements();
			}
		}

		private async void LoadConfigFile_Click( object sender, RoutedEventArgs e )
		{
			var dialog = new CommonOpenFileDialog {
				EnsureFileExists = true,
				Filters          = { new CommonFileDialogFilter( "JSON Files (.json)", ".json" ) },
				CookieIdentifier = App.FileDialogCookieConfigFile
			};
			var result = dialog.ShowDialog( this.Parent as Window );

			if ( result == CommonFileDialogResult.Ok )
			{
				try
				{
					using ( var fs = new FileStream( dialog.FileName, FileMode.Open, FileAccess.Read, FileShare.None ) )
					using ( var sr = new StreamReader( fs ) )
					{
						string configContents = await sr.ReadToEndAsync();
						this.Randomizer.Config = JsonConvert.DeserializeObject<RandomizerConfig>( configContents );
					}

					// Refresh config in UI
					this.DataContext = null;
					this.DataContext = this.Randomizer;
				}
				catch ( Exception ex )
				{
					MessageBox.Show( this.Parent as Window,
									 $"An error occurred loading the config file:\n\n" +
									 $"{ex.Message}",
									 "Error Loading Config",
									 MessageBoxButton.OK,
									 MessageBoxImage.Error );
				}
			}
		}

		private async void SaveConfigFile_Click( object sender, RoutedEventArgs e )
		{
			var dialog = new CommonSaveFileDialog {
				AlwaysAppendDefaultExtension = true,
				OverwritePrompt              = true,
				Filters                      = { new CommonFileDialogFilter( "JSON Files (.json)", ".json" ) },
				DefaultExtension             = ".json",
				CookieIdentifier             = App.FileDialogCookieConfigFile
			};
			var result = dialog.ShowDialog( this.Parent as Window );

			if ( result == CommonFileDialogResult.Ok )
			{
				try
				{
					using ( var fs = new FileStream( dialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None ) )
					using ( var sw = new StreamWriter( fs ) )
					{
						string json = JsonConvert.SerializeObject( this.Randomizer.Config, Formatting.Indented );
						await sw.WriteLineAsync( json );
					}
				}
				catch ( Exception ex )
				{
					MessageBox.Show( this.Parent as Window,
									 $"An error occurred saving the config file:\n\n" +
									 $"{ex.Message}",
									 "Error Saving Config",
									 MessageBoxButton.OK,
									 MessageBoxImage.Error );
				}
			}
		}

		private void ResetConfig_Click( object sender, RoutedEventArgs e )
		{
			if ( MessageBox.Show( this.Parent as Window,
								  "Are you sure you want to reset the settings back to default?\n\n" +
								  "All unsaved changes will be lost.",
								  "Reset Settings",
								  MessageBoxButton.YesNo,
								  MessageBoxImage.Question ) == MessageBoxResult.Yes )
			{
				this.Randomizer.Config = RandomizerConfig.Default.AsEditable();

				// Refresh config in UI
				this.DataContext = null;
				this.DataContext = this.Randomizer;
			}
		}

		private void SetOutputPath_Click( object sender, RoutedEventArgs e )
		{
			var dialog = new CommonOpenFileDialog {
				IsFolderPicker   = true,
				EnsurePathExists = true,
				CookieIdentifier = App.FileDialogCookieOutputFolder
			};
			var result = dialog.ShowDialog( this.Parent as Window );

			if ( result == CommonFileDialogResult.Ok )
			{
				this.OutputPath = dialog.FileName;
			}
		}

		private void SetSeed_Click( object sender, RoutedEventArgs e )
		{
			var promptDialog = new PromptDialog {
				Owner         = this.Parent as Window,
				Message       = "Enter a custom numeric seed value, or click Cancel to use an automatic seed",
				ValueRequired = true
			};

			// Only valid if it's a number
			promptDialog.Validate += ( o, args ) => args.IsValid = int.TryParse( promptDialog.Text, out _ );

			if ( promptDialog.ShowDialog() == true )
			{
				this.Seed = int.Parse( promptDialog.Text );
			}
			else
			{
				this.Seed = null;
			}
		}

		private void Randomize_Click( object sender, RoutedEventArgs e )
		{
			if ( this.Dispatcher == null )
				throw new ApplicationException( "Fatal error" );

			var shouldLogResult = MessageBox.Show( this.Parent as Window,
												   "Should a randomizer log file be created in the selected output path? " +
												   "The log file will contain a record of all randomizations performed, " +
												   "which may provide an unfair advantage to players who are participating " +
												   "in a random race. The log will also contain the randomizer seed being " +
												   "used, which will allow the same randomization to be performed at a later " +
												   "time, provided the same options are used.",
												   "Create Log File",
												   MessageBoxButton.YesNoCancel );

			if ( shouldLogResult == MessageBoxResult.Cancel )
				return;

			var logFilePath   = default( string );
			var logger        = default( FileLogger );
			var createLogFile = shouldLogResult == MessageBoxResult.Yes;
			var progressBar   = new TaskDialogProgressBar( 0, 100, 0 );
			var dialog = new TaskDialog {
				Cancelable        = false,
				Caption           = "Randomizing Game",
				ExpansionMode     = TaskDialogExpandedDetailsLocation.Hide,
				Icon              = TaskDialogStandardIcon.Information,
				InstructionText   = "Randomizing game...this may take a few minutes",
				ProgressBar       = progressBar,
				OwnerWindowHandle = new WindowInteropHelper( this.Parent as Window ).Handle,
				StartupLocation   = TaskDialogStartupLocation.CenterOwner,
				StandardButtons   = TaskDialogStandardButtons.Cancel
			};

			var cancelSource = new CancellationTokenSource();

			dialog.Closing += ( o, ev ) => {
				if ( ev.TaskDialogResult == TaskDialogResult.Cancel )
				{
					ev.Cancel = true;
					cancelSource.Cancel();
					progressBar.State = TaskDialogProgressBarState.Marquee;
					dialog.Text       = "Cancelling...";
				}
			};

			dialog.Opened += ( o, args ) => ThreadPool.QueueUserWorkItem( async _ => {
				this.Randomizer.Game.OutputPathOverride = this.OutputPath;

				if ( this.Seed.HasValue )
					this.Randomizer.Reseed( this.Seed.Value );
				else
					this.Randomizer.Reseed();

				if ( createLogFile )
				{
					logFilePath            = Path.Combine( this.OutputPath, "RandomizerLog.txt" );
					logger                 = new FileLogger( logFilePath );
					this.Randomizer.Logger = logger;
				}
				else
				{
					this.Randomizer.Logger = null;
				}

				var progress = new ProgressNotifier();
				var minLines = 0;

				progress.ProgressUpdated += ( notifier, update ) => this.Dispatcher.Invoke( () => {
					if ( update.Type == ProgressUpdate.UpdateType.Status )
					{
						var status   = update.Status ?? progress.Status;
						var numLines = status.Split( '\n' ).Length;

						if ( numLines > minLines )
							minLines = numLines;

						if ( numLines < minLines )
							status += "\n".Repeat( minLines - numLines ); // keep the text from shrinking and causing the dialog to spaz

						progressBar.Value = (int) ( progress.Progress * 100.0 );
						dialog.Text       = status;
					}
				} );

				try
				{
					await this.Randomizer.RandomizeAll( progress, cancelSource.Token );
				}
				catch ( Exception ex )
				{
					await this.Dispatcher.InvokeAsync(
						() => MessageBox.Show( this.Parent as Window,
											   $"An error occurred while randomizing the game:\n\n" +
											   $"{ex.Message}",
											   "Randomization Error",
											   MessageBoxButton.OK,
											   MessageBoxImage.Error ) );
					dialog.Close( TaskDialogResult.Ok );
					return;
				}
				finally
				{
					logger?.Dispose();
				}

				dialog.Close( TaskDialogResult.Ok );

				if ( progress.IsComplete )
				{
					await this.Dispatcher.InvokeAsync(
						() => MessageBox.Show( this.Parent as Window,
											   $"Game patch files were successfully saved to:\n\n{this.OutputPath}." +
											   ( createLogFile ? $"\n\nLog file was saved to:\n\n{logFilePath}." : string.Empty ),
											   "Randomization Complete",
											   MessageBoxButton.OK,
											   MessageBoxImage.Information ) );
				}
				else if ( progress.IsFailed )
				{
					await this.Dispatcher.InvokeAsync(
						() => MessageBox.Show( this.Parent as Window,
											   $"An error occurred while randomizing the game:\n\n" +
											   $"{progress.FailureException.Message}",
											   "Randomization Failed",
											   MessageBoxButton.OK,
											   MessageBoxImage.Error ) );
				}
				else if ( progress.IsCancelled )
				{
					await this.Dispatcher.InvokeAsync(
						() => MessageBox.Show( this.Parent as Window,
											   "Randomization was cancelled. There may still be partial game patch files in the output directory.",
											   "Randomization Cancelled",
											   MessageBoxButton.OK,
											   MessageBoxImage.Warning ) );
				}
			} );

			dialog.Show();
		}

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;


		[ NotifyPropertyChangedInvocator ]
		protected virtual void OnPropertyChanged( [ CallerMemberName ] string propertyName = null )
		{
			this.PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
		}

		#endregion
	}
}