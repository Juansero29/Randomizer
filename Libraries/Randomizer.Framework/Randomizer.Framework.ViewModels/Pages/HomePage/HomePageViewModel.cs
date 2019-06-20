using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Services.Navigation;
using Randomizer.Framework.Utils;
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
        private ObservableCollection<IRandomizerList> _Lists = new ObservableCollection<IRandomizerList>();
        #endregion

        #region Properties

        /// <summary>
        /// The collection of lists to show in the home page
        /// </summary>
        public ObservableCollection<IRandomizerList> Lists
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

            MessagingCenterExtensions.UnitarySubscribe<ListEditionPageViewModel, IRandomizerList, HomePageViewModel>(this,
            ListEditionPageViewModel.MessagingCenterConstants.ListSaved, (sender, newList) =>
            {
                // TODO update titles when saving list (however the id doesnt necessarily change)
                // TODO navigation from listeditionpage to homepage :
                //          If navigating back, and page is edited but not saved, show dialog asking to discard or save

                var oldItem = _Lists.FirstOrDefault(x => x.Id.Equals(newList.Id));
                if (oldItem != null)
                {
                    int index = _Lists.IndexOf(oldItem);
                    _Lists.Remove(oldItem);
                    _Lists.Insert(index, newList);
                }
                else
                {
                    _Lists.Add(newList);
                }
            });

           MessagingCenterExtensions.UnitarySubscribe<ListEditionPageViewModel, IRandomizerList, HomePageViewModel>(this,
           ListEditionPageViewModel.MessagingCenterConstants.ListDeleted, (sender, deletedList) =>
           {
               _Lists.Remove(deletedList);
           });

            #region Commands Init
            NewRandomizerListCommand = new Command(OnNewRandomizerList);
            ListTappedCommand = new Command<IRandomizerList>(OnListTapped);
            #endregion
        }

        private void InitListWithStubData()
        {
            Lists.Add(new RandomizerList() { Name = "List #1", Id = Guid.NewGuid() });
            Lists.Add(new RandomizerList() { Name = "List #2", Id = Guid.NewGuid() });
            Lists.Add(new RandomizerList() { Name = "List #3", Id = Guid.NewGuid() });
            Lists.Add(new RandomizerList() { Name = "List #4", Id = Guid.NewGuid() });
            Lists.Add(new RandomizerList() { Name = "List #5", Id = Guid.NewGuid() });
            Lists.Add(new RandomizerList() { Name = "List #6", Id = Guid.NewGuid() });
        }

        #endregion

        #region Methods
        async private void OnNewRandomizerList()
        {
            await NavigationService.GoToAsync("/listedition?new=true&editmode=true");
        }

        async private void OnListTapped(IRandomizerList list)
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
