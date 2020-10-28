using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Services.Navigation;
using EnigmatiKreations.Framework.Utils;
using Randomizer.Framework.ViewModels.Business;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using Randomizer.Framework.Services.Resources;
using Randomizer.Framework.ViewModels.Commanding;
using System.Collections.Generic;
using EnigmatiKreations.Framework.MVVM.Navigation;
using System.Threading.Tasks;
using EnigmatiKreations.Framework.Services.Navigation;

namespace Randomizer.Framework.ViewModels.Pages
{
    /// <summary>
    /// The ViewModel for the home page of Randomizer
    /// </summary>
    public class HomePageViewModel : BasePageViewModel
    { 

        #region Properties


        public ListsManagerVM Manager
        {
            get => Container.Resolve<ListsManagerVM>();
        }


        #endregion

        #region Commands
        public ICommandAsync NewRandomizerListCommand { get; }
        public ICommandAsync RefreshListsCommand { get; }
        public IGenericCommandAsync<RandomizerListVM> ListTappedCommand { get; }
        #endregion

        #region Constructor(s)


        public HomePageViewModel()
        {
            // setting title
            Title = TextResources.YourListsLabel;
            
            #region Commands Init
            NewRandomizerListCommand = new SimpleCommandAsync(NewListButtonPressed, CanExecuteNewListCommand);
            ListTappedCommand = new GenericCommandAsync<RandomizerListVM>(OnListTapped, CanExecuteListTapped);
            RefreshListsCommand = new SimpleCommandAsync(RefreshLists, CanExecuteRefreshLists);
            #endregion
        }

        private async Task RefreshLists()
        {
            await Manager.RefreshLists();
        }

        private bool CanExecuteRefreshLists()
        {
            return true;
        }




        #endregion

        #region Methods

        #region Commands

        public async Task NewListButtonPressed()
        {
            var args = new Dictionary<string, string>
            {
                {  NavigationParameters.IsNew, "true" }
            };

            await Container.Resolve<INavigationService>().NavigateToAsync(NavigationRoutes.ListEditionPage, args);
        }

        async private Task OnListTapped(RandomizerListVM list)
        {
            Manager.CurrentList = list;
            await Container.Resolve<INavigationService>().NavigateToAsync("/listedition");
        }


        private bool CanExecuteListTapped()
        {
            return true;
        }

        private bool CanExecuteNewListCommand()
        {
            return true;
        }
        #endregion

        public override async void Load(object parameter = null)
        {
            base.Load(parameter);

            await RefreshLists();
        }

        public override async void ReLoad(object parameter)
        {
            base.ReLoad(parameter);
            // To use reload we need to first use the LoadingBehaviorLifecycle
            // Reload the list of lists using the DataManager to see if there where any changes
            await RefreshLists();
        }

        public override void Destroy()
        {
            base.Destroy();
        }
        #endregion

        public class MessagingCenterConstants
        {
            public const string SelectedList = "SelectedList";
        }


    }
}
