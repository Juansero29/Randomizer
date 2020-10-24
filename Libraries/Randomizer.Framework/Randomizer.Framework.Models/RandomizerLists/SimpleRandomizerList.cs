﻿using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Linq;

namespace Randomizer.Framework.Models
{

    /// <summary>
    /// A simple randomizer list that only contains its items and offers methods for managing them
    /// </summary>
    public class SimpleRandomizerList : RandomizerList, IEquatable<SimpleRandomizerList>
    {
        private List<RandomizerItem> _Items = new List<RandomizerItem>();

        public SimpleRandomizerList()
        {
            Items = _Items;
        }

        public override void AddItem(RandomizerItem item)
        {
            _Items.Add(item);
            item.Parent = this;
        }

        public override bool RemoveItem(RandomizerItem item)
        {
            var r = _Items.Remove(item);
            if (r) item.Parent = null;
            return r;
        }

        public override bool ContainsItem(RandomizerItem item)
        {
            return _Items.Contains(item);
        }


        public override RandomizerItem UpdateItem(object id, RandomizerItem item)
        {
            if (!ContainsItem(item)) return null;
            _Items[_Items.IndexOf(item)] = item;
            return item;
        }

        public override RandomizerItem GetItem(object id)
        {
            return _Items.FirstOrDefault(i => i.Id == (int)id);
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (!(obj is SimpleRandomizerList other)) return false;
            var isEqual = Id == other.Id;
            return isEqual;
        }

        public bool Equals(SimpleRandomizerList other)
        {
            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = -1820475233;
            hashCode = hashCode * -1521134295 + EqualityComparer<int>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<IEnumerable<RandomizerItem>>.Default.GetHashCode(Items);
            return hashCode;
        }


    }
}
