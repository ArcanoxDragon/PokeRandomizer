using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using CtrDotNet.CTR.Garc;
using GarcBrowser.FileInfo;
using GarcBrowser.Properties;

namespace GarcBrowser.Pages
{
    public partial class GarcPage : INotifyPropertyChanged
    {
        private List<GarcFileInfo> files;
        private GarcFile garc;

        public GarcPage( MainWindow parent, string garcPath ) : base( parent, garcPath )
        {
            this.DataContext = this;

            this.InitializeComponent();
        }

        public List<GarcFileInfo> Files
        {
            get => this.files;
            private set
            {
                this.files = value;
                this.OnPropertyChanged();
            }
        }

        private async void Page_Loaded( object sender, System.Windows.RoutedEventArgs e )
        {
            this.ParentWindow.Title = $"GARC File - {this.Path}";

            await this.ReloadGarc();
        }

        private async Task ReloadGarc()
        {
            this.ParentWindow.Status = "Loading GARC entries...";

            this.garc = await GarcFile.FromFile( this.Path, useLz: true );

            var items = new List<GarcFileInfo>();

            for ( int i = 0; i < this.garc.FileCount; i++ )
            {
                var entry = this.garc.GarcData.Def.Fatb.Entries[ i ];
                var sub = entry.SubEntries[ 0 ];

                if ( !sub.Exists )
                    continue;

                items.Add( new GarcFileInfo {
                    FileNumber = i,
                    Offset = sub.Start + this.garc.GarcData.Def.DataOffset,
                    Size = sub.Length,
                    WasCompressed = null
                } );

                this.ParentWindow.Progress = i / (double) this.garc.FileCount;
            }

            this.Files = items;

            this.ParentWindow.Status = null;
            this.ParentWindow.IsWorking = false;
            this.ParentWindow.Progress = -1;
        }

        private async void ListView_MouseDoubleClick( object sender, MouseButtonEventArgs e )
        {
            if ( !( sender is ListView list ) )
                return;

            var item = list.SelectedItem as GarcFileInfo;

            if ( item == null )
                return;

            this.ParentWindow.Status = "Reading entry...";
            this.ParentWindow.IsWorking = true;

            await Task.Run( async () => {
                byte[] entryData = await this.garc.GetFile( item.FileNumber );
                bool compressed = this.garc.GarcData.IsFileCompressed( item.FileNumber ) ?? false;
                this.Files[ item.FileNumber ].WasCompressed = compressed;

                await this.HexEditor.LoadData( entryData );
                await this.Dispatcher.InvokeAsync( () => {
                    this.ParentWindow.Status = null;
                    this.ParentWindow.IsWorking = false;
                } );
            } );
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [ NotifyPropertyChangedInvocator ]
        protected virtual void OnPropertyChanged( [ CallerMemberName ] string propertyName = null )
            => this.PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );

        #endregion
    }
}