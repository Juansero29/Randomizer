using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Utils;
using Randomizer.Framework.ViewModels.Business;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Randomizer.Framework.ViewModels.Pages
{
    /// <summary>
    /// The model of a page that presents a <see cref="RandomizerListVM"/> in edition
    /// </summary>
    [QueryProperty(nameof(IsEditModeParam), "editmode")]
    [QueryProperty(nameof(IsNewParam), "new")]
    public class ListEditionPageViewModel : BasePageViewModel
    {
        #region Fields
        private RandomizerListVM _ListVM;
        private string _ToolbarTitle = Services.Resources.TextResources.NewListPageTitle;
        private string _ItemEntryText;
        private bool _IsEditMode;
        private bool _IsNew;
        #endregion

        #region Query Parameters
        /// <summary>
        /// A parameter indicating if the list in this view model is a new list
        /// </summary>
        public string IsNewParam
        {
            set
            {
                bool.TryParse(value, out bool res);
                IsNew = res;
            }
        }

        /// <summary>
        /// A parameter indicating if this view model is in edit mode
        /// </summary>
        public string IsEditModeParam
        {
            set
            {
                bool.TryParse(value, out bool res);
                IsEditMode = res;
            }
        }
        #endregion

        #region Properties

        /// <summary>
        /// The view model for the list we're editing
        /// </summary>
        public RandomizerListVM ListVM
        {
            get => _ListVM;
            set
            {
                SetValue(ref _ListVM, value);
                if (!IsNew) // If list isn't new, display the list title in the toolbar
                {
                    ToolbarTitle = value.Name;
                }
            }
        }

        /// <summary>
        /// The toolbar's title
        /// </summary>
        public string ToolbarTitle
        {
            get => _ToolbarTitle;
            set => SetValue(ref _ToolbarTitle, value);
        }

        /// <summary>
        /// The item's entry text
        /// </summary>
        public string ItemEntryText
        {
            get => _ItemEntryText;
            set => SetValue(ref _ItemEntryText, value);
        }

        /// <summary>
        /// Is this page in edit mode?
        /// </summary>
        public bool IsEditMode
        {
            get => _IsEditMode;
            set => SetValue(ref _IsEditMode, value, onChanged: () =>
            {
                if (!value) IsNew = value; // If we leave edit mode, IsNew = false
                OnPropertyChanged(nameof(ShowDeleteListToolbarItem));
            });
        }

        /// <summary>
        /// Is this a new list?
        /// </summary>
        public bool IsNew
        {
            get => _IsNew;
            set
            {
                SetValue(ref _IsNew, value, onChanged: () =>
                {
                    if (value) ListVM = new RandomizerListVM();
                });
            }
        }

        /// <summary>
        /// Should we show the delete icon on the page?
        /// </summary>
        public bool ShowDeleteListToolbarItem => !IsNew && IsEditMode;

        #endregion

        #region Commands
        public ICommand AddItemCommand { get; }
        public ICommand RemoveListItemCommand { get; }
        public ICommand SaveListCommand { get; }
        public ICommand EditListCommand { get; }
        public ICommand DeleteListCommand { get; }
        public ICommand RandomizeCommand { get; }
        public ICommand DisappearingCommand { get; }
        #endregion

        #region Constructor(s)
        public ListEditionPageViewModel()
        {
            MessagingCenter.Subscribe<HomePageViewModel, RandomizerListVM>
                (this, HomePageViewModel.MessagingCenterConstants.SelectedList, (sender, selectedList) =>
            {
                ListVM = selectedList;
            });

            #region InitCommands
            AddItemCommand = new Command<string>(OnAddItem);
            RemoveListItemCommand = new Command<IRandomizerItem>(OnRemoveListItem);
            SaveListCommand = new Command(OnSaveList);
            EditListCommand = new Command(OnEnterListEditionMode);
            DeleteListCommand = new Command(OnDeleteList);
            RandomizeCommand = new Command(OnRandomize);
            DisappearingCommand = new Command<EventArgs>(OnDisappearing);
            #endregion
        }

        #endregion

        #region Command Methods

        private void OnAddItem(string itemName)
        {
            ItemEntryText = "";
            ListVM.AddItem(new TextRandomizerItem { Name = itemName });
        }

        private void OnRemoveListItem(IRandomizerItem item)
        {
            ListVM.RemoveItem(item);
        }

        private void OnSaveList()
        {
            ToolbarTitle = _ListVM.Name;
            IsEditMode = false;
            MessagingCenter.Send(this, MessagingCenterConstants.ListSaved, _ListVM);
        }

        private void OnEnterListEditionMode()
        {
            IsEditMode = true;
        }

        private async void OnDeleteList()
        {
            MessagingCenter.Send(this, MessagingCenterConstants.ListDeleted, _ListVM);
            await NavigationService.GoToAsync("///home");
        }

        private void OnRandomize()
        {
            try
            {
                throw new NotImplementedException("TODO #9");
            }
            catch (Exception)
            {
                AlertsService.ShowFeatureNotImplementedAlert();
            }
        }

        private void OnDisappearing(EventArgs args)
        {
            MessagingCenter.Unsubscribe<HomePageViewModel, RandomizerListVM>
                (this, HomePageViewModel.MessagingCenterConstants.SelectedList);
        }

        #endregion

        public static class MessagingCenterConstants
        {
            public const string ListSaved = "ListSaved";
            public const string ListDeleted = "ListDeleted";
        }

    }
}
