using Randomizer.Framework.Models;
using Randomizer.Framework.ViewModels.Business;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Randomizer.Tests.CommonTestData
{
    public class RandomizerItemVMTestData : IEnumerable<object[]>
    {

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new RandomizerItemVM(new TextRandomizerItem()) };
            yield return new object[] { new RandomizerItemVM(new TextRandomizerItem() { Name = "Hi" }) };
            yield return new object[] { new RandomizerItemVM(new TextRandomizerItem()) };
            yield return new object[] { new RandomizerItemVM(new TextRandomizerItem() { Name = "Hello" }) };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
