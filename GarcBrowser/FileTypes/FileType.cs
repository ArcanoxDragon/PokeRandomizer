using System;
using System.Text;
using GarcBrowser.Pages;

namespace GarcBrowser.FileTypes
{
    public enum FileType
    {
        Unknown = -1,
        Garc,
        Cro
    }

    public static class FileTypeExtensions
    {
        public static FileEditorPage GetPage( this FileType type, MainWindow parent, string filePath )
        {
            switch ( type )
            {
                case FileType.Garc:
                    return new GarcPage( parent, filePath );
                case FileType.Cro:
                    return new CroPage( parent, filePath );
            }

            throw new NotSupportedException( $"The file type \"{type}\" is not supported" );
        }
    }

    public static class FileTypeUtil
    {
        public const int MaximumHeaderSize = 0x84;

        public static FileType IdentifyFile( byte[] data )
        {
            // Try for GARC
            if ( Encoding.ASCII.GetString( data, 0x00, 0x04 ) == "CRAG")
                return FileType.Garc;
            
            // Try for CRO
            if ( Encoding.ASCII.GetString( data, 0x80, 0x04 ) == "CRO0")
                return FileType.Cro;

            return FileType.Unknown;
        }
    }
}