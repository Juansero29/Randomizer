using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using EnigmatiKreations.Framework.Services.Alerts;
using EnigmatiKreations.Framework.Services.Navigation;
using FluentAssertions;
using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Pages.Navigation;
using Randomizer.Framework.Persistence;
using Randomizer.Framework.Services.Alerts;
using Randomizer.Framework.Services.Navigation;
using Randomizer.Framework.ViewModels.Business;
using Randomizer.Framework.ViewModels.Pages;
using Randomizer.Pages;
using Randomizer.Tests.CommonTestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xunit;

namespace Randomizer.Tests.ViewModels.Pages
{
    public class ListEditionPageViewModelTest
    {

        #region Lifecycle
        public ListEditionPageViewModelTest()
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
        private void ConstructorTest()
        {
            var vm = new ListEditionPageViewModel();
            vm.Should().NotBeNull();
        }

        [Fact]
        private void InitializationTest()
        {
            var vm = new ListEditionPageViewModel();
            vm.AddItemCommand.Should().NotBeNull();
            vm.RemoveListItemCommand.Should().NotBeNull();
            vm.SaveListCommand.Should().NotBeNull();
            vm.DeleteListCommand.Should().NotBeNull();
            vm.RandomizeCommand.Should().NotBeNull();
            vm.IsNew.Should().BeFalse();
            vm.ItemEntryText.Should().BeNullOrEmpty();
        }


        [Fact]
        private void NewListParamHasCorrectValues()
        {
            var vm = new ListEditionPageViewModel();
            vm.IsNewParam = "true";
            vm.IsNew.Should().BeTrue();

            vm.IsNewParam = "false";
            vm.IsNew.Should().BeFalse();
        }

        #region Command Tests

        [Fact]
        private async Task ModifyNameOfNewListAndSave()
        {
            var vm = new HomePageViewModel();
            await vm.NewListButtonPressed();
            var listEdPage = Container.Resolve<INavigationService>().GetCurrentPage();
            var listEdVm = listEdPage.BindingContext as ListEditionPageViewModel;
            listEdVm.IsNewParam = "true";
            listEdVm.IsNew.Should().BeTrue();
            listEdVm.ListVM.Name = "Beers";
            await listEdVm.SaveList();
            Container.Resolve<ListsManagerVM>().ListsVM.Should().Contain(listEdVm.ListVM);
        }


        [Fact]
        private async Task SaveNewListWithNoData()
        {
            var vm = new HomePageViewModel();
            await vm.NewListButtonPressed();
            var listEdPage = Container.Resolve<INavigationService>().GetCurrentPage();
            var listEdVm = listEdPage.BindingContext as ListEditionPageViewModel;
            listEdVm.IsNewParam = "true";
            listEdVm.IsNew.Should().BeTrue();
            listEdVm.ListVM.Name = string.Empty;
            await listEdVm.SaveListCommand.ExecuteAsync();
            Container.Resolve<ListsManagerVM>().ListsVM.Should().BeEmpty();
        }

        [Fact]
        private async Task AddItemCommand()
        {
            var vm = new ListEditionPageViewModel();
            vm.IsNewParam = "true";
            vm.IsNew.Should().BeTrue();
            string itemName = "Plumbus";
            await vm.AddItemCommand.ExecuteAsync(itemName);
            vm.ListVM.ItemsVM.Should().NotBeEmpty();
        }

        [Fact]
        private void RemoveListItemCommand()
        {
            //PrepareContext();

            //_ViewModel.ListVM.RemoveAllItems();
            //_ViewModel.ListVM.Items.Should().BeEmpty();
            //string itemName = "Blup";
            //_ViewModel.AddItemCommand.Execute(itemName);
            //_ViewModel.ListVM.Items.Should().NotBeEmpty();
            //_ViewModel.RemoveListItemCommand.Execute(_ViewModel.ListVM.Items.First());
            //_ViewModel.ListVM.Items.Should().BeEmpty();
        }


        [Fact]
        private async void SaveListFromHomePage()
        {
            await Task.FromResult(true);
            //var man = Container.Resolve<ListsManagerVM>();
            //var listModel = new SimpleRandomizerList() { Name = "Beers" };
            //var listVM = new RandomizerListVM(listModel);
            //await man.AddList(listVM);
        }

        [Fact]
        private void SaveListCommand()
        {
            //PrepareContext();
            //string oldName = _ViewModel.ListVM.Name;
            //string listTitle = "My list" + DateTime.Now.ToString();
            //_ViewModel.ListVM.Name = listTitle;
            //_ViewModel.SaveListCommand.Execute(null);
            //_HomePageViewModel.Lists.Should().Contain(l => l.Name == listTitle);
            //_HomePageViewModel.Lists.Should().NotContain(l => l.Name == oldName);
        }

        [Fact]
        private void DeleteListCommand()
        {
            //PrepareContext();
            //var deletedList = _ViewModel.ListVM;
            //_ViewModel.DeleteListCommand.Execute(null);
            //_HomePageViewModel.Lists.Should().NotContain(deletedList);
        }


        #endregion Command Tests
    }
}
