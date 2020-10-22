using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using Randomizer.Tests.CommonTestData;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Randomizer.Tests.Model
{
    public class RandomizerListTest
    {
        private Framework.Models.SimpleRandomizerList _RandomizerList;

        public RandomizerListTest()
        {
            _RandomizerList = new Framework.Models.SimpleRandomizerList();
        }

        [Fact]
        private void ConstructorTest()
        {
            var randomizerList = new Framework.Models.SimpleRandomizerList();

            Assert.NotNull(randomizerList);
            Assert.NotNull(randomizerList.Items);
            Assert.Empty(randomizerList.Items);
        }

        [Theory]
        [ClassData(typeof(RandomizerItemTestData))]
        private void InsertItemTest(RandomizerItem item)
        {
            _RandomizerList.AddItem(item);

            Assert.NotEmpty(_RandomizerList.Items);
            Assert.Contains(item, _RandomizerList.Items);
        }

        [Theory]
        [ClassData(typeof(RandomizerItemTestData))]
        private void RemoveItemTest(RandomizerItem item)
        {
            _RandomizerList.AddItem(item);
            _RandomizerList.RemoveItem(item);

            Assert.DoesNotContain(item, _RandomizerList.Items);
            Assert.Empty(_RandomizerList.Items);
        }

        [Theory]
        [InlineData("")]
        [InlineData("Eat")]
        [InlineData("This is the name of a very long item")]
        private void NamePropertyTest(string listName)
        {
            _RandomizerList.Name = listName;
            Assert.NotNull(_RandomizerList.Name);
            Assert.Equal(_RandomizerList.Name, listName);
        }


    }
}
