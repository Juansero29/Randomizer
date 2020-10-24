using FluentAssertions;
using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.ViewModels.Business;
using Randomizer.Tests.CommonTestData;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Randomizer.Tests.ViewModels.Business
{
    public class RandomizerListVMTest
    {

        [Fact]
        private void ConstructorTest()
        {
            var model = new SimpleRandomizerList() { Name = "Beers" };
            var vm = new RandomizerListVM(model);
            vm.Should().NotBeNull();
        }

        [Theory]
        [ClassData(typeof(RandomizerItemVMTestData))]
        private void AddItemTest(RandomizerItemVM item)
        {
            var model = new SimpleRandomizerList() { Name = "Beers" };
            var vm = new RandomizerListVM(model);
            vm.ItemsVM.Should().BeEmpty();
            vm.AddItem(item.Model);
            vm.ItemsVM.Should().NotBeEmpty();
            vm.ItemsVM.Should().OnlyContain((containedItem) => item.Equals(containedItem));
        }

        [Theory]
        [ClassData(typeof(RandomizerItemVMTestData))]
        private void RemoveItemTest(RandomizerItemVM item)
        {

            //_ViewModel.AddItem(item);
            //_ViewModel.RemoveItem(item);
            //_ViewModel.Items.Should().BeEmpty();
        }
        
    }
}
