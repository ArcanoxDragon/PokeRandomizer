using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using GarcBrowser.FileTypes;
using GarcBrowser.Properties;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace GarcBrowser
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        private string status;
        private bool isWorking;
        private double progress;

        public MainWindow()
        {
            this.DataContext = this;
            this.Status = "Ready";

            this.InitializeComponent();
        }

        public string Status
        {
            get => this.status;
            set
            {
                this.status = value ?? "Ready";
                this.OnPropertyChanged();
            }
        }

        public bool IsWorking
        {
            get => this.isWorking;
            set
            {
                this.isWorking = value;
                this.OnPropertyChanged();

                if ( !value )
                    this.Progress = -1;
            }
        }

        public double Progress
        {
            get => this.progress;
            set
            {
                this.progress = value;
                this.OnPropertyChanged();
            }
        }

        public bool HasProgress => this.IsWorking && this.Progress >= 0;

        private async void ButtonOpen_Click( object sender, RoutedEventArgs e )
        {
            var dialog = new CommonOpenFileDialog {
                CookieIdentifier = App.FileDialogCookieGarc,
                EnsureFileExists = true
            };
            var result = dialog.ShowDialog( this );

            if ( result == CommonFileDialogResult.Ok )
            {
                this.IsWorking = true;
                this.Status = "Reading file...";

                try
                {
                    FileType fileType;

                    using ( var fs = new FileStream( dialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read ) )
                    {
                        byte[] header = new byte[ FileTypeUtil.MaximumHeaderSize ];
                        await fs.ReadAsync( header, 0, header.Length );

                        fileType = FileTypeUtil.IdentifyFile( header );
                    }

                    if ( fileType == FileType.Unknown )
                        throw new NotSupportedException( "Unknown file type" );

                    var page = fileType.GetPage( this, dialog.FileName );
                    this.ViewFrame.Navigate( page );
                }
                catch ( Exception ex )
                {
                    MessageBox.Show( this,
                                     $"Could not open the file:\n\n" +
                                     $"{ex.Message}",
                                     "Error Opening File",
                                     MessageBoxButton.OK,
                                     MessageBoxImage.Error );
                }
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [ NotifyPropertyChangedInvocator ]
        protected virtual void OnPropertyChanged( [ CallerMemberName ] string propertyName = null )
            => this.PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );

        #endregion
    }
}