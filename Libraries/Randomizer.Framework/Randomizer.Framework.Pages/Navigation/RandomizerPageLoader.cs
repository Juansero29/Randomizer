using System;
using System.Globalization;
using System.Text;
using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using EnigmatiKreations.Framework.MVVM.Navigation;
using EnigmatiKreations.Framework.Services.Alerts;
using Xamarin.Forms;

namespace Randomizer.Framework.Pages.Navigation
{
    public class RandomizerPageLoader : IPageLoader
    {
        public RandomizerPageLoader()
        {
        }

        public Type DefaultPageType { get; set; }

        public Page InstantiatePageFromRoute(string route)
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append(char.ToUpper(route[0]) + route.Substring(1));
                var pageName = sb.Append("Page").ToString();
                var pagesAssemblyName = "Randomizer.Framework.Pages";
                var typeFulltQualifiedName = string.Format(CultureInfo.InvariantCulture, $"Randomizer.Pages.{pageName}, {pagesAssemblyName}");
                var pageType = Type.GetType(typeFulltQualifiedName);



                if (pageType == null)
                {
                    pageType = DefaultPageType;
                }

                var page = (Page)Activator.CreateInstance(pageType);


                return page;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
