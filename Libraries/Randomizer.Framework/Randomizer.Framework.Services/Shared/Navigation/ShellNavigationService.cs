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
		private bool _Initialized;

		private NavigableElement _NavigationRoot;

		private Shell _Shell => Application.Current.MainPage as Shell;

		private NavigableElement NavigationRoot
		{
			get => GetShellSection(_NavigationRoot) ?? _NavigationRoot;
			set => _NavigationRoot = value;
		}

		private void Shell_Navigated(object sender, ShellNavigatedEventArgs e)
		{
			var page = sender as Page;
			var vm = page?.BindingContext as BasePageViewModel;
			vm.Navigated(sender, e);
			Debug.WriteLine($"Navigated to {e.Current.Location} from {e.Previous?.Location} navigation type{e.Source.ToString()}");
		}

		private void Shell_Navigating(object sender, ShellNavigatingEventArgs e)
		{
			var page = sender as Page;
			var vm = page?.BindingContext as BasePageViewModel;
			vm.Navigating(sender, e);
		}

		private async Task NavigateShellAsync(string navigationRoute, Dictionary<string, string> args, bool animated = true)
		{
			var queryString = args.AsQueryString();
			navigationRoute = navigationRoute + queryString;
			Debug.WriteLine($"Shell Navigating to {navigationRoute}");
			await _Shell.GoToAsync(navigationRoute, true);
		}

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

        private Type GetPageForNavigationRoute(string route)
        {
			var sb = new StringBuilder();
			sb.Append(char.ToUpper(route[0]) + route.Substring(1));
            var pageName = sb.Append("Page").ToString();
			var pageAssemblyName = "Randomizer.Framework.Pages, Version = 1.0.0.0, Culture = neutral, PublicKeyToken = null";
			// var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, $"Randomizer.Pages.{pageName}, {viewModelAssemblyName}");
			// var viewType = Type.GetType(viewAssemblyName);

			Assembly a = Assembly.Load(pageAssemblyName);
			var pageType = a?.GetType(pageName);
            return pageType;
        }

        private Page CreatePage(string navigationroute)
        {
            Type pageType = GetPageForNavigationRoute(navigationroute);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {navigationroute}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            return page;
        }

        public async Task NavigateToAsync(string navigationRoute, Dictionary<string, string> args = null, NavigationOptions options = null)
		{

			var page = CreatePage(navigationRoute);
			var vm = page.BindingContext as BasePageViewModel;
			options = options ?? NavigationOptions.Default();

			if (page == null)
			{
				Debug.WriteLine($"Could not create page for {navigationRoute}: Assuming this is a shell route...");
				await NavigateShellAsync(navigationRoute, args, options.Animated);
				return;
			}

			//if (page is MvvmContentPage mvvmPage)
			//{
			//	mvvmPage.NavigationArgs = args;
			//}

			if (options.CloseFlyout)
			{
				await Task.Delay(5).ContinueWith((t) => _Shell.FlyoutIsPresented = false);
			}

			if (!options.Modal)
			{
				await NavigationRoot.Navigation.PushAsync(page, options.Animated).ConfigureAwait(false);
			}
			else
			{
				await NavigationRoot.Navigation.PushModalAsync(page, options.Animated).ConfigureAwait(false);
			}
		}

		public void Initialize(NavigableElement navigationRootPage)
		{
			if (_Initialized)
			{
				return;
			}

			_Initialized = true;
			NavigationRoot = navigationRootPage;
			_Shell.Navigating += Shell_Navigating;
			_Shell.Navigated += Shell_Navigated;
		}

		// Provides a navigatable section for elements which aren't explicitly defined within the Shell. For example,
		// if it's accessed from the fly-out through a MenuItem but it doesn't belong to any section
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
            throw new NotImplementedException();
        }
    }

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
}
