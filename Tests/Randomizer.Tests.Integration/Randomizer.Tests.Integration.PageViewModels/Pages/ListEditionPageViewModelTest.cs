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
        private async Task AddItemInNewList()
        {
            var vm = new ListEditionPageViewModel
            {
                IsNewParam = "true"
            };
            vm.IsNew.Should().BeTrue();
            await vm.LoadCommandAsync.ExecuteAsync(null);
            vm.ListVM.ItemsVM.Should().BeEmpty();
            vm.IsNewParam = "true";
            vm.IsNew.Should().BeTrue();
            string itemName = "Plumbus";
            await vm.ListVM.AddItemCommand.ExecuteAsync(itemName);
            vm.ListVM.ItemsVM.Should().NotBeEmpty();
            await vm.SaveList();
            var man = Container.Resolve<ListsManagerVM>();
            man.ListsVM.Should().Contain(vm.ListVM);
        }

        [Fact]
        private async Task RemoveItemInNewList()
        {
            var vm = new ListEditionPageViewModel
            {
                IsNewParam = "true"
            };
            vm.IsNew.Should().BeTrue();
            await vm.LoadCommandAsync.ExecuteAsync(null);
            vm.ListVM.ItemsVM.Should().BeEmpty();
            vm.IsNewParam = "true";
            vm.IsNew.Should().BeTrue();
            vm.ListVM.Name = "Essential Items";
            string itemName = "Plumbus";
            await vm.ListVM.AddItemCommand.ExecuteAsync(itemName);
            vm.ListVM.ItemsVM.Should().NotBeEmpty();
            await vm.SaveList();
            var man = Container.Resolve<ListsManagerVM>();
            man.ListsVM.Should().Contain(vm.ListVM);

            man.CurrentList.ItemsVM.Count.Should().Be(1);
            var id = man.CurrentList.ItemsVM.FirstOrDefault()?.Id;

            await vm.ListVM.RemoveItemCommand.ExecuteAsync(id);
            await vm.SaveList();

            var list = man.ListsVM.Where(l => l.Equals(vm.ListVM)).FirstOrDefault();
            list.ItemsVM.Should().BeEmpty();

        }


        [Fact]
        private async Task DeleteListCommand()
        {
            var vm = new ListEditionPageViewModel
            {
                IsNewParam = "true"
            };
            vm.IsNew.Should().BeTrue();
            await vm.LoadCommandAsync.ExecuteAsync(null);
            vm.ListVM.ItemsVM.Should().BeEmpty();
            vm.IsNewParam = "true";
            vm.IsNew.Should().BeTrue();
            vm.ListVM.Name = "Essential Items";
            await vm.SaveListCommand.ExecuteAsync();

            var man = Container.Resolve<ListsManagerVM>();
            man.ListsVM.Should().Contain(vm.ListVM);

            await vm.DeleteListCommand.ExecuteAsync();
            man.ListsVM.Should().NotContain(vm.ListVM);
        }


        #endregion
    }
}
