using FluentAssertions;
using Moq;
using EnigmatiKreations.Framework.Services;
using EnigmatiKreations.Framework.Services.Navigation;
using Randomizer.Framework.ViewModels.Business;
using Randomizer.Framework.ViewModels.Pages;
using System;
using Xunit;
using EnigmatiKreations.Framework.MVVM.BaseViewModels;

namespace Randomizer.Tests.ViewModels
{
    public class HomePageViewModelTest
    {

        private HomePageViewModel _HomePageViewModel;

        /// <summary>
        /// Constructs the tests. 
        /// </summary>
        /// <remarks>
        /// We prepare tests in here
        /// </remarks>
        public HomePageViewModelTest()
        {
            ViewModelLocator.RegisterDependencies(mockDependencies: true);
            _HomePageViewModel = new HomePageViewModel();
        }

        [Fact]
        public void TitleNotNullTest()
        {
            _HomePageViewModel.Title.Should().NotBeNull();
        }

        [Fact]
        public void ConstructorTest()
        {
            _HomePageViewModel.Should().NotBeNull();
        }

        [Fact]
        public void NavigateToAddListPageTest()
        {
            _HomePageViewModel.NewRandomizerListCommand.Should().NotBeNull();
            _HomePageViewModel.NewRandomizerListCommand?.Execute(null);
        }

        [Fact]
        private void HomePageViewModelListsShouldStartEmpty()
        {
            _HomePageViewModel.Lists.Should().NotBeNull();
            _HomePageViewModel.Lists.Should().BeEmpty();
        }

        [Fact]
        private void AddingListsToHomePage()
        {
            
            _HomePageViewModel.Lists.Add(new RandomizerListVM() { Name = "List #1" });
            _HomePageViewModel.Lists.Add(new RandomizerListVM() { Name = "List #2" });
            _HomePageViewModel.Lists.Add(new RandomizerListVM() { Name = "List #3" });
            _HomePageViewModel.Lists.Add(new RandomizerListVM() { Name = "List #4" });
            _HomePageViewModel.Lists.Add(new RandomizerListVM() { Name = "List #5" });
            _HomePageViewModel.Lists.Add(new RandomizerListVM() { Name = "List #6" });
        }


        [Fact]
        private void HomePageViewModelTestListsAreNotEmptyAfteradd()
        {
            _HomePageViewModel.Lists.Add(new RandomizerListVM() { Name = "List #1" });
            _HomePageViewModel.Lists.Should().NotBeEmpty();
            _HomePageViewModel.Lists[0].Should().NotBeNull();
        }





        [Fact]
        public void SelectListTest()
        {
            _HomePageViewModel.Lists.Add(new RandomizerListVM() { Name = "List #1" });
            _HomePageViewModel.Lists.Should().NotBeEmpty();
            _HomePageViewModel.ListTappedCommand.Execute(_HomePageViewModel.Lists[0]);

        }

    }
}
