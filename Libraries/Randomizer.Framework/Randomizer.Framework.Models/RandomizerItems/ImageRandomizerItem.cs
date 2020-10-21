using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Randomizer.Framework.Models
{
    public class ImageRandomizerItem : IRandomizerItem
    {
        public int Id { get; set; }

        public byte[] Image { get; set; }

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
            hashCode = hashCode * -1221135295 + EqualityComparer<int>.Default.GetHashCode(Id);
            hashCode = hashCode * -1221135295 + EqualityComparer<string>.Default.GetHashCode(Source);
            return hashCode;
        }
    }
}
