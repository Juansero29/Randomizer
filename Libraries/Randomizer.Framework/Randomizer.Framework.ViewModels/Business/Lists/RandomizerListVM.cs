
using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using EnigmatiKreations.Framework.Services.Alerts;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Services.Alerts;
using Randomizer.Framework.Services.Resources;
using Randomizer.Framework.ViewModels.Business.Items;
using Randomizer.Framework.ViewModels.Commanding;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace Randomizer.Framework.ViewModels.Business
{
    /// <summary>
    /// The ViewModel for a <see cref="RandomizerList"/>
    /// </summary>
    public class RandomizerListVM : BaseViewModel<RandomizerList>
    {

        #region Private Fields
        private ObservableCollection<RandomizerItemVM> _ItemsVM = new ObservableCollection<RandomizerItemVM>();
        private readonly ReadOnlyObservableCollection<RandomizerItemVM> _ReadOnlyItemsVM;
        #endregion

        #region Properties
        /// <summary>
        /// The name of the list
        /// </summary>
        public string Name
        {
            get => Model.Name;
            set => SetValueOnModel(value, Model);
        }

        
        /// <summary>
        /// The list's items
        /// </summary>
        public ReadOnlyObservableCollection<RandomizerItemVM> ItemsVM => _ReadOnlyItemsVM;
        #endregion

        #region Constructor(s)


        public RandomizerListVM(RandomizerList model) : base(model)
        {
            _ReadOnlyItemsVM = new ReadOnlyObservableCollection<RandomizerItemVM>(_ItemsVM);
            InitCommands();
        }


        #endregion


        #region Commands

        public IGenericCommandAsync<RandomizerItemVM> AddItemCommand { get; set; }

        public IGenericCommandAsync<RandomizerItemVM> RemoveItemCommand { get; set; }

        public IGenericCommandAsync<RandomizerItemVM> UpdateItemCommand { get; set; }

        public ICommandAsync ClearListCommand { get; set; }


        private void InitCommands()
        {
            AddItemCommand = new GenericCommandAsync<RandomizerItemVM>(AddItem, CanExecuteAddItem);
            RemoveItemCommand = new GenericCommandAsync<RandomizerItemVM>(RemoveItem, CanExecuteRemoveItem);
            UpdateItemCommand = new GenericCommandAsync<RandomizerItemVM>(UpdateItem, CanExecuteUpdateItem);
            ClearListCommand = new SimpleCommandAsync(ClearList, CanExecuteClearList);
        }


        private bool CanExecuteClearList()
        {
            return true;
        }

        private async void ClearList()
        {
            var success = !(Model.Items.Count == 0);
            if(!success)
            {
                await Container.Resolve<IAlertsService>().DisplayAlert(TextResources.OopsMessage, TextResources.ListNotCleared, TextResources.OKMessage);
                return;
            }
            Model.Items.Clear();


            await RefreshItems();

        }


        private bool CanExecuteUpdateItem()
        {
            return true;
        }

        private async void UpdateItem(RandomizerItemVM obj)
        {
            var success = Model.UpdateItem(obj.Model);
            if(success == null)
            {
                await Container.Resolve<IAlertsService>().DisplayAlert(TextResources.OopsMessage, TextResources.ItemNotUpdated, TextResources.OKMessage);
                return;
            }

            await RefreshItems();
        }

        private bool CanExecuteRemoveItem()
        {
            return true;
        }

        private async void RemoveItem(RandomizerItemVM item)
        {
            var success = Model.ContainsItem(item.Model);
            success &= await Task.FromResult(Model.RemoveItem(item.Model));
            if (!success)
            {
                await Container.Resolve<IAlertsService>().DisplayAlert(TextResources.OopsMessage, TextResources.ItemNotDeleted, TextResources.OKMessage);
                return;
            }
            await RefreshItems();
        }

        private bool CanExecuteAddItem()
        {
            return true;
        }

        private async void AddItem(RandomizerItemVM item)
        {
            if (Model.ContainsItem(item.Model))
            {
                await Container.Resolve<IAlertsService>().DisplayAlert(TextResources.OopsMessage, TextResources.ItemAlreadyExists, TextResources.OKMessage);
                return;
            }

            if(!Model.AddItem(item.Model))
            {
                await Container.Resolve<IAlertsService>().DisplayAlert(TextResources.OopsMessage, TextResources.ErrorAddingItem, TextResources.OKMessage);
                return;
            }

            await RefreshItems();
        }

        #endregion

        #region Methods

        public async Task RefreshItems()
        {
            var items = await GetItems();
            _ItemsVM.Clear();

            foreach (var i in items)
            {
                _ItemsVM.Add(new RandomizerItemVM(i));
            }
        }

        private async Task<IEnumerable<RandomizerItem>> GetItems() => await Task.FromResult(Model.Items);


        public void RemoveAllItems()
        {
            Model.Items.ForEach(i => Model.RemoveItem(i));
        }


        #endregion
    }
}
