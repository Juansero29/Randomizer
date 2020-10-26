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
        /// Initialize the navigation service
        /// </summary>
        /// <param name="navigationRootPage"></param>
        void Initialize(NavigableElement navigationRootPage);


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
        /// <param name="fromModal">Do we go back from a modal page?</param>
        /// <returns>An awaitable task</returns>
        Task GoBackAsync(bool fromModal = false);

    }
}


