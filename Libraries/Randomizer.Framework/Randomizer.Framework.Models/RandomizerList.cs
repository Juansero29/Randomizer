using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;

namespace Randomizer.Framework.Models
{
    public class RandomizerList : IRandomizerList, IEquatable<RandomizerList>
    {
        public Guid Id { get; set; } = Guid.NewGuid();

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
    }
}
