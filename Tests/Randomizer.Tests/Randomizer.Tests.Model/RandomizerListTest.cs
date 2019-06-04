using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Randomizer.Tests.Model
{
    public class RandomizerListTest
    {
        private RandomizerList _RandomizerList;

        public RandomizerListTest()
        {
            _RandomizerList = new RandomizerList();
        }

        [Fact]
        private void ConstructorTest()
        {
            var randomizerList = new RandomizerList();

            Assert.NotNull(randomizerList);
            Assert.NotNull(randomizerList.Items);
            Assert.Empty(randomizerList.Items);
        }

        public static IEnumerable<object[]> TestItems => new[]
        {
            new object[]{ new TextRandomizerItem() },
            new object[]{ new TextRandomizerItem() { Name = "Hi" } },
            new object[]{ new TextRandomizerItem() { Id = new Guid() } },
            new object[]{ new TextRandomizerItem() { Id = new Guid(), Name= "Hello" } }
        };

        [Theory]
        [MemberData(nameof(TestItems))]
        private void InsertItemTest(IRandomizerItem item)
        {
            _RandomizerList.AddItem(item);

            Assert.NotEmpty(_RandomizerList.Items);
            Assert.Contains(item, _RandomizerList.Items);
        }

        [Theory]
        [MemberData(nameof(TestItems))]
        private void RemoveItemTest(IRandomizerItem item)
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

        [Fact]
        private void IdPropertyTest()
        {
            var guid = new Guid();
            _RandomizerList.Id = guid;

            Assert.Equal(_RandomizerList.Id, guid);
        }


    }
}
