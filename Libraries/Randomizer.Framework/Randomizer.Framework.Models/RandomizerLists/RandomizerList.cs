using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Randomizer.Framework.Models
{
    public class RandomizerList : IRandomizerList, IEquatable<RandomizerList>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        private readonly ICollection<IRandomizerItem> _Items = new List<IRandomizerItem>();

        public IEnumerable<IRandomizerItem> Items
        {
            get { foreach (var item in _Items) yield return item; }
        }

        public void AddItem(IRandomizerItem item)
        {
            _Items.Add(item);
        }

        public bool RemoveItem(IRandomizerItem item)
        {
            return _Items.Remove(item);
        }

        public bool ContainsItem(IRandomizerItem item)
        {
            return _Items.Contains(item);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (!(obj is RandomizerList other)) return false;
            var isEqual = Id == other.Id;
            return isEqual;
        }

        public bool Equals(RandomizerList other)
        {
            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = -1820475233;
            hashCode = hashCode * -1521134295 + EqualityComparer<int>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<IEnumerable<IRandomizerItem>>.Default.GetHashCode(Items);
            return hashCode;
        }


    }
}
