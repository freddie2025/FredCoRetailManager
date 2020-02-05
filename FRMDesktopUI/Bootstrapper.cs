using Caliburn.Micro;
using FRMDesktopUI.ViewModels;
using System.Windows;

namespace FRMDesktopUI
{
	public class Bootstrapper : BootstrapperBase
	{
		public Bootstrapper()
		{
			Initialize();
		}

		protected override void OnStartup(object sender, StartupEventArgs e)
		{
			DisplayRootViewFor<ShellViewModel>();
		}
	}
}
