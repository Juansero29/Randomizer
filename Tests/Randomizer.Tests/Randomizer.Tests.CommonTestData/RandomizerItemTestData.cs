using Randomizer.Framework.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Randomizer.Tests.CommonTestData
{
    public class RandomizerItemTestData : IEnumerable<object[]>
    {

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new TextRandomizerItem() };
            yield return new object[] { new TextRandomizerItem() { Name = "Hi" } };
            yield return new object[] { new TextRandomizerItem() };
            yield return new object[] { new TextRandomizerItem() { Name = "Hello" } };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
