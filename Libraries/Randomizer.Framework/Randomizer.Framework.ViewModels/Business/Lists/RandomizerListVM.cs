
using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using EnigmatiKreations.Framework.Services.Alerts;
using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Services.Alerts;
using Randomizer.Framework.Services.Resources;
using Randomizer.Framework.ViewModels.Business.Items;
using Randomizer.Framework.ViewModels.Commanding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        /// A manager use to link vm data to the model
        /// </summary>
        private ListsManagerVM Manager => Container.Resolve<ListsManagerVM>();

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

        public IGenericCommandAsync<object> AddItemCommand { get; set; }

        public IGenericCommandAsync<object> RemoveItemCommand { get; set; }

        public IGenericCommandAsync<object> UpdateItemCommand { get; set; }

        public ICommandAsync ClearListCommand { get; set; }

        public ICommandAsync RefreshListCommand { get; set; }


        private void InitCommands()
        {
            AddItemCommand = new GenericCommandAsync<object>(AddItem, CanExecuteAddItem);
            RemoveItemCommand = new GenericCommandAsync<object>(RemoveItem, CanExecuteRemoveItem);
            UpdateItemCommand = new GenericCommandAsync<object>(UpdateItem, CanExecuteUpdateItem);
            ClearListCommand = new SimpleCommandAsync(ClearList, CanExecuteClearList);
            RefreshListCommand = new SimpleCommandAsync(RefreshItems, CanExecuteRefreshList);
        }

        private bool CanExecuteRefreshList()
        {
            return true;
        }

        private bool CanExecuteClearList()
        {
            return true;
        }

        public async Task ClearList()
        {
            var success = !(Model.Items.Count == 0);
            if (!success)
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

        private async Task UpdateItem(object args)
        {
            var item = TryToCreateRandomizerItem(args);
            var success = Model.UpdateItem(item.Model);
            if (success == null)
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

        private async Task RemoveItem(object param)
        {
            var item = param as RandomizerItemVM;
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

        private async Task AddItem(object args)
        {
            var item = TryToCreateRandomizerItem(args);
                        
            //if (Model.ContainsItem(item.Model))
            //{
            //    await Container.Resolve<IAlertsService>().DisplayAlert(TextResources.OopsMessage, TextResources.ItemAlreadyExists, TextResources.OKMessage);
            //    return;
            //}

            if (!Model.AddItem(item.Model))
            {
                await Container.Resolve<IAlertsService>().DisplayAlert(TextResources.OopsMessage, TextResources.ErrorAddingItem, TextResources.OKMessage);
                return;
            }

            await RefreshItems();
        }

        #endregion

        #region Methods

        private async Task RefreshItems()
        {
            var items = await GetItems();
            _ItemsVM.Clear();

            foreach (var i in items)
            {
                if (i is TextRandomizerItem t)
                {
                    _ItemsVM.Add(new TextRandomizerItemVM(t));
                }
                else
                {
                    _ItemsVM.Add(new RandomizerItemVM(i));
                }
            }
        }

        private async Task<IEnumerable<RandomizerItem>> GetItems() => await Task.FromResult(Model.Items);


        private RandomizerItemVM TryToCreateRandomizerItem(object args)
        {
            try
            {
                RandomizerItemVM item = default;

                if (args is string name)
                {
                    item = new TextRandomizerItemVM(new TextRandomizerItem()) { Name = name };
                }

                if (args is RandomizerItemVM ri)
                {
                    item = ri;
                }
                return item;
            }
            catch (Exception)
            {
                return null;
            }

        }

        #endregion
    }
}
