using EnigmatiKreations.Framework.Managers.Contract;
using Microsoft.EntityFrameworkCore;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Persistence.PersistenceManagers.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Randomizer.Framework.Persistence
{
    /// <summary>
    /// An implementation of <see cref="IDataManager{T}"/> that uses <see cref="Microsoft.EntityFrameworkCore"/>
    /// </summary>
    public class EFRandomizerDataManager : IDataManager<IRandomizerList>
    {

        private RandomizerContext _Context;
        private EFUnitOfWork _UnitOfWork;


        public EFRandomizerDataManager()
        {
            // Context init
            _Context = new RandomizerContext();

            // Unit of work init
            _UnitOfWork = new EFUnitOfWork(_Context);

            // Create database
            _Context.Database.EnsureCreated();
        }

        public Task<int> Count()
        {
            return _UnitOfWork.Repository<IRandomizerList>().Count();
        }
        public async Task<bool> Add(IRandomizerList item)
        {
            var r = await _UnitOfWork.Repository<IRandomizerList>().Add(item);
            await _UnitOfWork.SaveChangesAsync();
            return r;
        }

        public async Task<bool> AddRange(params IRandomizerList[] items)
        {
            var r = await _UnitOfWork.Repository<IRandomizerList>().AddRange(items);
            await _UnitOfWork.SaveChangesAsync();
            return r;
        }



        public async Task<IRandomizerList> Get(object id)
        {
            var r = await _UnitOfWork.Repository<IRandomizerList>().Get(id);
            await _UnitOfWork.SaveChangesAsync();
            return r;
        }


        public async Task<IEnumerable<IRandomizerList>> GetItems(int index, int count)
        {
            var r = await _UnitOfWork.Repository<IRandomizerList>().GetItems(index, count);
            await _UnitOfWork.SaveChangesAsync();
            return r;
        }

        public async Task<bool> Remove(object id)
        {
            var r = await _UnitOfWork.Repository<IRandomizerList>().Remove(id);
            await _UnitOfWork.SaveChangesAsync();
            return r;
        }

        public async Task<IRandomizerList> Update(IRandomizerList item)
        {

            var r = await _UnitOfWork.Repository<IRandomizerList>().Update(item);
            await _UnitOfWork.SaveChangesAsync();
            return r;
        }


        public async Task<IRandomizerList> Update(object id, IRandomizerList item)
        {
            var r = await _UnitOfWork.Repository<IRandomizerList>().Update(id, item);
            await _UnitOfWork.SaveChangesAsync();
            return r;

        }

        public void Dispose()
        {
            _UnitOfWork?.Dispose();
            _Context?.Dispose();
        }
    }

}
