using System;
using System.Collections.Generic;
using Caliburn.Micro;
using System.Windows;
using Luke.Net.ViewModels;
using Luke.Net.Lib;
using Luke.Net.Lib.UI;
using Luke.Net.Lib.Index;

namespace Luke.Net
{
	public class AppBootstrapper : BootstrapperBase
	{
		SimpleContainer container;

		public AppBootstrapper()
		{
			Start();
		}

		protected override void Configure()
		{
			container = new SimpleContainer();

            container.Instance(container);
			
            container.Singleton<IWindowManager, WindowManager>();
			container.Singleton<IEventAggregator, EventAggregator>();
            container.Singleton<IIndexService, IndexService>();
            
            container.PerRequest<ShellViewModel>();
            container.PerRequest<OverviewViewModel>();

            container.PerRequest<IFolderBrowser, FolderBrowser>();
            container.PerRequest<IMessageBox, Luke.Net.Lib.UI.MessageBox>();
		}

		protected override object GetInstance(Type service, string key)
		{
            if (!string.IsNullOrEmpty(key))
            {
                var type = Type.GetType(key);
                return container.GetInstance(type, null);
            }

			var instance = container.GetInstance(service, key);
			if (instance != null)
				return instance;

			throw new InvalidOperationException("Could not locate any instances.");
		}

		protected override IEnumerable<object> GetAllInstances(Type service)
		{
			return container.GetAllInstances(service);
		}

		protected override void BuildUp(object instance)
		{
			container.BuildUp(instance);
		}

		protected override void OnStartup(object sender, StartupEventArgs e)
		{
			DisplayRootViewFor<ShellViewModel>();
		}
	}
}