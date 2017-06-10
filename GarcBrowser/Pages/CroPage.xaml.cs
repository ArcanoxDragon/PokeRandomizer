using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CtrDotNet.CTR.Cro;
using GarcBrowser.FileInfo;
using GarcBrowser.Properties;
using MahApps.Metro.Controls;

namespace GarcBrowser.Pages
{
    public partial class CroPage : INotifyPropertyChanged
    {
        private List<CroFileInfo> files;
        private CroFile cro;

        public CroPage( MainWindow parent, string garcPath ) : base( parent, garcPath )
        {
            this.DataContext = this;

            this.InitializeComponent();
        }

        public List<CroFileInfo> Files
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
            this.ParentWindow.Title = $"CRO File - {this.Path}";

            await this.ReloadCro();
        }

        private async Task ReloadCro()
        {
            this.ParentWindow.Status = "Loading CRO file...";

            this.cro = await CroFile.FromFile( this.Path );

            this.Files = this.cro.Sections.Select( s => new CroFileInfo {
                SectionName = s.Key.ToString(),
                Offset = s.Value.Offset,
                Size = s.Value.Size
            } ).ToList();

            this.ParentWindow.Status = null;
            this.ParentWindow.IsWorking = false;
        }

        private async void ListView_MouseDoubleClick( object sender, MouseButtonEventArgs e )
        {
            if ( !( sender is ListView list ) )
                return;

            var item = list.SelectedItem as CroFileInfo;

            if ( item == null )
                return;

            this.ParentWindow.Status = "Reading entry...";
            this.ParentWindow.IsWorking = true;

            await Task.Run( async () => {
                byte[] entryData;

                if ( !Enum.TryParse<CroFile.SectionType>( item.SectionName, out var section ) )
                {
                    await this.Dispatcher.InvokeAsync(
                        () => MessageBox.Show( this.TryFindParent<Window>(),
                                               $"Unknown section: {item.SectionName}",
                                               "Unknown Section",
                                               MessageBoxButton.OK,
                                               MessageBoxImage.Error ) );
                    return;
                }

                entryData = await this.cro.GetSection( section );

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