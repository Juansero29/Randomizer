using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnigmatiKreations.Framework.MVVM.BaseViewModels;
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

        public Task GoToAsync(string uri)
        {
            return Task.CompletedTask;
        }

        public Task GoBackAsync()
        {
            return Task.CompletedTask;
        }

        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : BasePageViewModel
        {
            throw new NotImplementedException();
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BasePageViewModel
        {
            throw new NotImplementedException();
        }

        public Task RemoveLastFromBackStackAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveBackStackAsync()
        {
            throw new NotImplementedException();
        }
    }
}
