using EnigmatiKreations.Framework.Managers.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Randomizer.Framework.Models.Contract
{
    /// <summary>
    /// An abstract RandomizerItem
    /// </summary>
    public abstract class RandomizerItem
    {
        public int Id { get; set; }

        public virtual RandomizerList Parent { get; set; }
    }
}
