using FluentAssertions;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.ViewModels.Business;
using Randomizer.Tests.CommonTestData;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Randomizer.Tests.ViewModels
{
    public class RandomizerListVMTest
    {
        RandomizerListVM _ViewModel;

        public RandomizerListVMTest()
        {
            _ViewModel = new RandomizerListVM();
        }

        [Fact]
        private void ConstructorTest()
        {
            var vm = new RandomizerListVM();
            vm.Should().NotBeNull();
            vm.Items.Should().BeEmpty();
        }

        [Theory]
        [ClassData(typeof(RandomizerItemTestData))]
        private void AddItemTest(RandomizerItem item)
        {
            _ViewModel.Items.Should().BeEmpty();
            _ViewModel.AddItem(item);
            _ViewModel.Items.Should().NotBeEmpty();
            _ViewModel.Items.Should().OnlyContain((containedItem) => item.Equals(containedItem));
        }

        [Theory]
        [ClassData(typeof(RandomizerItemTestData))]
        private void RemoveItemTest(RandomizerItem item)
        {
            _ViewModel.AddItem(item);
            _ViewModel.RemoveItem(item);
            _ViewModel.Items.Should().BeEmpty();
        }
        
    }
}
