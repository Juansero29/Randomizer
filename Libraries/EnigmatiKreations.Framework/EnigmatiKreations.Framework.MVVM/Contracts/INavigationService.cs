using EnigmatiKreations.Framework.MVVM.BaseViewModels;
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
        /// Goes to the specified uri in the application
        /// </summary>
        /// <param name="uri">The uri where we want to go</param>
        /// <returns>A task that can be awaited</returns>
        /// <remarks>
        /// Parameters can be passed if needed
        /// </remarks>
        Task GoToAsync(string uri);

        /// <summary>
        /// Makes a pop gesture in the app (goes back)
        /// </summary>
        /// <returns>A task that can be awaited</returns>
        Task GoBackAsync();

        /// <summary>
        /// Gets the current page in the application
        /// </summary>
        /// <returns>The page</returns>
        Page GetCurrentPage();



    }
}


