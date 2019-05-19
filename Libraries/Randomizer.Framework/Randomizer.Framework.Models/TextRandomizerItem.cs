using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Randomizer.Framework.Models
{
    public class TextRandomizerItem : IRandomizerItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
