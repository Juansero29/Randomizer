using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using EnigmatiKreations.Framework.MVVM.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EnigmatiKreations.Framework.Services.Navigation
{
    /// <summary>
    /// Interface defining what our NavigationService should do
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// The page loader, used to instatiate pages when necessary
        /// </summary>
        IPageLoader PageLoader { get; set; }

        /// <summary>
        /// Initialize the navigation service (dependency injection)
        /// </summary>
        /// <param name="navigationRootPage">The root rootpage is the first page the user will go to. Every navigation will be perfomed from here. </param>
        /// <param name="loader">The page loader to use in this navigation service</param>
        void Initialize(NavigableElement navigationRootPage, IPageLoader loader);


        /// <summary>
        /// Gets the current page in the application
        /// </summary>
        /// <returns>The curent pagepage</returns>
        Page GetCurrentPage();


        /// <summary>
        /// Navigates to the specified route
        /// </summary>
        /// <param name="navigationRoute">Route</param>
        /// <param name="args">arguments</param>
        /// <param name="options">options</param>
        /// <returns>An awaitable task</returns>
        Task NavigateToAsync(string navigationRoute, Dictionary<string, string> args = null, NavigationOptions options = null);

        /// <summary>
        /// Makes a pop gesture in the app (goes back)
        /// </summary>
        /// <param name="usingShell">Are we using shell for this navigation?</param>
        /// <param name="fromModal">Do we go back from a modal page?</param>
        /// <returns>An awaitable task</returns>
        Task GoBackAsync(bool usingShell = true, bool fromModal = false);

    }
}


