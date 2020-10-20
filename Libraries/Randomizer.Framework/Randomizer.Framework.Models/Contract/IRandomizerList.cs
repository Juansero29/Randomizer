using EnigmatiKreations.Framework.Managers.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Randomizer.Framework.Models.Contract
{
    public interface IRandomizerList : IIdentifiable
    {
        string Name { get; set; }

        IEnumerable<IRandomizerItem> Items { get; }

        void AddItem(IRandomizerItem item);

        bool RemoveItem(IRandomizerItem item);

        bool ContainsItem(IRandomizerItem item);

    }
}
