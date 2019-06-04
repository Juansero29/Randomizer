using Randomizer.Framework.Services;
using Randomizer.Framework.ViewModels.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Randomizer.Framework.ViewModels
{
    /// <summary>
    /// A base view model for a page
    /// </summary>
    public class BasePageViewModel : BaseViewModel
    {
        #region Fields
        bool _IsBusy = false;
        string _Title = string.Empty;

        #endregion


        #region Constructor(s)
        /// <summary>
        /// Initializes a new instance of the <see cref="BasePageViewModel"/> class.
        /// </summary>
        public BasePageViewModel(INavigationService navService = null)
        {
            if (navService != null)
            {
                NavigationService = navService;
            }
            else
            {
                NavigationService = new ShellNavigationService();
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// The title of this page 
        /// </summary>
        public string Title
        {
            get => _Title;
            set => SetValue(ref _Title, value);
        }

        /// <summary>
        /// Property indicating if the ViewModel is busy loading.
        /// If it's true, show a visual feedback to the user.
        /// </summary>
        public bool IsBusy
        {
            get => _IsBusy;
            set => SetValue(ref _IsBusy, value);
        }

        /// <summary>
        /// Every view model has acces to a NavigationService
        /// </summary>
        public INavigationService NavigationService
        {
            get;
            private set;
        }

        #endregion

    }

}
