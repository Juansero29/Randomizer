using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Randomizer.Framework.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public ICommand NewRandomizerListCommand { get; }

        public HomePageViewModel()
        {
            NewRandomizerListCommand = new Command(NewRandomizerList); 
        }

        async private void NewRandomizerList()
        {
            var shell = (Application.Current.MainPage as Shell);
            await shell.GoToAsync("/listedition?editmode=true");
        }
    }
}
