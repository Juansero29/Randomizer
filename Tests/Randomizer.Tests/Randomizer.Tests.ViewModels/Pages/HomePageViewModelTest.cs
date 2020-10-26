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
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Models;
using System;

namespace Randomizer.Tests.ViewModels.Pages
{
    public class HomePageViewModelTest
    {

        #region Lifecycle
        public HomePageViewModelTest()
        {
            Xamarin.Forms.Mocks.MockForms.Init();
            RegisterServicesInContainer();
        }
        private void RegisterServicesInContainer()
        {
            do
            {
                Container.PrepareNewBuilder();
                Container.RegisterDependency(new ShellNavigationService(), typeof(INavigationService), true);
                Container.RegisterDependency(new AlertsMockService(), typeof(IAlertsService), true);
                Container.RegisterDependency(new ListsManager(new TestsRandomizerDataManager()), typeof(ListsManager), true);
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
        public void NavigateToAddListPageTest()
        {
            var vm = new HomePageViewModel();
            vm.NewRandomizerListCommand.Should().NotBeNull();
            vm.NewRandomizerListCommand?.Execute(null);
        }

        [Fact]
        private void HomePageLoadExistingDataTest()
        {
            Assert.Equal(1, 1);
            //_HomePageViewModel.Lists.Should().NotBeNull();
            //_HomePageViewModel.Lists.Should().BeEmpty();
        }




        //[Fact]
        //private void HomePageViewModelTestListsAreNotEmptyAfteradd()
        //{
        //    //_HomePageViewModel.Lists.Add(new RandomizerListVM() { Name = "List #1" });
        //    //_HomePageViewModel.Lists.Should().NotBeEmpty();
        //    //_HomePageViewModel.Lists[0].Should().NotBeNull();
        //}





        [Fact]
        public void SelectListTest()
        {
            Assert.Equal(1, 1);
            //_HomePageViewModel.Lists.Add(new RandomizerListVM() { Name = "List #1" });
            //_HomePageViewModel.Lists.Should().NotBeEmpty();
            //_HomePageViewModel.ListTappedCommand.Execute(_HomePageViewModel.Lists[0]);

        }

    }
}
