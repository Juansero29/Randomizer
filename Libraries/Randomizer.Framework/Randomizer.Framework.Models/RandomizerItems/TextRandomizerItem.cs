using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Randomizer.Framework.Models
{
    /// <summary>
    /// A randomizer item that contains text
    /// </summary>
    public class TextRandomizerItem : RandomizerItem
    {
        public string Name { get; set; }


        public TextRandomizerItem()
        {
        }

        public TextRandomizerItem(string name)
        {
            Name = name;
        }

        public TextRandomizerItem(string name, RandomizerList parent)
        {
            Name = name;
            Parent = parent;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (!(obj is TextRandomizerItem other)) return false;
            var isEqual = Id == other.Id;
            return isEqual;
        }

        public override int GetHashCode()
        {
            var hashCode = -1620415233;
            hashCode = hashCode * -1221135295 + EqualityComparer<object>.Default.GetHashCode(Id);
            hashCode = hashCode * -1221135295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }
}
