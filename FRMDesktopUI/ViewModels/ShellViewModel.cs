﻿using Caliburn.Micro;
using FRMDesktopUI.EventModels;

namespace FRMDesktopUI.ViewModels
{
	public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
	{
		private SalesViewModel _salesVM;
		private IEventAggregator _events;

		public ShellViewModel(IEventAggregator events, SalesViewModel salesVM)
		{
			_events = events;
			_salesVM = salesVM;

			_events.Subscribe(this);

			ActivateItem(IoC.Get<LoginViewModel>());
		}

		public void Handle(LogOnEvent message)
		{
			ActivateItem(_salesVM);
		}
	}
}
