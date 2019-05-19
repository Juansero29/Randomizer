using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;

namespace Randomizer.Framework.Models
{
    public class RandomizerList : IRandomizerList
    {
        public Guid Id { get; set; }

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
            return  _Items.Remove(item);
        }
        
    }
}
