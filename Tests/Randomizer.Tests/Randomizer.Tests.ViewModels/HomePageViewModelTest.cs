using FluentAssertions;
using Moq;
using Randomizer.Framework.Services;
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
            _HomePageViewModel.NewRandomizerListCommand?.Execute(null);
        }

        [Fact]

        public void ListsNotNullTest()
        {
            _HomePageViewModel.Lists.Should().NotBeNull();
        }

    }
}
