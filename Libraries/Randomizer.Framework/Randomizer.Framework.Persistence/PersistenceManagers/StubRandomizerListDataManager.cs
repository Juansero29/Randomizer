﻿using EnigmatiKreations.Framework.Managers.Contract;
using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Randomizer.Framework.Persistence
{
    public class StubRandomizerListDataManager : IDataManager<IRandomizerList>
    {
        private IEnumerable<IRandomizerList> _SavedLists;
        private List<IRandomizerList> _Lists = new List<IRandomizerList>();


        public StubRandomizerListDataManager()
        {
            _SavedLists = new List<IRandomizerList>();
            _Lists.AddRange(_SavedLists);
        }

        public Task<bool> Add(IRandomizerList item)
        {
            _Lists.Add(item);
            return Task.FromResult(_Lists.Contains(item));
        }

        public Task<bool> AddRange(params IRandomizerList[] items)
        {
            var count = items.Length;
            var origin = _Lists.Count;
            _Lists.AddRange(items);
            return Task.FromResult(_Lists.Count - count == origin)
        }

        public Task<IRandomizerList> Get(int id)
        {
            return Task.FromResult(_Lists.Find(t => t.Id == id));
        }

        public Task<IRandomizerList> Get(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IRandomizerList>> GetItems()
        {
            return Task.FromResult(_Lists as IEnumerable<IRandomizerList>);
        }

        public Task<IEnumerable<IRandomizerList>> GetItems(int index, int count)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(Guid id)
        {
            var r = _Lists.Find(t => t.Id == id);
            return Task.FromResult(_Lists.Remove(r));
        }

        public Task<bool> Remove(object id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Save()
        {
            var c = _Lists.Count;
            _SavedLists = _Lists;
            return Task.FromResult(_Lists.Count);
        }

        public Task<Tuple<bool, IRandomizerList>> Update(IRandomizerList item)
        {
            var index = _Lists.IndexOf(item);
            var oldItem = _Lists[index];
            _Lists[index] = item;
            return Task.FromResult(new Tuple<bool, IRandomizerList>(true, oldItem));
        }

        public Task<Tuple<bool, IRandomizerList>> Update(object id, IRandomizerList item)
        {
            throw new NotImplementedException();
        }
    }
}