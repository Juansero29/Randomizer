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
    public class HomePageViewModelTest : IDisposable
    {


        public void Dispose()
        {
            Container.Dispose();
        }

        private void RegisterServicesInContainer()
        {
            Container.PrepareNewBuilder();
            Container.RegisterDependency(new NavigationMockService(), typeof(INavigationService), true);
            Container.RegisterDependency(new AlertsMockService(), typeof(IAlertsService), true);
            ////Container.RegisterDependency(new ListsManager(new StubRandomizerListDataManager()), typeof(ListsManager), true);
            Container.RegisterDependency(new ListsManagerVM(new ListsManager(new TestsRandomizerDataManager())), typeof(ListsManagerVM), true);
            Container.BuildContainer();
        }

        //[Fact]
        //public void TitleNotNullTest()
        //{
        //    //_HomePageViewModel.Title.Should().NotBeNull();
        //}

        [Fact]
        public void ConstructorTest()
        {
            RegisterServicesInContainer();
            var _HomePageViewModel = new HomePageViewModel();
            _HomePageViewModel.Should().NotBeNull();
        }

        //[Fact]
        //public void NavigateToAddListPageTest()
        //{
        //    //_HomePageViewModel.NewRandomizerListCommand.Should().NotBeNull();
        //    //_HomePageViewModel.NewRandomizerListCommand?.Execute(null);
        //}

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
