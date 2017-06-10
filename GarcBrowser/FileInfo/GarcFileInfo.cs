using System.ComponentModel;
using System.Runtime.CompilerServices;
using GarcBrowser.Properties;

namespace GarcBrowser.FileInfo
{
    public class GarcFileInfo : INotifyPropertyChanged
    {
        private int fileNumber;
        private long size;
        private long offset;
        private bool? wasCompressed;

        public int FileNumber
        {
            get => this.fileNumber;
            set
            {
                this.fileNumber = value;
                this.OnPropertyChanged();
            }
        }

        public long Size
        {
            get => this.size;
            set
            {
                this.size = value;
                this.OnPropertyChanged();
            }
        }

        public long Offset
        {
            get => this.offset;
            set
            {
                this.offset = value;
                this.OnPropertyChanged();
            }
        }

        public bool? WasCompressed
        {
            get => this.wasCompressed;
            set
            {
                this.wasCompressed = value;
                this.OnPropertyChanged();
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