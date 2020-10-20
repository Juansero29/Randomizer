using Autofac;
using EnigmatiKreations.Framework.Services.Alerts;
using EnigmatiKreations.Framework.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnigmatiKreations.Framework.MVVM.BaseViewModels
{
    /// <summary>
    /// This class locates view models on two different cases. Either mock view models for unit tests, 
    /// or real view models for the correct execution of the app. It uses a <see cref="Autofac.IContainer"/>
    /// for this purpose. 
    /// </summary>
    public static class ViewModelLocator
    {
        private static IContainer _Container;

        static ViewModelLocator()
        {
            RegisterDependencies(false);
        }


        /// <summary>
        /// Registers instances of the desired viewmodels to be resolved later. The boolean parameter defines
        /// wether the view models should be created as mocks or as real view models.
        /// </summary>
        /// <param name="mockDependencies"></param>
        public static void RegisterDependencies(bool mockDependencies)
        {
            var builder = new ContainerBuilder();

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

        /// <summary>
        /// Resolves the desired type, then returns the object that corresponds to this type using the <see cref="_Container"/>
        /// </summary>
        /// <typeparam name="T">The type of the object to be resolved</typeparam>
        /// <returns></returns>
        public static T Resolve<T>() where T : class
        {
            return _Container.Resolve<T>();
        }

        public static void Dispose()
        {
            _Container.Dispose();
        }

    }
}
