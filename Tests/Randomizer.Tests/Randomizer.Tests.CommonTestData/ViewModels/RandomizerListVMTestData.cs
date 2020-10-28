using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.ViewModels.Business;
using Randomizer.Framework.ViewModels.Business.Items;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Randomizer.Tests.Common.ViewModels
{
    public class RandomizerListVMTestData : IEnumerable<object[]>
    {

        public IEnumerator<object[]> GetEnumerator()
        {

            var model = new SimpleRandomizerList() { Name = "Stuff" };
            model.AddItem(new TextRandomizerItem() { Name = "My Stuff 1" });

            yield return new object[] {
                new RandomizerListVM(model)
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
