using EnigmatiKreations.Framework.Managers.Contract;
using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Randomizer.Framework.Persistence
{
    public class JSONRandomizerListDataManager : IDataManager<IRandomizerList>
    {
        public Task<bool> Add(IIdentifiable item)
        {
            throw new NotImplementedException();
        }

        public Task<IRandomizerList> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IRandomizerList>> GetItems()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<bool, IRandomizerList>> Update(IRandomizerList item)
        {
            throw new NotImplementedException();
        }
    }
}
