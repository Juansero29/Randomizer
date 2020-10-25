using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.ViewModels.Business;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using Randomizer.Framework.Services.Resources;

namespace Randomizer.Framework.ViewModels.Pages
{
    /// <summary>
    /// The view model of a page that presents a <see cref="RandomizerListVM"/> in edition
    /// </summary>
    [QueryProperty(nameof(IsNewParam), "new")]
    public class ListEditionPageViewModel : BasePageViewModel
    {
        #region Fields
        private RandomizerListVM _ListVM;
        private string _ToolbarTitle = TextResources.NewListPageTitle;
        private string _ItemEntryText;
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

                if (value == null) return;
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
        /// Is this a new list?
        /// </summary>
        public bool IsNew
        {
            get => _IsNew;
            set
            {
                SetValue(ref _IsNew, value, onChanged: () =>
                {
                    //if (value) ListVM = new RandomizerListVM();
                });
            }
        }

        /// <summary>
        /// Should we show the delete icon on the page?
        /// </summary>
        public bool ShowDeleteListToolbarItem => !IsNew;

        #endregion

        #region Commands
        public ICommand AddItemCommand { get; }
        public ICommand RemoveListItemCommand { get; }
        public ICommand SaveListCommand { get; }

        public ICommand DeleteListCommand { get; }
        public ICommand RandomizeCommand { get; }
        
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
            RemoveListItemCommand = new Command<RandomizerItem>(OnRemoveListItem);
            SaveListCommand = new Command(OnSaveList);
            DeleteListCommand = new Command(OnDeleteList);
            RandomizeCommand = new Command(OnRandomize);
            #endregion
        }

        #endregion

        #region Command Methods

        private void OnAddItem(string itemName)
        {
            ItemEntryText = "";
            ListVM.AddItemCommand.Execute(new TextRandomizerItem { Name = itemName });
        }

        private void OnRemoveListItem(RandomizerItem item)
        {
            // ListVM.RemoveItem(item);
        }

        private void OnSaveList()
        {
            ToolbarTitle = _ListVM.Name;
            MessagingCenter.Send(this, MessagingCenterConstants.ListSaved, _ListVM);
        }

        private async void OnDeleteList()
        {
            MessagingCenter.Send(this, MessagingCenterConstants.ListDeleted, _ListVM);
            await NavigationService.PopAsync();
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

        #endregion

        #region Methods


        public override void UnLoad()
        {
            base.UnLoad();
            MessagingCenter.Unsubscribe<HomePageViewModel, RandomizerListVM>(this, HomePageViewModel.MessagingCenterConstants.SelectedList);
        }

        public override void Destroy()
        {

            base.Destroy();
            MessagingCenter.Unsubscribe<HomePageViewModel, RandomizerListVM>(this, HomePageViewModel.MessagingCenterConstants.SelectedList);
        }

        #endregion

        public static class MessagingCenterConstants
        {
            public const string ListSaved = "ListSaved";
            public const string ListDeleted = "ListDeleted";
        }

    }
}
