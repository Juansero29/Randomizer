using FluentAssertions;
using Moq;
using Randomizer.Framework.Services;
using Randomizer.Framework.Services.Navigation;
using Randomizer.Framework.ViewModels.BaseViewModels;
using Randomizer.Framework.ViewModels.Pages;
using System;
using Xunit;

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
            ViewModelLocator.RegisterDependencies(true);
            _HomePageViewModel = new HomePageViewModel();
        }

        [Fact]
        public void ConstructorTest()
        {
            var homeViewModel = new HomePageViewModel();
            homeViewModel.Should().NotBeNull();
        }

        [Fact]
        public void NavigateToAddListPageTest()
        {
            _HomePageViewModel.NewRandomizerListCommand.Should().NotBeNull();
            _HomePageViewModel.NewRandomizerListCommand?.Execute(null);
        }

        [Fact]
        public void ListsNotNullTest()
        {
            _HomePageViewModel.Lists.Should().NotBeNull();
        }

        [Fact]
        public void TitleNotNullTest()
        {
            _HomePageViewModel.Title.Should().NotBeNull();
        }

    }
}
