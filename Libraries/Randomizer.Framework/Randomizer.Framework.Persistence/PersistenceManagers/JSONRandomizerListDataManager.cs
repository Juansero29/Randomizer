using EnigmatiKreations.Framework.Managers.Contract;
using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Randomizer.Framework.Persistence
{
    public class JSONRandomizerListDataManager : IDataManager<RandomizerList>
    {
        public Task<RandomizerList> Add(RandomizerList item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddRange(params RandomizerList[] items)
        {
            throw new NotImplementedException();
        }

        public Task<int> Count()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<RandomizerList> Get(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RandomizerList>> GetItems(int index, int count)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(object id)
        {
            throw new NotImplementedException();
        }

        public Task<RandomizerList> Update(object id, RandomizerList item)
        {
            throw new NotImplementedException();
        }
    }
}
