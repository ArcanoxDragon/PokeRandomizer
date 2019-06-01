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
using PokeRandomizer.Config;
using PokeRandomizer.Progress;
using PokeRandomizer.UI.Properties;
using PokeRandomizer.UI.Utility;

namespace PokeRandomizer.UI.Pages
{
	public partial class RandomizerPage : INotifyPropertyChanged
	{
		private readonly List<UIElement> hintElements;
		private string defaultHintText;
		private int lastTab = -1;
		private string outputPath;

		public RandomizerPage( IRandomizer randomizer )
		{
			this.hintElements = new List<UIElement>();

			this.Randomizer = randomizer;
			this.DataContext = this.Randomizer;

			this.InitializeComponent();
		}

		public RandomizerConfig Config => this.Randomizer.Config;
		public IRandomizer Randomizer { get; }

		public string OutputPath
		{
			get => this.outputPath;
			private set
			{
				this.outputPath = value;
				this.OnPropertyChanged( nameof(this.OutputPath) );
				this.OnPropertyChanged( nameof(this.HasOutputPath) );
				this.OnPropertyChanged( nameof(this.OutputPathDisplay) );
			}
		}

		public bool HasOutputPath => !string.IsNullOrEmpty( this.OutputPath );
		public string OutputPathDisplay => this.OutputPath ?? "None";

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
				Filters = { new CommonFileDialogFilter( "JSON Files (.json)", ".json" ) },
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
				OverwritePrompt = true,
				Filters = { new CommonFileDialogFilter( "JSON Files (.json)", ".json" ) },
				DefaultExtension = ".json",
				CookieIdentifier = App.FileDialogCookieConfigFile
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
				IsFolderPicker = true,
				EnsurePathExists = true,
				CookieIdentifier = App.FileDialogCookieOutputFolder
			};
			var result = dialog.ShowDialog( this.Parent as Window );

			if ( result == CommonFileDialogResult.Ok )
			{
				this.OutputPath = dialog.FileName;
			}
		}

		private async void Randomize_Click( object sender, RoutedEventArgs e )
		{
			var progressBar = new TaskDialogProgressBar( 0, 100, 0 );
			var dialog = new TaskDialog {
				Cancelable = false,
				Caption = "Randomizing Game",
				ExpansionMode = TaskDialogExpandedDetailsLocation.Hide,
				Icon = TaskDialogStandardIcon.Information,
				InstructionText = "Randomizing game...this may take a few minutes",
				ProgressBar = progressBar,
				OwnerWindowHandle = new WindowInteropHelper( this.Parent as Window ).Handle,
				StartupLocation = TaskDialogStartupLocation.CenterOwner,
				StandardButtons = TaskDialogStandardButtons.Cancel
			};

			var cancelSource = new CancellationTokenSource();
			var overallTask = Task.Run( async () => {
				this.Randomizer.Game.OutputPathOverride = this.OutputPath;

				var progress = new ProgressNotifier();
				var randTask = this.Randomizer.RandomizeAll( progress, cancelSource.Token );

				while ( !progress.IsComplete && !progress.IsFailed && !progress.IsCancelled )
				{
					var update = await progress.AwaitUpdate();

					if ( update.Type == ProgressUpdate.UpdateType.Status )
						await this.Dispatcher.InvokeAsync( () => {
							progressBar.Value = (int) ( progress.Progress * 100.0 );
							dialog.Text = update.Status ?? progress.Status;
						} );
				}

				try
				{
					await randTask;
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

				dialog.Close( TaskDialogResult.Ok );

				if ( progress.IsComplete )
					await this.Dispatcher.InvokeAsync(
						() => MessageBox.Show( this.Parent as Window,
											   $"Game patch files were successfully saved to:\n\n" +
											   $"{this.OutputPath}",
											   "Randomization Complete",
											   MessageBoxButton.OK,
											   MessageBoxImage.Information ) );
				else if ( progress.IsFailed )
					await this.Dispatcher.InvokeAsync(
						() => MessageBox.Show( this.Parent as Window,
											   $"An error occurred while randomizing the game:\n\n" +
											   $"{progress.FailureException.Message}",
											   "Randomization Failed",
											   MessageBoxButton.OK,
											   MessageBoxImage.Error ) );
				else if ( progress.IsCancelled )
					await this.Dispatcher.InvokeAsync(
						() => MessageBox.Show( this.Parent as Window,
											   "Randomization was cancelled. There may still be partial game patch files in the output directory.",
											   "Randomization Cancelled",
											   MessageBoxButton.OK,
											   MessageBoxImage.Warning ) );
			} );

			dialog.Closing += ( o, ev ) => {
				if ( ev.TaskDialogResult == TaskDialogResult.Cancel )
				{
					ev.Cancel = true;
					cancelSource.Cancel();
					progressBar.State = TaskDialogProgressBarState.Marquee;
					dialog.Text = "Cancelling...";
				}
			};
			dialog.Show();

			await overallTask;
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