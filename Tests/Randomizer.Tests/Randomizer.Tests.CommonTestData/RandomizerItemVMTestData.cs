using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.ViewModels.Business;
using Randomizer.Framework.ViewModels.Business.Items;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Randomizer.Tests.CommonTestData
{
    public class RandomizerItemVMTestData : IEnumerable<object[]>
    {

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new TextRandomizerItemVM(new TextRandomizerItem()) };
            yield return new object[] { new TextRandomizerItemVM(new TextRandomizerItem() { Name = "Leffe" }) };
            yield return new object[] { new TextRandomizerItemVM(new TextRandomizerItem() { Name = "Petrus", Parent = default}) };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
