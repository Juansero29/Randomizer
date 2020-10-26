using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using EnigmatiKreations.Framework.MVVM.Navigation;
using EnigmatiKreations.Framework.Services.Navigation;
using Randomizer.Framework.Services.Navigation;
using Xamarin.Forms;

namespace Randomizer.Framework.Services.Navigation
{
    public class NavigationMockService : INavigationService
    {
        public BasePageViewModel PreviousPageViewModel => throw new NotImplementedException();

        public Page GetCurrentPage()
        {
            return Application.Current.MainPage;
        }

        public Task GoBackAsync(bool fromModal = false)
        {
            throw new NotImplementedException();
        }

        public void Initialize(NavigableElement navigationRootPage)
        {
            throw new NotImplementedException();
        }

        public Task NavigateToAsync(string navigationRoute, Dictionary<string, string> args = null, NavigationOptions options = null)
        {
            throw new NotImplementedException();
        }
    }
}
