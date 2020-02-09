using Caliburn.Micro;
using FRMDesktopUI.EventModels;
using FRMDesktopUI.Library.Models;

namespace FRMDesktopUI.ViewModels
{
	public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
	{
		private SalesViewModel _salesVM;
		private ILoggedInUserModel _user;
		private IEventAggregator _events;

		public ShellViewModel(IEventAggregator events, SalesViewModel salesVM, ILoggedInUserModel user)
		{
			_events = events;
			_salesVM = salesVM;
			_user = user;

			_events.Subscribe(this);

			ActivateItem(IoC.Get<LoginViewModel>());
		}

		public void ExitApplication()
		{
			TryClose();
		}

		public void LogOut()
		{
			_user.LogOffUser();
			ActivateItem(IoC.Get<LoginViewModel>());
			NotifyOfPropertyChange(() => IsLoggedIn);
		}

		public bool IsLoggedIn
		{
			get
			{
				bool output = false;

				if (string.IsNullOrWhiteSpace(_user.Token) == false)
				{
					output = true;
				}

				return output;
			}
		}

		public void Handle(LogOnEvent message)
		{
			ActivateItem(_salesVM);
			NotifyOfPropertyChange(() => IsLoggedIn);
		}
	}
}
