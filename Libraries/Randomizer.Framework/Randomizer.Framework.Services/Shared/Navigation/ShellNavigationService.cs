using Randomizer.Framework.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Randomizer.Framework.Services.Navigation
{
    public class ShellNavigationService : INavigationService
    {
        #region Private Fields
        private Shell _Shell;
        #endregion

        public Shell Shell
        {
            get
            {
                if (_Shell == null)
                {
                    _Shell = Application.Current.MainPage as Shell;
                    if (_Shell == null) throw new InvalidOperationException($"For using {nameof(ShellNavigationService)} the property {nameof(Application.Current.MainPage)} must contain a Shell instance.");
                }

                return _Shell;
            }
            set
            {
                if (value == null)
                {
                    _Shell = Application.Current.MainPage as Shell;
                    if (_Shell == null) throw new InvalidOperationException($"For using {nameof(ShellNavigationService)} the property {nameof(Application.Current.MainPage)} must contain a Shell instance.");
                }
                else
                {
                    _Shell = value;
                }
            }
        }


        #region Constructor(s)
        public ShellNavigationService()
        {
        }

        #endregion

        #region Methods
        public async Task GoToAsync(string uri)
        {
            await Shell.GoToAsync(uri);
        }

        public async Task PopAsync()
        {
            await Shell.Navigation.PopAsync();
        }

        #endregion
    }
}
