﻿using Randomizer.Framework.Models;
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

namespace Randomizer.Framework.ViewModels.Pages
{
    /// <summary>
    /// The ViewModel for the home page of Randomizer
    /// </summary>
    public class HomePageViewModel : BasePageViewModel
    {

        #region Private Fields
        private ObservableCollection<RandomizerListVM> _Lists = new ObservableCollection<RandomizerListVM>();
        #endregion

        #region Properties

        /// <summary>
        /// The collection of lists to show in the home page
        /// </summary>
        public ObservableCollection<RandomizerListVM> Lists
        {
            get => _Lists;
            set => SetValue(ref _Lists, value);
        }

        #endregion

        #region Commands
        public ICommandAsync NewRandomizerListCommand { get; }
        public IGenericCommandAsync<RandomizerListVM> ListTappedCommand { get; }
        #endregion

        #region Constructor(s)


        public HomePageViewModel()
        {
            // setting title
            Title = TextResources.YourListsLabel;

            //MessagingCenterExtensions.UnitarySubscribe<ListEditionPageViewModel, RandomizerListVM, HomePageViewModel>(this,
            //ListEditionPageViewModel.MessagingCenterConstants.ListSaved, (sender, newList) =>
            //{
            //    if (Lists.Contains(newList)) return;
            //    Lists.Add(newList);
            //});

            //MessagingCenterExtensions.UnitarySubscribe<ListEditionPageViewModel, RandomizerListVM, HomePageViewModel>(this,
            //ListEditionPageViewModel.MessagingCenterConstants.ListDeleted, (sender, deletedList) =>
            //{
            //    Lists.Remove(deletedList);
            //});

            #region Commands Init
            NewRandomizerListCommand = new SimpleCommandAsync(OnNewRandomizerList, CanExecuteNewListCommand);
            ListTappedCommand = new GenericCommandAsync<RandomizerListVM>(OnListTapped, CanExecuteListTapped);
            #endregion
        }




        #endregion

        #region Methods

        #region Commands
         private void OnNewRandomizerList()
        {

            //await NavigationService.NavigateToAsync("/listedition?new=true&editmode=true");
            
            var args = new Dictionary<string, string>
            {
                {  NavigationParameters.IsNew, "true" }
            };



            NavigationService.NavigateToAsync(NavigationRoutes.ListEditionPage, args);
        }

        async private void OnListTapped(RandomizerListVM list)
        {
            await NavigationService.NavigateToAsync("/listedition");
            MessagingCenter.Send(this, MessagingCenterConstants.SelectedList, list);
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


        public override void ReLoad(object parameter)
        {
            base.ReLoad(parameter);
            // To use reload we need to first use the LoadingBehaviorLifecycle
            // Reload the list of lists using the DataManager to see if there where any changes
        }

        public override void Destroy()
        {
            base.Destroy();
            MessagingCenter.Unsubscribe<ListEditionPageViewModel, RandomizerListVM> (this, ListEditionPageViewModel.MessagingCenterConstants.ListDeleted);
        }
        #endregion

        public class MessagingCenterConstants
        {
            public const string SelectedList = "SelectedList";
        }


    }
}
