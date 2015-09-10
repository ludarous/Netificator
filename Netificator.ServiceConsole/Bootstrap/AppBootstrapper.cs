using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Netificator.ServiceConsole.ViewModels;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Netificator.ServiceConsole.Bootstrap
{
    public class AppBootstrapper : BootstrapperBase
	{
		WindsorContainer _container;

        public AppBootstrapper()
        {
            try
            {
                Initialize();
            }
            catch (Exception ex)
            {

            }
        }       

		protected override void Configure()
		{
            _container = new WindsorContainer();

		    _container.AddFacility<EventRegistrationFacility>();

		    _container.Register(
		        Component.For<IWindowManager>().ImplementedBy<WindowManager>().LifestyleSingleton(),
		        Component.For<IEventAggregator>().ImplementedBy<EventAggregator>().LifestyleSingleton()
		        );

             _container.Register(
                Classes.FromThisAssembly().BasedOn<ShellViewModel>().LifestyleTransient());
		}

		protected override object GetInstance(Type serviceType, string key)
		{
            if (string.IsNullOrEmpty(key))
            {
                return _container.Resolve(serviceType);
            }
		    return _container.Resolve(key, serviceType);
		}     

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.ResolveAll(serviceType).Cast<object>();
        }


        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            var windowManager = IoC.Get<IWindowManager>();
            var mainViewModel = IoC.Get<ShellViewModel>();
            windowManager.ShowWindow(mainViewModel);

            base.OnStartup(sender, e);
        }
	}
}