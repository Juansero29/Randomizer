using Autofac;
using Randomizer.Framework.Services.Alerts;
using Randomizer.Framework.Services.Navigation;
using Randomizer.Framework.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Randomizer.Framework.ViewModels.BaseViewModels
{
    public static class ViewModelLocator
    {
        private static IContainer _Container;

        static ViewModelLocator()
        {
            RegisterDependencies(false);
        }

        public static void RegisterDependencies(bool mockDependencies)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<HomePageViewModel>();
            builder.RegisterType<ListEditionPageViewModel>();

            // Service are instantiated as singletons
            if (mockDependencies)
            {
                builder.RegisterInstance(new NavigationMockService()).As<INavigationService>().SingleInstance();
                builder.RegisterInstance(new AlertsMockService()).As<IAlertsService>().SingleInstance();
            }
            else
            {
                builder.RegisterType<ShellNavigationService>().As<INavigationService>().SingleInstance();
                builder.RegisterType<AlertsService>().As<IAlertsService>().SingleInstance();
            }

            if (_Container != null)
            {
                _Container.Dispose();
            }
            _Container = builder.Build();
        }

        public static T Resolve<T>() where T : class
        {
            return _Container.Resolve<T>();
        }


    }
}
