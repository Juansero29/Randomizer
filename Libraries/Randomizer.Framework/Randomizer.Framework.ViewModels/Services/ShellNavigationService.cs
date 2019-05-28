using Randomizer.Framework.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Randomizer.Framework.ViewModels.Services
{
    public class ShellNavigationService : INavigationService
    {
        #region Private Fields
        private Shell _Shell;
        #endregion

        #region Constructor(s)
        public ShellNavigationService()
        {
            _Shell = Application.Current.MainPage as Shell;
            if (_Shell == null) throw new InvalidOperationException($"For using {nameof(ShellNavigationService)} the property {nameof(Application.Current.MainPage)} must contain a Shell instance.");
        }
        #endregion

        #region Methods
        public async Task GoToAsync(string uri)
        {
            await _Shell.GoToAsync(uri);
        }
        #endregion
    }
}
