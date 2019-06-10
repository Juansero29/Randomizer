using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Services.Navigation;
using System;
using System.Collections.ObjectModel;
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
        #endregion

        #region Constructor(s)

        public HomePageViewModel() : this(null)
        {

        }

        public HomePageViewModel(INavigationService navService = null) : base(navService)
        {
            InitListWithStubData();

            #region Commands Init
            NewRandomizerListCommand = new Command(OnNewRandomizerList);
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
        #endregion
    }
}
