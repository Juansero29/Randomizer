using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Randomizer.Framework.Models
{
    public class ImageRandomizerItem : IRandomizerItem
    {
        public Guid Id { get; set; }

        // The URI of the image ?
        // TODO Issue #2
        public string Source { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
