using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Randomizer.Framework.Services.Navigation
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
        Task GoToAsync(string uri);
        Task PopAsync();
    }

}
