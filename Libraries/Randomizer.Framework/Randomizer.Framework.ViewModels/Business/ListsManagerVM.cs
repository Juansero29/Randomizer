using EnigmatiKreations.Framework.Managers.Contract;
using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using EnigmatiKreations.Framework.Services.Alerts;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Persistence;
using Randomizer.Framework.Services.Alerts;
using Randomizer.Framework.Services.Resources;
using Randomizer.Framework.ViewModels.Commanding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Randomizer.Framework.ViewModels.Business
{
    /// <summary>
    /// The view model exposing the lists in the app
    /// </summary>
    public class ListsManagerVM : BaseViewModel<ListsManager>
    {

        #region Private Fields
        private ObservableCollection<RandomizerListVM> _ListsVM = new ObservableCollection<RandomizerListVM>();
        private readonly ReadOnlyObservableCollection<RandomizerListVM> _ReadOnlyListsVM;
        #endregion


        #region Properties


        private int _RequestedListsCount = 100;

        /// <summary>
        /// The number of lists to load
        /// </summary>
        public int RequestedListsCount
        {
            get => _RequestedListsCount;
            set => SetValue(ref _RequestedListsCount, value);
        }

        /// <summary>
        ///  The current list for the app
        /// </summary>
        public RandomizerListVM CurrentList
        {
            get => new RandomizerListVM(Model.CurrentList);
            set
            {
                if (!(value is RandomizerListVM list)) return;
                Model.CurrentList = list.Model;
            }
        }

        /// <summary>
        /// All the lists 
        /// </summary>
        public ReadOnlyObservableCollection<RandomizerListVM> ListsVM { get => _ReadOnlyListsVM; }

        #endregion


        #region Commands

        public IGenericCommandAsync<RandomizerListVM> AddListCommand { get; set; }
        public IGenericCommandAsync<RandomizerListVM> UpdateListCommand { get; set; }
        public IGenericCommandAsync<RandomizerListVM> DeleteListCommand { get; set; }

        #endregion

        public ListsManagerVM(ListsManager model) : base(model)
        {
            _ReadOnlyListsVM = new ReadOnlyObservableCollection<RandomizerListVM>(_ListsVM);

            InitCommands();
        }

        private void InitCommands()
        {
            AddListCommand = new GenericCommandAsync<RandomizerListVM>(AddList, CanExecuteAddList);
            UpdateListCommand = new GenericCommandAsync<RandomizerListVM>(UpdateList, CanExecuteUpdateList);
            DeleteListCommand = new GenericCommandAsync<RandomizerListVM>(DeleteList, CanExecuteDeleteList);
        }

  
        public async Task RefreshLists(int startIndex = 0)
        {
            var lists = await Model.GetLists(startIndex, RequestedListsCount);

            _ListsVM.Clear();

            foreach (var l in lists)
            {
                _ListsVM.Add(new RandomizerListVM(l));
            }
        }

        private bool CanExecuteDeleteList()
        {
            return true;
        }

        private async Task DeleteList(RandomizerListVM obj)
        {
            var r = await Model.RemoveList(obj.Model);
            if(!r)
            {
                await Container.Resolve<IAlertsService>().DisplayAlert(TextResources.OopsMessage, TextResources.ItemNotDeleted, TextResources.OKMessage);
                return;
            }
            await RefreshLists();
        }



        private bool CanExecuteUpdateList()
        {
            return true;
        }

        private async Task UpdateList(RandomizerListVM item)
        {
            var r = await Model.Update(item.Model.Id, item.Model);
            if (r == null)
            {
                await Container.Resolve<IAlertsService>().DisplayAlert(TextResources.OopsMessage, TextResources.ItemNotUpdated, TextResources.OKMessage);
                return;
            }

            await RefreshLists();

        }


        private bool CanExecuteAddList()
        {
            return true;
        }

        private async Task AddList(RandomizerListVM listVM)
        {
            var r = await Model.AddList(listVM.Model);
            if (r == null)
            {
                await Container.Resolve<IAlertsService>().DisplayAlert(TextResources.OopsMessage, TextResources.ItemAlreadyExists, TextResources.OKMessage);
                return;
            }

            await RefreshLists();
        }
    }
}
