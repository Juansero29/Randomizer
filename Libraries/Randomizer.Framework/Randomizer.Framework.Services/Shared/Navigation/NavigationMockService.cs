using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Randomizer.Framework.Services.Navigation;
using Xamarin.Forms;

namespace Randomizer.Framework.Services.Navigation
{
    public class NavigationMockService : INavigationService
    {
        public Task GoToAsync(string uri)
        {
            return Task.CompletedTask;
        }

        public Task PopAsync()
        {
            return Task.CompletedTask;
        }
    }
}
