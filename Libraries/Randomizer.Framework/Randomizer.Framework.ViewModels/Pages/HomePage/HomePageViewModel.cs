using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Randomizer.Framework.ViewModels.Pages
{
    /// <summary>
    /// The ViewModel for the home page of Randomizer
    /// </summary>
    public class HomePageViewModel : BasePageViewModel
    {
        #region Commands
        public ICommand NewRandomizerListCommand { get; }
        #endregion

        #region Constructor(s)
        public HomePageViewModel()
        {
            NewRandomizerListCommand = new Command(OnNewRandomizerList);
        }
        #endregion

        #region Methods
        async private void OnNewRandomizerList()
        {
            var shell = (Application.Current.MainPage as Shell);
            await shell.GoToAsync("/listedition?new=true&editmode=true");
        } 
        #endregion
    }
}
