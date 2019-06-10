using System;
using System.Windows;

namespace PokeRandomizer.UI
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public static readonly Guid FileDialogCookieRomPath = new Guid( "04e707ac-90d3-49bf-a5db-a551dbda2de9" );
		public static readonly Guid FileDialogCookieConfigFile = new Guid( "fbf79292-c7a5-4699-a38e-096a0cedb727" );
		public static readonly Guid FileDialogCookieOutputFolder = new Guid( "3345becb-7e79-4098-b565-d79754860b80" );
	}
}