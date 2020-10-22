using Randomizer.Framework.Models;
using System;
using Xunit;

namespace Randomizer.Tests.Model
{
    public class TextRandomizerItemTest
    {
        
        [Fact]
        public void ConstructorTest()
        {
            var item = new TextRandomizerItem("");
            Assert.NotNull(item);            
        }

        [Theory]
        [InlineData("")]
        [InlineData("Eat")]
        [InlineData("This is the name of a very long item")]
        public void NamePropertyTest(string itemName)
        {
            var item = new TextRandomizerItem("");

            item.Name = itemName;

            Assert.NotNull(item.Name);
            Assert.Equal(item.Name, itemName);
        }


    }
}
