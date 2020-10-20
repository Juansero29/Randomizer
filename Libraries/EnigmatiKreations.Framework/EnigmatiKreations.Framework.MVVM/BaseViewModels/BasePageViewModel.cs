using EnigmatiKreations.Framework.MVVM.BaseViewModels.Contract;
using EnigmatiKreations.Framework.Services.Alerts;
using EnigmatiKreations.Framework.Services.Navigation;

using Randomizer.Framework.ViewModels.Commanding;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnigmatiKreations.Framework.MVVM.BaseViewModels
{
    /// <summary>
    /// A base view model for a page
    /// </summary>
    public class BasePageViewModel : BaseViewModel, ILifecycleable
    {
        #region Fields
        bool _IsBusy = false;
        string _Title = string.Empty;
        private bool _IsReload = false;

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


            LoadCommand = new SimpleCommand(PreLoad);
            UnloadCommand = new SimpleCommand(PreUnLoad);
            LoadCommandAsync = new SimpleCommandAsync(PreLoad);
            UnloadCommandAsync = new SimpleCommandAsync(PreUnLoad);
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
        public bool IsDestroy { get; private set; }

        #endregion

        #region Commands
        /// <summary>
        /// Command executed when this view model needs to be loaded
        /// </summary>
        public ICommand LoadCommand
        {
            get => _loadCommand;
            set => SetValue(ref _loadCommand, value);
        }



        private ICommand _LoadCommandAsync;

        /// <summary>
        /// Command executed async when this view model needs to be loaded
        /// </summary>
        public ICommand LoadCommandAsync
        {
            get => _LoadCommandAsync;
            set => SetValue(ref _LoadCommandAsync, value);
        }



        private ICommand _unloadCommandAsync;

        /// <summary>
        /// Command executed async when this view model needs to be unloaded
        /// </summary>
        public ICommand UnloadCommandAsync
        {
            get => _unloadCommandAsync;
            set => SetValue(ref _unloadCommandAsync, value);
        }



        private ICommand _unloadCommand;
        private ICommand _loadCommand;

        /// <summary>
        /// Command executed when this view model needs to be unloaded
        /// </summary>
        public ICommand UnloadCommand
        {
            get => _unloadCommand;
            set => SetValue(ref _unloadCommand, value);
        }
        #endregion

        #region Lifecycle Methods
        private void PreLoad()
        {
            if (_IsReload)
            {
                ReLoad();
            }
            else
            {
                Load();
                _IsReload = true;
            }
        }

        /// <summary>
        /// Method called when the view model needs to be loaded (called only once)
        /// </summary>
        public virtual void Load()
        {

        }

        /// <summary>
        /// Method called when the view model needs to be reloaded (called if the <see cref="LoadCommand"/> is executed twice or more)
        /// </summary>
        public virtual void ReLoad()
        {

        }


        /// <summary>
        /// Method called when the viewmodel needs to be unloaded
        /// </summary>
        private void PreUnLoad()
        {
            UnLoad();
            //Destroy();
        }


        /// <summary>
        /// Method called when the viewmodel needs to be unloaded
        /// Methode surchargeable coté viewmodel
        /// </summary>
        public virtual void UnLoad()
        {

        }

        void ILifecycleable.Destroy()
        {
            PreDestroy();
        }

        internal void PreDestroy()
        {
            IsDestroy = true;
            Destroy();
        }

        /// <summary>
        /// Called when its not possible to come to this viewmodel anymore
        /// Override to liberate mecessay resssources
        /// </summary>
        public virtual void Destroy()
        { }
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

