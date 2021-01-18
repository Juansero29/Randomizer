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
            // NavigationService = Container.Resolve<INavigationService>();
            // Resolve the AlertService instance
            // AlertsService = Container.Resolve<IAlertsService>();

            LoadCommand = new GenericCommand<object>(PreLoad);
            UnloadCommand = new GenericCommand<object>(PreUnLoad);
            LoadCommandAsync = new GenericCommandAsync<object>(PreLoadAsync);
            UnloadCommandAsync = new GenericCommandAsync<object>(PreUnLoadAsync);
        }

        private async Task PreUnLoadAsync(object arg)
        {
            await Task.Run(() => PreUnLoad(arg));
        }

        private async Task PreLoadAsync(object arg)
        {
            await Task.Run(() => PreLoad(arg));
        }

        /// <summary>
        /// Method called when this view model has been navigated to
        /// </summary>
        /// <param name="sender">The page that called this method</param>
        /// <param name="e">The arguments to treat the navigation</param>
        public virtual void Navigated(object sender, object e)
        {
        }

        /// <summary>
        /// Method called when this view model is navigating somewhere
        /// </summary>
        /// <param name="sender">The page that called this methods</param>
        /// <param name="args">The arguments to treat the navigation</param>
        /// <remarks>
        /// Usually used to cancel the navigation of some conditions aren't met
        /// </remarks>
        public virtual void Navigating(object sender, object args)
        {

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

        ///// <summary>
        ///// Every view model has acces to a NavigationService
        ///// </summary>
        //public INavigationService NavigationService
        //{
        //    get;
        //    private set;
        //}

        ///// <summary>
        ///// Every view model has acces to an AlertsServices
        ///// </summary>
        //public IAlertsService AlertsService
        //{
        //    get;
        //    private set;
        //}
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



        private IGenericCommandAsync<object> _LoadCommandAsync;

        /// <summary>
        /// Command executed async when this view model needs to be loaded
        /// </summary>
        public IGenericCommandAsync<object> LoadCommandAsync
        {
            get => _LoadCommandAsync;
            set => SetValue(ref _LoadCommandAsync, value);
        }



        private IGenericCommandAsync<object> _unloadCommandAsync;

        /// <summary>
        /// Command executed async when this view model needs to be unloaded
        /// </summary>
        public IGenericCommandAsync<object> UnloadCommandAsync
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
        private void PreLoad(object parameter)
        {
            if (_IsReload)
            {
                ReLoad(parameter);
            }
            else
            {
                Load(parameter);
                _IsReload = true;
            }
        }

        /// <summary>
        /// Method called when the view model needs to be loaded (called only once)
        /// </summary>
        public virtual void Load(object parameter = null)
        {

        }

        /// <summary>
        /// Method called when the view model needs to be reloaded (called if the <see cref="LoadCommand"/> is executed twice or more)
        /// </summary>
        public virtual void ReLoad(object parameter = null)
        {

        }


        /// <summary>
        /// Method called when the viewmodel needs to be unloaded
        /// </summary>
        private void PreUnLoad(object parameter)
        {
            UnLoad(parameter);
        }


        /// <summary>
        /// Method called when the viewmodel needs to be unloaded
        /// It can be overriden
        /// </summary>
        public virtual void UnLoad(object parameter)
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


     

    }

}

