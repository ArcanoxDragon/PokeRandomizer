using System.Windows;

namespace PokeRandomizer.UI.Controls
{
	public partial class RandomizerActions
	{
		public static DependencyProperty OutputPathProperty = DependencyProperty.Register(
			nameof(OutputPath),
			typeof(string),
			typeof(RandomizerActions)
		);

		public static DependencyProperty CanRandomizeProperty = DependencyProperty.Register(
			nameof(CanRandomize),
			typeof(bool),
			typeof(RandomizerActions)
		);

		public static DependencyProperty SeedDisplayProperty = DependencyProperty.Register(
			nameof(SeedDisplay),
			typeof(string),
			typeof(RandomizerActions)
		);

		public static DependencyProperty CanCreatePatchFolderProperty = DependencyProperty.Register(
			nameof(CanCreatePatchFolder),
			typeof(string),
			typeof(RandomizerActions)
		);

		public static DependencyProperty CreatePatchFolderProperty = DependencyProperty.Register(
			nameof(CreatePatchFolder),
			typeof(string),
			typeof(RandomizerActions)
		);

		public event RoutedEventHandler LoadConfigFileClick;
		public event RoutedEventHandler SaveConfigFileClick;
		public event RoutedEventHandler ResetConfigClick;
		public event RoutedEventHandler SetOutputPathClick;
		public event RoutedEventHandler SetSeedClick;
		public event RoutedEventHandler RandomizeClick;

		public RandomizerActions()
		{
			InitializeComponent();
		}

		public string OutputPath
		{
			get => GetValue(OutputPathProperty) as string;
			set => SetValue(OutputPathProperty, value);
		}

		public bool CanRandomize
		{
			get => (bool) GetValue(CanRandomizeProperty);
			set => SetValue(CanRandomizeProperty, value);
		}

		public string SeedDisplay
		{
			get => (string) GetValue(SeedDisplayProperty);
			set => SetValue(SeedDisplayProperty, value);
		}

		public bool CanCreatePatchFolder
		{
			get => (bool) GetValue(CanCreatePatchFolderProperty);
			set => SetValue(CanCreatePatchFolderProperty, value);
		}

		public bool CreatePatchFolder
		{
			get => (bool) GetValue(CreatePatchFolderProperty);
			set => SetValue(CreatePatchFolderProperty, value);
		}

		private void LoadConfigFile_Click(object sender, RoutedEventArgs e) => LoadConfigFileClick?.Invoke(this, e);
		private void SaveConfigFile_Click(object sender, RoutedEventArgs e) => SaveConfigFileClick?.Invoke(this, e);
		private void ResetConfig_Click(object sender, RoutedEventArgs e) => ResetConfigClick?.Invoke(this, e);
		private void SetOutputPath_Click(object sender, RoutedEventArgs e) => SetOutputPathClick?.Invoke(this, e);
		private void SetSeed_Click(object sender, RoutedEventArgs e) => SetSeedClick?.Invoke(this, e);
		private void Randomize_Click(object sender, RoutedEventArgs e) => RandomizeClick?.Invoke(this, e);
	}
}