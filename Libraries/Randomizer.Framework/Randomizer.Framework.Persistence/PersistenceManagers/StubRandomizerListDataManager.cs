using EnigmatiKreations.Framework.Managers.Contract;
using Randomizer.Framework.Models;
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

        int idCounter;

        private List<IRandomizerList> Items = new List<IRandomizerList>()
        {
            new RandomizerList(){Id = 1, Name = "List #1 "},
            new RandomizerList(){Id = 2, Name = "List #2 "},
            new RandomizerList(){Id = 3, Name = "List #3 "},
            new RandomizerList(){Id = 4, Name = "List #4 "},
            new RandomizerList(){Id = 5, Name = "List #5 "},
            new RandomizerList(){Id = 6, Name = "List #6 "},
            new RandomizerList(){Id = 7, Name = "List #7 "},
            new RandomizerList(){Id = 8, Name = "List #8 "},
        };

        public StubRandomizerListDataManager()
        {
            idCounter = Items.Count + 1;
        }

        public Task<int> Count() => Task.FromResult(Items.Count);

        public Task<bool> AddRange(params IRandomizerList[] items)
        {
            if (Items.Intersect(items).Count() > 0)
            {
                return Task.FromResult(false);
            }
            foreach (var item in items)
            {
                Add(item);
            }
            return Task.FromResult(true);
        }

        public Task Clear()
        {
            Items.Clear();
            return Task.CompletedTask;
        }

        public Task<bool> Remove(object id)
        {
            Items.RemoveAll(n => n.Id == (int)id);
            return Task.FromResult(true);
        }

        public Task<IRandomizerList> Get(object id)
        {
            return Task.FromResult(Items.SingleOrDefault(n => n.Id == (int)id));
        }

        public Task<IEnumerable<IRandomizerList>> GetItems(int index, int count)
        {
            return Task.FromResult(Items.Skip(index * count).Take(count));
        }

        public Task<IRandomizerList> Add(IRandomizerList item)
        {
            if (Get(item.Id).Result != null)
            {
                return Task.FromResult<IRandomizerList>(null);
            }
            IRandomizerList inserted = new RandomizerList() { Id = item.Id, Name = item.Name };
            idCounter++;
            Items.Add(inserted);
            return Task.FromResult(inserted);
        }

        public Task<IRandomizerList> Update(object id, IRandomizerList item)
        {
            if (Get(item.Id).Result == null)
            {
                return Task.FromResult<IRandomizerList>(null);
            }
            Items.RemoveAll(n => n.Id == (int)id);
            Items.Add(item);
            return Task.FromResult(item);
        }

        public void Dispose()
        {
        }


    }
}
