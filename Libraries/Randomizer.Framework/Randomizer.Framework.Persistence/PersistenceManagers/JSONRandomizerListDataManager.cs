using EnigmatiKreations.Framework.Managers.Contract;
using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Randomizer.Framework.Persistence
{
    public class JSONRandomizerListDataManager : IDataManager<IRandomizerList>
    {
        public Task<IRandomizerList> Add(IRandomizerList item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddRange(params IRandomizerList[] items)
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

        public Task<IRandomizerList> Get(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IRandomizerList>> GetItems(int index, int count)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IRandomizerList> Update(object id, IRandomizerList item)
        {
            throw new NotImplementedException();
        }
    }
}
