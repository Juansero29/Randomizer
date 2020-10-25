using EnigmatiKreations.Framework.Controls.Shared;
using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using EnigmatiKreations.Framework.Services.Navigation;
using Randomizer.Framework.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Randomizer.Framework.Services.Navigation
{
    public class ShellNavigationService : INavigationService
    {
        #region Private Fields
        private Shell _Shell;
        #endregion

        public Shell Shell
        {
            get
            {
                if (_Shell == null)
                {
                    _Shell = Application.Current.MainPage as Shell;
                    if (_Shell == null) throw new InvalidOperationException($"For using {nameof(ShellNavigationService)} the property {nameof(Application.Current.MainPage)} must contain a Shell instance.");
                }

                return _Shell;
            }
            set
            {
                if (value == null)
                {
                    _Shell = Application.Current.MainPage as Shell;
                    if (_Shell == null) throw new InvalidOperationException($"For using {nameof(ShellNavigationService)} the property {nameof(Application.Current.MainPage)} must contain a Shell instance.");
                }
                else
                {
                    _Shell = value;
                }
            }
        }

        public BasePageViewModel PreviousPageViewModel => (Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault()?.BindingContext as BasePageViewModel);


        #region Constructor(s)
        public ShellNavigationService()
        {
        }

        #endregion

        #region Methods
        public async Task GoToAsync(string uri)
        {
            await Shell.GoToAsync(uri);
        }

        public async Task GoBackAsync()
        {
            await Shell.Navigation.PopAsync();
        }

        public Page GetCurrentPage()
        {
            return (Shell.Current?.CurrentItem?.CurrentItem as IShellSectionController)?.PresentedPage;
        }




        //private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        //{


        //    Page page = CreatePage(viewModelType, parameter);


        //    var navigationPage = Application.Current.MainPage as Shell;
        //    if (navigationPage != null)
        //    {
        //        await navigationPage.Navigation.PushAsync(page);
        //    }
        //    else
        //    {
        //        Application.Current.MainPage = new NavigationPageEK(page);
        //    }

        //    await PreviousPageViewModel.UnloadCommandAsync.ExecuteAsync(parameter);
        //    await (page.BindingContext as BasePageViewModel).LoadCommandAsync.ExecuteAsync(parameter);
        //}

        //private Type GetPageTypeForViewModel(Type viewModelType)
        //{
        //    var viewName = viewModelType.FullName.Replace("ViewModel", string.Empty);
        //    var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
        //    var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
        //    var viewType = Type.GetType(viewAssemblyName);
        //    return viewType;
        //}

        //private Page CreatePage(Type viewModelType, object parameter)
        //{
        //    Type pageType = GetPageTypeForViewModel(viewModelType);
        //    if (pageType == null)
        //    {
        //        throw new Exception($"Cannot locate page type for {viewModelType}");
        //    }

        //    Page page = Activator.CreateInstance(pageType) as Page;
        //    return page;
        //}



        #endregion
    }
}
