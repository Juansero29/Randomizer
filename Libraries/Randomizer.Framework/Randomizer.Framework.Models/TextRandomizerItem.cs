﻿using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Randomizer.Framework.Models
{
    public class TextRandomizerItem : IRandomizerItem<string>
    {
        public Guid Id { get; set; }

        // The URI of the image ?
        // TODO Issue #2
        public string Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
