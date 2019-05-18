using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Randomizer.Framework.Models
{
    public class ImageRandomizerItem : IRandomizerItem<string>
    {
        public Guid Id { get; set; }

        public string Value { get; set; }
    }
}
