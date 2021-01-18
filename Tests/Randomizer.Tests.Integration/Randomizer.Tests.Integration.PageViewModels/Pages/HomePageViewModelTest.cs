using FluentAssertions;
using Randomizer.Framework.ViewModels.Business;
using Randomizer.Framework.ViewModels.Pages;
using Xunit;
using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using Randomizer.Framework.Services.Navigation;
using Randomizer.Framework.Services.Alerts;
using EnigmatiKreations.Framework.Services.Navigation;
using EnigmatiKreations.Framework.Services.Alerts;
using Randomizer.Framework.Persistence;
using Randomizer.Framework.Pages.Navigation;
using Xamarin.Forms;
using Randomizer.Pages;
using System.Threading.Tasks;
using Randomizer.Framework.Services.Resources;
using System.Linq;

namespace Randomizer.Tests.ViewModels.Pages
{

    /// <summary>
    /// Makes an integration test of <see cref="HomePageViewModel"/> to see if everything works as exepected
    /// </summary>
    public class HomePageViewModelTest
    {

        #region Lifecycle
        /// <summary>
        /// Executed before each test
        /// </summary>
        public HomePageViewModelTest()
        {
            Xamarin.Forms.Mocks.MockForms.Init();
            Application.Current = new App();
        }

        #endregion

        #region Methods
        private void RegisterServicesInContainer()
        {
            do
            {
                Container.PrepareNewBuilder();
                var navService = new ShellNavigationService();
                navService.Initialize(new NavigationPage(new Page()), new RandomizerPageLoader());
                Container.RegisterDependency(navService, typeof(INavigationService), true);
                Container.RegisterDependency(new AlertsService(), typeof(IAlertsService), true);
                Container.RegisterDependency(new ListsManagerVM(new ListsManager(new TestsRandomizerDataManager())), typeof(ListsManagerVM), true);
            } while (!Container.BuildContainer());
        } 
        #endregion

        [Fact]
        public void ConstructorTest()
        {
            var vm = new HomePageViewModel();
            vm.Should().NotBeNull();
        }

        [Fact]
        public void TitleNotNullTest()
        {
            var vm = new HomePageViewModel();
            vm.Title.Should().NotBeNull();
        }

        [Fact]
        public async Task NavigateToAddListPageTest()
        {
            var vm = new HomePageViewModel();
            vm.NewRandomizerListCommand.Should().NotBeNull();
            await vm.NewListButtonPressed();
            Container.Resolve<INavigationService>().GetCurrentPage().GetType().Should().Be(typeof(ListEditionPage));
        }

        [Fact]
        private async Task HomePageLoadExistingDataTest()
        {
            // re register services because need to test with TestsRandomizerDataManager
            RegisterServicesInContainer();
            var vm = new HomePageViewModel();
            await vm.RefreshListsCommand.ExecuteAsync();
            vm.Manager.ListsVM.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        private async Task ListEditIsNewWhenAddingList()
        {
            var vm = new HomePageViewModel();
            await vm.NewListButtonPressed();
            var listEdPage = Container.Resolve<INavigationService>().GetCurrentPage();
            var listEdVm = listEdPage.BindingContext as ListEditionPageViewModel;
            Container.Resolve<INavigationService>().GetCurrentPage().GetType().Should().Be(typeof(ListEditionPage));
            listEdVm.IsNew.Should().BeTrue();
            listEdVm.Title.Should().Be(TextResources.NewListPageTitle);
        }

        [Fact]
        private async Task HomePageViewModelTestListsAreNotEmptyAfteradd()
        {
            var vm = new HomePageViewModel();
            await vm.NewListButtonPressed();
            Container.Resolve<INavigationService>().GetCurrentPage().GetType().Should().Be(typeof(ListEditionPage));
            var listEdPage = Container.Resolve<INavigationService>().GetCurrentPage();
            var listEdVm = listEdPage.BindingContext as ListEditionPageViewModel;

         
        }

        [Fact]
        public async void SelectListTest()
        {
            var vm = new HomePageViewModel();
            await vm.RefreshListsCommand.ExecuteAsync();
            await vm.ListTappedCommand.ExecuteAsync(vm.Manager.ListsVM.First());
            var page = Container.Resolve<INavigationService>().GetCurrentPage();
            page.GetType().Should().Be(typeof(ListEditionPage));
        }

    }
}
