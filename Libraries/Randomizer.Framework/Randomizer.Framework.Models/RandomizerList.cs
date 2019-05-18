using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;

namespace Randomizer.Framework.Models
{
    public class RandomizerList<T> : IRandomizerList<T>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        private readonly ICollection<IRandomizerItem<T>> _Items = new List<IRandomizerItem<T>>();

        public IEnumerable<IRandomizerItem<T>> Items
        {
            get { foreach (var item in _Items) yield return item; }
        }
       
    }
}
