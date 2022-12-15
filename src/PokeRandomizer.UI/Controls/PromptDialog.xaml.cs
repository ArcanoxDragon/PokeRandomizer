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
			typeof(string),
			typeof(PromptDialog),
			new PropertyMetadata(defaultValue: "Enter some text: ")
		);

		public static DependencyProperty TextProperty = DependencyProperty.Register(
			nameof(Text),
			typeof(string),
			typeof(PromptDialog)
		);

		public static DependencyProperty ValueRequiredProperty = DependencyProperty.Register(
			nameof(ValueRequired),
			typeof(bool),
			typeof(PromptDialog)
		);

		public event EventHandler<ValidateEventArgs> Validate;

		private bool isValid = true;

		public PromptDialog()
		{
			InitializeComponent();
		}

		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);

			this.TextBox.Focus();
		}

		public string Message
		{
			get => (string) GetValue(MessageProperty);
			set => SetValue(MessageProperty, value);
		}

		public string Text
		{
			get => (string) GetValue(TextProperty);
			set
			{
				SetValue(TextProperty, value);
				OnPropertyChanged(nameof(OkEnabled));
			}
		}

		public bool IsValid
		{
			get => this.isValid;
			set
			{
				this.isValid = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(OkEnabled));
			}
		}

		public bool ValueRequired
		{
			get => (bool) GetValue(ValueRequiredProperty);
			set
			{
				SetValue(ValueRequiredProperty, value);
				OnPropertyChanged(nameof(OkEnabled));
			}
		}

		public bool OkEnabled => IsValid && !( ValueRequired && string.IsNullOrEmpty(Text) );

		private void OkButton_Click(object sender, RoutedEventArgs e) => DialogResult = true;
		private void CancelButton_Click(object sender, RoutedEventArgs e) => DialogResult = false;

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			var validateEventArgs = new ValidateEventArgs();

			Text = this.TextBox.Text;
			Validate?.Invoke(this, validateEventArgs);
			IsValid = validateEventArgs.IsValid;
		}

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}