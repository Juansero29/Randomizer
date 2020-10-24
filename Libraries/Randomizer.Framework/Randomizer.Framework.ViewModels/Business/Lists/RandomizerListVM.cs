using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Services.Alerts;
using Randomizer.Framework.Services.Resources;
using Randomizer.Framework.ViewModels.Commanding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        public ObservableCollection<RandomizerItemVM> ItemsVM => _ItemsVM;
        #endregion

        #region Constructor(s)


        public RandomizerListVM(RandomizerList model) : base(model)
        {
            InitCommands();
        }


        #endregion


        #region Commands

        public ICommand AddItemCommand { get; set; }

        public ICommand RemoveItemCommand { get; set; }

        public ICommand UpdateItemCommand { get; set; }

        public ICommand ClearListCommand { get; set; }


        private void InitCommands()
        {
            AddItemCommand = new GenericCommandAsync<RandomizerItem>(AddItem, CanExecuteAddItem);
            RemoveItemCommand = new GenericCommandAsync<RandomizerItem>(RemoveItem, CanExecuteRemoveItem);
            UpdateItemCommand = new GenericCommandAsync<RandomizerItem>(UpdateItem, CanExecuteUpdateItem);
            ClearListCommand = new SimpleCommandAsync(ClearList, CanExecuteClearList);
        }


        private bool CanExecuteClearList()
        {
            return true;
        }

        private async void ClearList()
        {

        }


        private bool CanExecuteUpdateItem()
        {
            return true;
        }

        private void UpdateItem(RandomizerItem obj)
        {
        }

        private bool CanExecuteRemoveItem()
        {
            return true;
        }

        private async void RemoveItem(RandomizerItem item)
        {
            var success = await Task.FromResult(Model.RemoveItem(item));
            if (!success) return;

        }

        private bool CanExecuteAddItem()
        {
            return true;
        }

        public async void AddItem(RandomizerItem item)
        {
            if (Model.ContainsItem(item))
            {
                await Container.Resolve<AlertsService>().DisplayAlert(TextResources.OopsMessage, TextResources.ItemAlreadyExists, TextResources.OKMessage);
                return;
            }

            if(!Model.AddItem(item))
            {
                await Container.Resolve<AlertsService>().DisplayAlert(TextResources.OopsMessage, TextResources.ErrorAddingItem, TextResources.OKMessage);
                return;
            }

            await RefreshItems();
        }

        #endregion

        #region Methods

        public async Task RefreshItems()
        {
            var items = await GetItems();
            ItemsVM.Clear();

            foreach (var i in items)
            {
                ItemsVM.Add(new RandomizerItemVM(i));
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
