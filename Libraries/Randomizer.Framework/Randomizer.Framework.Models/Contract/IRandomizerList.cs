using EnigmatiKreations.Framework.Managers.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Randomizer.Framework.Models.Contract
{
    public interface IRandomizerList
    {
        int Id { get; set; }

        string Name { get; set; }

        IEnumerable<IRandomizerItem> Items { get; }

        void AddItem(IRandomizerItem item);

        bool RemoveItem(IRandomizerItem item);

        bool ContainsItem(IRandomizerItem item);

    }
}
