using Randomizer.Framework.Services.Alerts;
using Randomizer.Framework.Services.Navigation;
using Randomizer.Framework.ViewModels.BaseViewModels;
using System.Threading.Tasks;

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
        public BasePageViewModel()
        {
            // Resolve the NavigationService instance
            NavigationService = ViewModelLocator.Resolve<INavigationService>();
            // Resolve the AlertService instance
            AlertsService = ViewModelLocator.Resolve<IAlertsService>();
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
        /// If it's true, this should show a visual feedback to the user.
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

        /// <summary>
        /// Every view model has acces to an AlertsServices
        /// </summary>
        public IAlertsService AlertsService
        {
            get;
            private set;
        }

        #endregion

        /// <summary>
        /// Override this method to allow providing navigationData to your ViewModel when navigating
        /// </summary>
        /// <param name="navigationData"></param>
        /// <returns></returns>
        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

    }

}
