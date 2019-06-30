using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Services.Navigation;
using Randomizer.Framework.Utils;
using Randomizer.Framework.ViewModels.Business;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

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
        public ICommand NewRandomizerListCommand { get; }
        public ICommand ListTappedCommand { get; }
        #endregion

        #region Constructor(s)


        public HomePageViewModel() : base()
        {
            InitListWithStubData();

            MessagingCenterExtensions.UnitarySubscribe<ListEditionPageViewModel, RandomizerListVM, HomePageViewModel>(this,
            ListEditionPageViewModel.MessagingCenterConstants.ListSaved, (sender, newList) =>
            {
                if (Lists.Contains(newList)) return;
                Lists.Add(newList);
            });

            MessagingCenterExtensions.UnitarySubscribe<ListEditionPageViewModel, RandomizerListVM, HomePageViewModel>(this,
            ListEditionPageViewModel.MessagingCenterConstants.ListDeleted, (sender, deletedList) =>
            {
                Lists.Remove(deletedList);
            });

            #region Commands Init
            NewRandomizerListCommand = new Command(OnNewRandomizerList);
            ListTappedCommand = new Command<RandomizerListVM>(OnListTapped);
            #endregion
        }

        private void InitListWithStubData()
        {
            Lists.Add(new RandomizerListVM() { Name = "List #1" });
            Lists.Add(new RandomizerListVM() { Name = "List #2" });
            Lists.Add(new RandomizerListVM() { Name = "List #3" });
            Lists.Add(new RandomizerListVM() { Name = "List #4" });
            Lists.Add(new RandomizerListVM() { Name = "List #5" });
            Lists.Add(new RandomizerListVM() { Name = "List #6" });
        }

        #endregion

        #region Methods
        async private void OnNewRandomizerList()
        {
            await NavigationService.GoToAsync("/listedition?new=true&editmode=true");
        }

        async private void OnListTapped(RandomizerListVM list)
        {
            await NavigationService.GoToAsync("/listedition");
            MessagingCenter.Send(this, MessagingCenterConstants.SelectedList, list);
        }
        #endregion

        public class MessagingCenterConstants
        {
            public const string SelectedList = "SelectedList";
        }
    }
}
