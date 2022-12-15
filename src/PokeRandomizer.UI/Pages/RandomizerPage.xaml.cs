using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
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
		private bool   canCreatePatchFolder;
		private bool   createPatchFolder;

		public RandomizerPage(IRandomizer randomizer)
		{
			this.hintElements = new List<UIElement>();

			Randomizer = randomizer;
			DataContext = Randomizer;
			CanCreatePatchFolder = !string.IsNullOrEmpty(Randomizer.Game.GetTitleId());

			InitializeComponent();
		}

		public IRandomizer      Randomizer { get; }
		public RandomizerConfig Config     => Randomizer.Config;

		public string OutputPath
		{
			get => this.outputPath;
			private set
			{
				this.outputPath = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(OutputPathDisplay));
				OnPropertyChanged(nameof(CanRandomize));
			}
		}

		public int? Seed
		{
			get => this.seed;
			private set
			{
				this.seed = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(SeedDisplay));
			}
		}

		public bool CanCreatePatchFolder
		{
			get => this.canCreatePatchFolder;
			set
			{
				this.canCreatePatchFolder = value;
				OnPropertyChanged();
			}
		}

		public bool CreatePatchFolder
		{
			get => this.createPatchFolder;
			set
			{
				this.createPatchFolder = value;
				OnPropertyChanged();
			}
		}

		public string OutputPathDisplay => OutputPath ?? "None";
		public bool   CanRandomize      => !string.IsNullOrEmpty(OutputPath);
		public string SeedDisplay       => Seed?.ToString() ?? "Automatic";

		private void RefreshHintElements()
		{
			// Remove listeners for existing elements so we don't end up with duplicates
			foreach (var element in this.hintElements)
			{
				element.MouseEnter -= OnControlMouseEnter;
				element.MouseLeave -= OnControlMouseLeave;
			}

			this.hintElements.Clear();

			// Find all children with hint text properties and hook their mouseover behavior
			foreach (var child in from child in this.GetVisualChildren<UIElement>()
								  let hintText = child.GetValue(DependencyProperties.Properties.HintTextProperty) as string
								  where !string.IsNullOrEmpty(hintText)
								  select child)
			{
				child.MouseEnter += OnControlMouseEnter;
				child.MouseLeave += OnControlMouseLeave;
				this.hintElements.Add(child);
			}
		}

		private void MainPage_Loaded(object sender, RoutedEventArgs e)
		{
			this.defaultHintText = this.HintBox.Text;
		}

		private void OnControlMouseEnter(object sender, MouseEventArgs e)
		{
			if (sender is not DependencyObject dep)
				return;

			string hintText = dep.GetValue(DependencyProperties.Properties.HintTextProperty) as string
							  ?? this.defaultHintText;

			hintText = Regex.Replace(hintText, @"\s*\\n\s*", "\n");

			this.HintBox.Text = hintText;
		}

		private void OnControlMouseLeave(object sender, MouseEventArgs mouseEventArgs)
		{
			this.HintBox.Text = this.defaultHintText;
		}

		private void Tabs_LayoutUpdated(object sender, EventArgs e)
		{
			if (this.Tabs.SelectedIndex != this.lastTab)
			{
				this.lastTab = this.Tabs.SelectedIndex;
				RefreshHintElements();
			}
		}

		private async void LoadConfigFile_Click(object sender, RoutedEventArgs e)
		{
			var dialog = new CommonOpenFileDialog { EnsureFileExists = true, Filters = { new CommonFileDialogFilter("JSON Files (.json)", ".json") }, CookieIdentifier = App.FileDialogCookieConfigFile };
			var result = dialog.ShowDialog(Parent as Window);

			if (result == CommonFileDialogResult.Ok)
			{
				try
				{
					await using var fs = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read, FileShare.None);
					using var sr = new StreamReader(fs);
					string configContents = await sr.ReadToEndAsync();

					Randomizer.Config = JsonConvert.DeserializeObject<RandomizerConfig>(configContents);

					// Refresh config in UI
					DataContext = null;
					DataContext = Randomizer;
				}
				catch (Exception ex)
				{
					MessageBox.Show(Parent as Window,
									$"An error occurred loading the config file:\n\n" +
									$"{ex.Message}",
									"Error Loading Config",
									MessageBoxButton.OK,
									MessageBoxImage.Error);
				}
			}
		}

		private async void SaveConfigFile_Click(object sender, RoutedEventArgs e)
		{
			var dialog = new CommonSaveFileDialog {
				AlwaysAppendDefaultExtension = true,
				OverwritePrompt = true,
				Filters = { new CommonFileDialogFilter("JSON Files (.json)", ".json") },
				DefaultExtension = ".json",
				CookieIdentifier = App.FileDialogCookieConfigFile
			};
			var result = dialog.ShowDialog(Parent as Window);

			if (result == CommonFileDialogResult.Ok)
			{
				try
				{
					await using var fs = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
					await using var sw = new StreamWriter(fs);
					string json = JsonConvert.SerializeObject(Randomizer.Config, Formatting.Indented);

					await sw.WriteLineAsync(json);
				}
				catch (Exception ex)
				{
					MessageBox.Show(Parent as Window,
									$"An error occurred saving the config file:\n\n" +
									$"{ex.Message}",
									"Error Saving Config",
									MessageBoxButton.OK,
									MessageBoxImage.Error);
				}
			}
		}

		private void ResetConfig_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show(Parent as Window,
							    "Are you sure you want to reset the settings back to default?\n\n" +
							    "All unsaved changes will be lost.",
							    "Reset Settings",
							    MessageBoxButton.YesNo,
							    MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				Randomizer.Config = RandomizerConfig.Default.AsEditable();

				// Refresh config in UI
				DataContext = null;
				DataContext = Randomizer;
			}
		}

		private void SetOutputPath_Click(object sender, RoutedEventArgs e)
		{
			var dialog = new CommonOpenFileDialog { IsFolderPicker = true, EnsurePathExists = true, CookieIdentifier = App.FileDialogCookieOutputFolder };
			var result = dialog.ShowDialog(Parent as Window);

			if (result == CommonFileDialogResult.Ok)
			{
				OutputPath = dialog.FileName;
			}
		}

		private void SetSeed_Click(object sender, RoutedEventArgs e)
		{
			var promptDialog = new PromptDialog { Owner = Parent as Window, Message = "Enter a custom numeric seed value, or click Cancel to use an automatic seed", ValueRequired = true };

			// Only valid if it's a number
			promptDialog.Validate += (o, args) => args.IsValid = int.TryParse(promptDialog.Text, out _);

			if (promptDialog.ShowDialog() == true)
			{
				Seed = int.Parse(promptDialog.Text);
			}
			else
			{
				Seed = null;
			}
		}

		private void Randomize_Click(object sender, RoutedEventArgs e)
		{
			if (Dispatcher == null)
				throw new ApplicationException("Fatal error");

			var shouldLogResult = MessageBox.Show(Parent as Window,
												  "Should a randomizer log file be created in the selected output path? " +
												  "The log file will contain a record of all randomizations performed, " +
												  "which may provide an unfair advantage to players who are participating " +
												  "in a random race. The log will also contain the randomizer seed being " +
												  "used, which will allow the same randomization to be performed at a later " +
												  "time, provided the same options are used.",
												  "Create Log File",
												  MessageBoxButton.YesNoCancel);

			if (shouldLogResult == MessageBoxResult.Cancel)
				return;

			var logFilePath = default(string);
			var logger = default(FileLogger);
			var createLogFile = shouldLogResult == MessageBoxResult.Yes;
			var progressBar = new TaskDialogProgressBar(0, 100, 0);
			var dialog = new TaskDialog {
				Cancelable = false,
				Caption = "Randomizing Game",
				ExpansionMode = TaskDialogExpandedDetailsLocation.Hide,
				Icon = TaskDialogStandardIcon.Information,
				InstructionText = "Randomizing game...this may take a few minutes",
				ProgressBar = progressBar,
				OwnerWindowHandle = new WindowInteropHelper(Parent as Window).Handle,
				StartupLocation = TaskDialogStartupLocation.CenterOwner,
				StandardButtons = TaskDialogStandardButtons.Cancel
			};

			var cancelSource = new CancellationTokenSource();

			dialog.Closing += (_, ev) => {
				if (ev.TaskDialogResult == TaskDialogResult.Cancel)
				{
					ev.Cancel = true;
					cancelSource.Cancel();
					progressBar.State = TaskDialogProgressBarState.Marquee;
					dialog.Text = "Cancelling...";
				}
			};

			// ReSharper disable once AsyncVoidLambda
			dialog.Opened += (_, _) => ThreadPool.QueueUserWorkItem(async _ => {
				var outputPath = OutputPath;

				if (CreatePatchFolder)
				{
					var titleId = Randomizer.Game.GetTitleId();

					if (!string.IsNullOrEmpty(titleId))
					{
						outputPath = Path.Combine(outputPath, titleId);
					}
				}

				Randomizer.Game.OutputPathOverride = outputPath;

				if (Seed.HasValue)
					Randomizer.Reseed(Seed.Value);
				else
					Randomizer.Reseed();

				if (createLogFile)
				{
					logFilePath = Path.Combine(OutputPath, "RandomizerLog.txt");
					logger = new FileLogger(logFilePath);
					Randomizer.Logger = logger;
				}
				else
				{
					Randomizer.Logger = null;
				}

				var progress = new ProgressNotifier();
				var minLines = 0;

				progress.ProgressUpdated += (_, update) => Dispatcher.Invoke(() => {
					if (update.Type == ProgressUpdate.UpdateType.Status)
					{
						var status = update.Status ?? progress.Status;
						var numLines = status.Split('\n').Length;

						if (numLines > minLines)
							minLines = numLines;

						if (numLines < minLines)
							status += "\n".Repeat(minLines - numLines); // keep the text from shrinking and causing the dialog to spaz

						progressBar.Value = (int) ( progress.Progress * 100.0 );
						dialog.Text = status;
					}
				});

				try
				{
					await Randomizer.RandomizeAll(progress, cancelSource.Token);
				}
				catch (Exception ex)
				{
					await Dispatcher.InvokeAsync(
						() => MessageBox.Show(Parent as Window,
											  $"An error occurred while randomizing the game:\n\n" +
											  $"{ex.Message}",
											  "Randomization Error",
											  MessageBoxButton.OK,
											  MessageBoxImage.Error));
					dialog.Close(TaskDialogResult.Ok);
					return;
				}
				finally
				{
					logger?.Dispose();
				}

				dialog.Close(TaskDialogResult.Ok);

				if (progress.IsComplete)
				{
					await Dispatcher.InvokeAsync(
						() => MessageBox.Show(Parent as Window,
											  $"Game patch files were successfully saved to:\n\n{OutputPath}." +
											  ( createLogFile ? $"\n\nLog file was saved to:\n\n{logFilePath}." : string.Empty ),
											  "Randomization Complete",
											  MessageBoxButton.OK,
											  MessageBoxImage.Information));
				}
				else if (progress.IsFailed)
				{
					await Dispatcher.InvokeAsync(
						() => MessageBox.Show(Parent as Window,
											  $"An error occurred while randomizing the game:\n\n" +
											  $"{progress.FailureException.Message}",
											  "Randomization Failed",
											  MessageBoxButton.OK,
											  MessageBoxImage.Error));
				}
				else if (progress.IsCancelled)
				{
					await Dispatcher.InvokeAsync(
						() => MessageBox.Show(Parent as Window,
											  "Randomization was cancelled. There may still be partial game patch files in the output directory.",
											  "Randomization Cancelled",
											  MessageBoxButton.OK,
											  MessageBoxImage.Warning));
				}
			});

			dialog.Show();
		}

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}