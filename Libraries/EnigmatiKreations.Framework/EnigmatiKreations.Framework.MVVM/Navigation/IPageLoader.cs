using System;
using Xamarin.Forms;

namespace EnigmatiKreations.Framework.MVVM.Navigation
{
    public interface IPageLoader
    {
        /// <summary>
        /// Instantiates the corresponding page using the info passed in the route.
        /// The route must be formed of the name of the page in camel case, without the "Page" string at the end
        /// eg.
        ///
        /// For a page that is called "ItemDetailPage"
        /// The route should be "itemDetail"
        ///
        /// For a page that is called "HomePage"
        /// The route should be "home"
        ///
        /// 
        /// </summary>
        /// <param name="route">The string used to find the page and instantiate it </param>
        /// <returns>The new page</returns>
        Page InstantiatePageFromRoute(string route);

        /// Gets or sets the default page.
        /// <remarks>
        /// This page is used when the route didn't correspond to any page.
        /// That way we have a page to show and can manage the exception thrown.
        /// </remarks>
        Type DefaultPageType { get; set; }
    }
}
