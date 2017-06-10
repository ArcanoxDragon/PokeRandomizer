using System.Windows.Controls;

namespace GarcBrowser.Pages
{
    public abstract class FileEditorPage : Page
    {
        protected FileEditorPage( MainWindow parent, string path )
        {
            this.ParentWindow = parent;
            this.Path = path;
        }

        protected FileEditorPage() : this( null, null ) { }

        protected MainWindow ParentWindow { get; }
        protected string Path { get; }
    }
}