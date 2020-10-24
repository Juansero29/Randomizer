using EnigmatiKreations.Framework.Managers.Contract;
using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Persistence;
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
        private ObservableCollection<RandomizerListVM> _ListsVM;
        #endregion


        #region Properties


        private int _RequestedListsCount;

        /// <summary>
        /// The number of lists to load
        /// </summary>
        public int RequestedListsCount
        {
            get => _RequestedListsCount;
            set => SetValue(ref _RequestedListsCount, value);
        }



        public ObservableCollection<RandomizerListVM> ListsVM { get => _ListsVM; }

        #endregion


        #region Commands

        public ICommand AddListCommand { get; set; }

        #endregion

        public ListsManagerVM(ListsManager model) : base(model)
        {
            InitCommands();
        }

        private void InitCommands()
        {
            AddListCommand = new GenericCommandAsync<RandomizerListVM>(AddList, CanExecuteAddList);
        }



        public async Task GetLists(int startIndex)
        {
            var lists = await Model.GetLists(startIndex, RequestedListsCount);

            ListsVM.Clear();
            
            foreach(var l in lists)
            {
                ListsVM.Add(new RandomizerListVM(l));
            }
        }

        private bool CanExecuteAddList()
        {
            return true;
        }

        public async void AddList(RandomizerListVM listVM)
        {
            var r = await Model.AddList(listVM.Model);
            if (r == null) return;
            ListsVM.Add(listVM);
        }
    }
}
