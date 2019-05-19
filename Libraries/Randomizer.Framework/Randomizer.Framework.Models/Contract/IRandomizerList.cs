using System;
using System.Collections.Generic;
using System.Text;

namespace Randomizer.Framework.Models.Contract
{
    public interface IRandomizerList<T> : IIdentifiable
    {
        string Name { get; set; }

        IEnumerable<IRandomizerItem<T>> Items { get; }

        void AddItem(IRandomizerItem<T> item);

        bool RemoveItem(IRandomizerItem<T> item);

    }
}
