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
using Randomizer.Framework.ViewModels.Business.Items;
using Randomizer.Tests.CommonTestData;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Randomizer.Tests.ViewModels.Business
{
    public class RandomizerListVMTest
    {
        #region Lifecycle
        public RandomizerListVMTest()
        {
            // Register (create) view models in the view model locator
            RegisterServicesInContainer();


        }

        private void RegisterServicesInContainer()
        {
            do
            {
                Container.PrepareNewBuilder();
                Container.RegisterDependency(new NavigationMockService(), typeof(INavigationService), true);
                Container.RegisterDependency(new AlertsMockService(), typeof(IAlertsService), true);
            } while (!Container.BuildContainer());
        }


        #endregion


        [Fact]
        private void ConstructorTest()
        {
            var model = new SimpleRandomizerList() { Name = "Beers" };
            var vm = new RandomizerListVM(model);
            vm.Should().NotBeNull();
        }

        [Theory]
        [ClassData(typeof(RandomizerItemVMTestData))]
        private async void AddItemTest(RandomizerItemVM item)
        {
            var model = new SimpleRandomizerList() { Name = "Beers" };
            var vm = new RandomizerListVM(model);
            vm.ItemsVM.Should().BeEmpty();
            await vm.AddItemCommand.ExecuteAsync(item);
            vm.ItemsVM.Should().NotBeEmpty();
            vm.ItemsVM.Should().OnlyContain((containedItem) => item.Equals(containedItem));
        }

        [Theory]
        [ClassData(typeof(RandomizerItemVMTestData))]
        private async void AddItemTwiceTest(RandomizerItemVM item)
        {
            var model = new SimpleRandomizerList() { Name = "Beers" };
            var vm = new RandomizerListVM(model);
            vm.ItemsVM.Should().BeEmpty();
            await vm.AddItemCommand.ExecuteAsync(item);
            await vm.AddItemCommand.ExecuteAsync(item);
            vm.ItemsVM.Should().OnlyContain((containedItem) => item.Equals(containedItem));
        }

        [Theory]
        [ClassData(typeof(RandomizerItemVMTestData))]
        private async void RemoveItemTest(RandomizerItemVM item)
        {
            var model = new SimpleRandomizerList() { Name = "Beers" };
            var vm = new RandomizerListVM(model);
            vm.ItemsVM.Should().BeEmpty();

            await vm.AddItemCommand.ExecuteAsync(item);
            vm.ItemsVM.Should().NotBeEmpty();

            await vm.RemoveItemCommand.ExecuteAsync(item);
            vm.ItemsVM.Should().BeEmpty();
        }

        [Theory]
        [ClassData(typeof(RandomizerItemVMTestData))]
        private async void UpdateItemTest(TextRandomizerItemVM item)
        {
            var model = new SimpleRandomizerList() { Name = "Beers" };
            var vm = new RandomizerListVM(model);
            vm.ItemsVM.Should().BeEmpty();

            await vm.AddItemCommand.ExecuteAsync(item);
            vm.ItemsVM.Should().NotBeEmpty();

            item.Name = "Petrus Red";
            await vm.UpdateItemCommand.ExecuteAsync(item);

            vm.ItemsVM.Should().OnlyContain(i => i.Equals(item));
        }


        [Theory]
        [ClassData(typeof(RandomizerItemVMTestData))]
        private async void ClearItemsTests(TextRandomizerItemVM item)
        {
            var model = new SimpleRandomizerList() { Name = "Beers" };
            var vm = new RandomizerListVM(model);
            vm.ItemsVM.Should().BeEmpty();

            await vm.AddItemCommand.ExecuteAsync(item);
            vm.ItemsVM.Should().NotBeEmpty();

            await vm.ClearListCommand.ExecuteAsync();

            vm.ItemsVM.Should().BeEmpty();
        }



    }
}
