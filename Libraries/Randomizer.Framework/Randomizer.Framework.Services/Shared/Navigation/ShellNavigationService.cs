using EnigmatiKreations.Framework.Controls.Shared;
using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using EnigmatiKreations.Framework.MVVM.Navigation;
using EnigmatiKreations.Framework.Services.Navigation;
using Randomizer.Framework.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private Page _CurrentPage;
        private NavigableElement _NavigationRoot;
        private Shell _Shell;

        #endregion

        #region Properties

        public IPageLoader PageLoader { get; set; }

        private NavigableElement NavigationRoot
        {
            get => GetShellSection(_NavigationRoot) ?? _NavigationRoot;
            set => _NavigationRoot = value;
        }
        #endregion

        #region Constructor

        public void Initialize(NavigableElement navigationRootPage, IPageLoader loader)
        {
            NavigationRoot = navigationRootPage;
            PageLoader = loader;
            _Shell = Application.Current.MainPage as Shell;
            if(_Shell == null)
            {
                throw new InvalidOperationException($"Application.Current.MainPage must be assigned to a Shell instance to use Randomizer.Framework.Services.Navigation.ShellNavigationService");
            }
            _Shell.Navigating += Shell_Navigating;
            _Shell.Navigated += Shell_Navigated;
        }

        #endregion

        #region Methods


        #region Shell Navigation
        private void Shell_Navigated(object sender, ShellNavigatedEventArgs e)
        {
            var page = sender as Page;
            var vm = page?.BindingContext as BasePageViewModel;
            vm?.Navigated(sender, e);
            Debug.WriteLine($"Navigated to {e.Current.Location} from {e.Previous?.Location}m navigation type {e.Source}");
        }

        private void Shell_Navigating(object sender, ShellNavigatingEventArgs e)
        {
            var page = sender as Page;
            var vm = page?.BindingContext as BasePageViewModel;
            vm?.Navigating(sender, e);
            Debug.WriteLine($"Navigating from {e.Current.Location} to {e.Target?.Location}, navigation type{ e.Source}");
        }

        private async Task ShellNavigateAsync(string navigationRoute, Dictionary<string, string> args, bool animated = true)
        {
            var queryString = args.AsQueryString();
            navigationRoute = navigationRoute + queryString;
            Debug.WriteLine($"Shell Navigating to {navigationRoute}");
            await _Shell.GoToAsync(navigationRoute, true);
        } 
        #endregion

        public async Task GoBackAsync(bool fromModal = false)
        {
            if (!fromModal)
            {
                await NavigationRoot.Navigation.PopAsync();
            }
            else
            {
                await NavigationRoot.Navigation.PopModalAsync();
            }
        }

        private Page CreatePage(string navigationroute)
        {
            var page = PageLoader.InstantiatePageFromRoute(navigationroute);
            if (page == null)
            {
                throw new Exception($"Cannot locate page type for {navigationroute}");
            }
            return page;
        }

        public async Task NavigateToAsync(string navigationRoute, Dictionary<string, string> args = null, NavigationOptions options = null)
        {
            // if options is null, then use default options
            options ??= NavigationOptions.Default();


            if (options.CloseFlyout)
            {
                await Task.Delay(5).ContinueWith((t) => _Shell.FlyoutIsPresented = false);
            }

            // if options say not to use shell navigation
            if(options is ShellNavigationOptions sno && !sno.UseShellNavigation)
            {
                // create the page manually
                var page = CreatePage(navigationRoute);
                if (page == null)
                {
                    Debug.WriteLine($"Could not create page for {navigationRoute}. Not navigating.");
                    return;
                }
                // navigate from the navigation root to the new page
                await RootNavigateToAsync(options, page).ConfigureAwait(false);
            }

            await ShellNavigateAsync(navigationRoute, args, options.Animated);

        }

        /// <summary>
        /// Navigates to a page checking the navigation options using the root page push async
        /// </summary>
        /// <param name="options">The navigation options</param>
        /// <param name="page">The target page</param>
        /// <returns></returns>
        private async Task RootNavigateToAsync(NavigationOptions options, Page page)
        {
            //if not modal page
            if (!options.Modal)
            {
                await NavigationRoot.Navigation.PushAsync(page, options.Animated).ConfigureAwait(false);
            }
            else
            {
                // if modal page
                await NavigationRoot.Navigation.PushModalAsync(page, options.Animated).ConfigureAwait(false);
            }
        }



        /// <summary>
        /// Provides a navigatable section for elements which aren't explicitly defined within
        /// the Shell. For example, if it's accessed from the fly-out through a MenuItem but it
        /// doesn't belong to any section
        /// </summary>
        /// <param name="element">The element to proviade a shell section</param>
        /// <returns>The shell section</returns>
        internal static ShellSection GetShellSection(Element element)
        {
            if (element == null)
            {
                return null;
            }

            var parent = element;
            var parentSection = parent as ShellSection;

            while (parentSection == null && parent != null)
            {
                parent = parent.Parent;
                parentSection = parent as ShellSection;
            }

            return parentSection;
        }

        public Page GetCurrentPage()
        {
            return (Shell.Current?.CurrentItem?.CurrentItem as IShellSectionController)?.PresentedPage;
        }

        #endregion
    }

    public class ShellNavigationOptions : NavigationOptions
    {
        public ShellNavigationOptions(bool useShellNavigation = true) : base()
        {
            UseShellNavigation = useShellNavigation;
        }
        public bool UseShellNavigation { get; set; } = true;
    }
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



//public class ShellNavigationService : INavigationService
//   {
//       #region Private Fields
//       private Shell _Shell;
//       #endregion

//       public Shell Shell
//       {
//           get
//           {
//               if (_Shell == null)
//               {
//                   _Shell = Application.Current.MainPage as Shell;
//                   if (_Shell == null) throw new InvalidOperationException($"For using {nameof(ShellNavigationService)} the property {nameof(Application.Current.MainPage)} must contain a Shell instance.");
//               }

//               return _Shell;
//           }
//           set
//           {
//               if (value == null)
//               {
//                   _Shell = Application.Current.MainPage as Shell;
//                   if (_Shell == null) throw new InvalidOperationException($"For using {nameof(ShellNavigationService)} the property {nameof(Application.Current.MainPage)} must contain a Shell instance.");
//               }
//               else
//               {
//                   _Shell = value;
//               }
//           }
//       }

//       public BasePageViewModel PreviousPageViewModel => (Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault()?.BindingContext as BasePageViewModel);


//       #region Constructor(s)
//       public ShellNavigationService()
//       {
//       }

//       #endregion

//       #region Methods


//       public async Task GoBackAsync()
//       {
//           await Shell.Navigation.PopAsync();
//       }

//       public Page GetCurrentPage()
//       {
//           return (Shell.Current?.CurrentItem?.CurrentItem as IShellSectionController)?.PresentedPage;
//       }

//       public void Initialize(NavigableElement navigationRootPage)
//       {
//           throw new NotImplementedException();
//       }

//       public async Task NavigateToAsync(string navigationRoute, Dictionary<string, string> args = null, NavigationOptions options = null)
//       {
//           await Shell.GoToAsync(navigationRoute);
//       }

//       public Task GoBackAsync(bool fromModal = false)
//       {
//           throw new NotImplementedException();
//       }







//       #endregion
//   }
