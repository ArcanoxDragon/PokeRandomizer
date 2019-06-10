using System.ComponentModel;
using System.Windows;

namespace PokeRandomizer.UI.Controls
{
	public partial class RandomizerActions : INotifyPropertyChanged
	{
		public static DependencyProperty OutputPathProperty = DependencyProperty.Register(
			"OutputPath",
			typeof( string ),
			typeof( RandomizerActions )
		);

		public static DependencyProperty CanRandomizeProperty = DependencyProperty.Register(
			"CanRandomize",
			typeof( bool ),
			typeof( RandomizerActions )
		);

		public event PropertyChangedEventHandler PropertyChanged;

		public event RoutedEventHandler LoadConfigFileClick;
		public event RoutedEventHandler SaveConfigFileClick;
		public event RoutedEventHandler ResetConfigClick;
		public event RoutedEventHandler SetOutputPathClick;
		public event RoutedEventHandler RandomizeClick;

		public RandomizerActions()
		{
			this.InitializeComponent();
		}

		public string OutputPath
		{
			get => this.GetValue( OutputPathProperty ) as string;
			set => this.SetValue( OutputPathProperty, value );
		}

		public bool CanRandomize
		{
			get => (bool) this.GetValue( CanRandomizeProperty );
			set => this.SetValue( CanRandomizeProperty, value );
		}

		private void LoadConfigFile_Click( object sender, RoutedEventArgs e ) => this.LoadConfigFileClick?.Invoke( this, e );
		private void SaveConfigFile_Click( object sender, RoutedEventArgs e ) => this.SaveConfigFileClick?.Invoke( this, e );
		private void ResetConfig_Click( object sender, RoutedEventArgs e ) => this.ResetConfigClick?.Invoke( this, e );
		private void SetOutputPath_Click( object sender, RoutedEventArgs e ) => this.SetOutputPathClick?.Invoke( this, e );
		private void Randomize_Click( object sender, RoutedEventArgs e ) => this.RandomizeClick?.Invoke( this, e );
	}
}