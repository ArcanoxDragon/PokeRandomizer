using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GarcBrowser.Controls
{
    public partial class HexControl
    {
        public HexControl()
        {
            this.InitializeComponent();
        }

        public async Task LoadData( byte[] data )
        {
            await this.Dispatcher.InvokeAsync( () => this.HexEditor.IsEnabled = false );

            if ( data != null )
            {
                await this.Dispatcher.InvokeAsync( () => {
                    this.HexEditor.Stream?.Dispose();
                    this.HexEditor.Stream = new MemoryStream( data );
                    this.HexEditor.IsEnabled = true;
                } );
            }
        }
    }
}