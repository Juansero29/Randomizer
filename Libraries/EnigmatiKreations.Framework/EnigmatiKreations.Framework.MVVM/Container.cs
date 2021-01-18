using Autofac;
using Autofac.Core.Activators;
using EnigmatiKreations.Framework.Services.Alerts;
using EnigmatiKreations.Framework.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnigmatiKreations.Framework.MVVM.BaseViewModels
{
    /// <summary>
    /// A container of different services that need to be accessed globally by the app. It uses a <see cref="Autofac.IContainer"/>
    /// </summary>
    public static class Container
    {
        private static IContainer _Container;
        private static ContainerBuilder _Builder;

        static Container()
        {
      
        }

        /// <summary>
        /// Prepares a new builder
        /// </summary>
        public static void PrepareNewBuilder()
        {
            _Builder = new ContainerBuilder();
            if(_Container != null)
            {
                _Container.Dispose();
            }
            
            _Container = null;
        }

        /// <summary>
        /// Builds the container
        /// </summary>
        /// <returns>
        /// Wether the builder was kept or not or not
        /// </returns>
        public static bool BuildContainer()
        {
            if (_Container != null) return true;
            try
            {
                _Container = _Builder.Build();
                return true;
            }
            catch(Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// Registers an instance of an app dependency along with its type using a new builder
        /// </summary>
        /// <param name="instance">The instance</param>
        /// <param name="typeOfDependency">The type</param>
        /// <param name="isSingleton">Wether it has to be a singleton or not</param>
        public static void RegisterDependency(object instance, Type type, bool isSingleton)
        {
            if (_Builder == null) return;

            // Service are instantiated as singletons
            if (isSingleton)
            {
                _Builder.RegisterInstance(instance).As(type).SingleInstance();
            }
            else
            {
                _Builder.RegisterInstance(instance).As(type);
            }
        }

        /// <summary>
        /// Resolves the desired type, then returns the object that corresponds to this type using the <see cref="_Container"/>
        /// </summary>
        /// <typeparam name="T">The type of the object to be resolved</typeparam>
        /// <returns></returns>
        public static T Resolve<T>() where T : class
        {
            if (_Container == null || !_Container.IsRegistered<T>()) return null;
            return _Container.Resolve<T>();
        }
        
        /// <summary>
        /// Determines if a type has already been registered in the container
        /// </summary>
        /// <typeparam name="T">The type to check</typeparam>
        /// <returns>If it is there true, if it isn't false</returns>
        public static bool IsRegistered<T>() where T : class
        {
            return _Container.IsRegistered<T>();
        }
        
        /// <summary>
        /// Disposes the container
        /// </summary>
        public static void Dispose()
        {
            _Container?.Dispose();
        }

    }
}
