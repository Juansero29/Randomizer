using FluentAssertions;
using Moq;
using Randomizer.Framework.Services;
using Randomizer.Framework.Services.Navigation;
using Randomizer.Framework.ViewModels.Pages;
using System;
using Xunit;

namespace Randomizer.Tests.ViewModels
{
    public class HomePageViewModelTest
    {

        private readonly HomePageViewModel _HomePageViewModel;


        /// <summary>
        /// Constructs the tests. 
        /// </summary>
        /// <remarks>
        /// We prepare tests in here
        /// </remarks>
        public HomePageViewModelTest()
        {
            var mock = new Mock<INavigationService>();
            mock.Setup(m => m.GoToAsync("somewhere"));
            _HomePageViewModel = new HomePageViewModel(mock.Object);
            _HomePageViewModel.Should().NotBeNull();
        }
        [Fact]
        public void ConstructorTest()
        {
            var mock = new Mock<INavigationService>();
            mock.Setup(m => m.GoToAsync("somewhere"));
            var homeViewModel = new HomePageViewModel(mock.Object);
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
