using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace PokeRandomizer.UI.Controls
{
	public class ValidateEventArgs : EventArgs
	{
		public bool IsValid { get; set; } = true;
	}

	/// <summary>
	/// Interaction logic for PromptDialog.xaml
	/// </summary>
	public partial class PromptDialog : Window, INotifyPropertyChanged
	{
		public static DependencyProperty MessageProperty = DependencyProperty.Register(
			nameof(Message),
			typeof( string ),
			typeof( PromptDialog ),
			new PropertyMetadata( defaultValue: "Enter some text: " )
		);

		public static DependencyProperty TextProperty = DependencyProperty.Register(
			nameof(Text),
			typeof( string ),
			typeof( PromptDialog )
		);

		public static DependencyProperty ValueRequiredProperty = DependencyProperty.Register(
			nameof(ValueRequired),
			typeof( bool ),
			typeof( PromptDialog )
		);

		public event EventHandler<ValidateEventArgs> Validate;

		private bool isValid = true;

		public PromptDialog()
		{
			this.InitializeComponent();
		}

		protected override void OnInitialized( EventArgs e )
		{
			base.OnInitialized( e );

			this.TextBox.Focus();
		}

		public string Message
		{
			get => (string) this.GetValue( MessageProperty );
			set => this.SetValue( MessageProperty, value );
		}

		public string Text
		{
			get => (string) this.GetValue( TextProperty );
			set
			{
				this.SetValue( TextProperty, value );
				this.OnPropertyChanged( nameof(this.OkEnabled) );
			}
		}

		public bool IsValid
		{
			get => this.isValid;
			set
			{
				this.isValid = value;
				this.OnPropertyChanged();
				this.OnPropertyChanged( nameof(this.OkEnabled) );
			}
		}

		public bool ValueRequired
		{
			get => (bool) this.GetValue( ValueRequiredProperty );
			set
			{
				this.SetValue( ValueRequiredProperty, value );
				this.OnPropertyChanged( nameof(this.OkEnabled) );
			}
		}

		public bool OkEnabled => this.IsValid && !( this.ValueRequired && string.IsNullOrEmpty( this.Text ) );

		private void OkButton_Click( object sender, RoutedEventArgs e ) => this.DialogResult = true;
		private void CancelButton_Click( object sender, RoutedEventArgs e ) => this.DialogResult = false;

		private void TextBox_TextChanged( object sender, TextChangedEventArgs e )
		{
			var validateEventArgs = new ValidateEventArgs();

			this.Text = this.TextBox.Text;
			this.Validate?.Invoke( this, validateEventArgs );
			this.IsValid = validateEventArgs.IsValid;
		}

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged( [ CallerMemberName ] string propertyName = null )
		{
			this.PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
		}

		#endregion
	}
}