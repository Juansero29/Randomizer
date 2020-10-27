using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.ViewModels.Business;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using Randomizer.Framework.Services.Resources;
using EnigmatiKreations.Framework.Services.Navigation;
using EnigmatiKreations.Framework.Services.Alerts;
using Randomizer.Framework.ViewModels.Commanding;
using System.Threading.Tasks;
using Randomizer.Framework.ViewModels.Business.Items;

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
        private string _ToolbarTitle;
        private string _ItemEntryText;
        private bool _IsNew;
        private ListsManagerVM Manager => Container.Resolve<ListsManagerVM>();
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
                    if (value)
                    {
                        ToolbarTitle = TextResources.NewListPageTitle;
                        ListVM = new RandomizerListVM(new SimpleRandomizerList());
                    }
                });
            }
        }

        /// <summary>
        /// Should we show the delete icon on the page?
        /// </summary>
        public bool ShowDeleteListToolbarItem => !IsNew;

        #endregion

        #region Commands
        public IGenericCommandAsync<string> AddItemCommand { get; }
        public IGenericCommandAsync<RandomizerItemVM> RemoveListItemCommand { get; }
        public ICommandAsync SaveListCommand { get; }
        public ICommandAsync DeleteListCommand { get; }
        public ICommandAsync RandomizeCommand { get; }

        #endregion

        #region Constructor(s)
        public ListEditionPageViewModel()
        {
            //MessagingCenter.Subscribe<HomePageViewModel, RandomizerListVM>
            //    (this, HomePageViewModel.MessagingCenterConstants.SelectedList, (sender, selectedList) =>
            //{
            //    ListVM = selectedList;
            //});

            #region InitCommands
            AddItemCommand = new GenericCommandAsync<string>(OnAddItem, CanExecuteAddItem);
            RemoveListItemCommand = new GenericCommandAsync<RandomizerItemVM>(OnRemoveListItem, CanExecuteRemoveItem);
            SaveListCommand = new SimpleCommandAsync(SaveList, CanExecuteSaveList);
            DeleteListCommand = new SimpleCommandAsync(OnDeleteList, CanExecuteDeleteList);
            RandomizeCommand = new SimpleCommandAsync(OnRandomize, CanExecuteRandomize);
            #endregion
        }

        private bool CanExecuteRemoveItem()
        {
            return true;
        }

        private bool CanExecuteRandomize()
        {
            return true;
        }

        private bool CanExecuteDeleteList()
        {
            return true;
        }

        private bool CanExecuteSaveList()
        {
            return ListVM?.Name.Length > 0;
        }

        private bool CanExecuteAddItem()
        {
            return true;
        }

        private bool CanExecuteRandomize(object arg)
        {
            return true;
        }

        private bool CanExecuteDeleteList(object arg)
        {
            return true;
        }

        private bool CanExecuteSaveList(object arg)
        {
            return true;
        }



        private bool CanExecuteAddItem(string arg)
        {
            return true;
        }

        #endregion

        #region Command Methods

        private async Task OnAddItem(string itemName)
        {
            ItemEntryText = "";
            await ListVM.AddItemCommand.ExecuteAsync(new TextRandomizerItemVM(new TextRandomizerItem { Name = itemName }));
        }

        private async Task OnRemoveListItem(RandomizerItemVM item)
        {
            await ListVM.RemoveItem(item);
        }

        public async Task SaveList()
        {
            await Manager.AddList(ListVM);
            await Container.Resolve<INavigationService>().GoBackAsync();
        }

        private async Task OnDeleteList()
        {
            MessagingCenter.Send(this, MessagingCenterConstants.ListDeleted, _ListVM);
            await Container.Resolve<INavigationService>().GoBackAsync();
        }

        private async Task OnRandomize()
        {
            try
            {
                throw new NotImplementedException("TODO #9");
            }
            catch (Exception)
            {
                await Container.Resolve<IAlertsService>().ShowFeatureNotImplementedAlert();
            }
        }

        #endregion

        #region Methods


        public override void UnLoad(object parameter)
        {
            base.UnLoad(parameter);
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
