using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using EnigmatiKreations.Framework.Services.Alerts;
using EnigmatiKreations.Framework.Services.Navigation;
using FluentAssertions;
using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Persistence;
using Randomizer.Framework.Services.Alerts;
using Randomizer.Framework.Services.Navigation;
using Randomizer.Framework.ViewModels.Business;
using Randomizer.Framework.ViewModels.Pages;
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

        public ListEditionPageViewModelTest()
        {
            Xamarin.Forms.Mocks.MockForms.Init();
            RegisterServicesInContainer();
        }

        private void RegisterServicesInContainer()
        {
            do
            {
                Container.PrepareNewBuilder();
                Container.RegisterDependency(new NavigationMockService(), typeof(INavigationService), true);
                Container.RegisterDependency(new AlertsMockService(), typeof(IAlertsService), true);
                Container.RegisterDependency(new ListsManager(new TestsRandomizerDataManager()), typeof(ListsManager), true);
            } while (!Container.BuildContainer());


        }



        [Fact]
        private void ConstructorTest()
        {
            
            var vm = new ListEditionPageViewModel();
            vm.Should().NotBeNull();
        }

        [Fact]
        private void InitializationTest()
        {   
            //_ViewModel.AddItemCommand.Should().NotBeNull();
            //_ViewModel.RemoveListItemCommand.Should().NotBeNull();
            //_ViewModel.SaveListCommand.Should().NotBeNull();
            //_ViewModel.DeleteListCommand.Should().NotBeNull();
            //_ViewModel.RandomizeCommand.Should().NotBeNull();
            //_ViewModel.IsNew.Should().BeFalse();
            //_ViewModel.ItemEntryText.Should().BeNullOrEmpty();
        }


        [Fact]
        private void NewListParam_SetsIsNewProperty()
        {
            //_ViewModel.IsNewParam = "true";
            //_ViewModel.IsNew.Should().BeTrue();
        }

        #region Command Tests

        [Fact]
        private void AddItemCommand()
        {
            //PrepareContext();
            //string itemName = "Blup";
            //_ViewModel.AddItemCommand.Execute(itemName);
            //_ViewModel.ListVM.Items.Should().NotBeEmpty();
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
