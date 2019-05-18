using System;
using System.Collections.Generic;
using System.Text;

namespace Randomizer.Framework.Models.Contract
{
    public interface IRandomizerList<T> : IIdentifiable
    {
        string Title { get; set; }

        IEnumerable<IRandomizerItem<T>> Items { get; }
    }
}
