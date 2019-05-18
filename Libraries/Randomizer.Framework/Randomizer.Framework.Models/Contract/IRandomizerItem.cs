using System;
using System.Collections.Generic;
using System.Text;

namespace Randomizer.Framework.Models.Contract
{
    public interface IRandomizerItem<T> : IIdentifiable
    {
        T Value { get; set; }
    }
}
