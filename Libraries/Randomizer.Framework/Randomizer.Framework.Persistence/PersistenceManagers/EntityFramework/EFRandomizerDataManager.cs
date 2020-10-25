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
    public class EFRandomizerDataManager : IDataManager<RandomizerList>
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
            return _UnitOfWork.Repository<RandomizerList>().Count();
        }
        public async Task<RandomizerList> Add(RandomizerList item)
        {
            try
            {
                var r = await _UnitOfWork.Repository<RandomizerList>().Add(item);
                await _UnitOfWork.SaveChangesAsync();
                return r;
            }
            catch(Exception)
            {
                return null;
            }

        }

        public async Task<bool> AddRange(params RandomizerList[] items)
        {
            var r = await _UnitOfWork.Repository<RandomizerList>().AddRange(items);
            await _UnitOfWork.SaveChangesAsync();
            return r;
        }



        public async Task<RandomizerList> Get(object id)
        {
            var r = await _UnitOfWork.Repository<RandomizerList>().Get(id);
            await _UnitOfWork.SaveChangesAsync();
            return r;
        }


        public async Task<IEnumerable<RandomizerList>> GetItems(int index, int count)
        {
            var r = await _UnitOfWork.Repository<RandomizerList>().GetItems(index, count);
            await _UnitOfWork.SaveChangesAsync();
            return r;
        }

        public async Task<bool> Remove(object id)
        {
            var r = await _UnitOfWork.Repository<RandomizerList>().Remove(id);
            await _UnitOfWork.SaveChangesAsync();
            return r;
        }

        public async Task<RandomizerList> Update(RandomizerList item)
        {

            var r = await _UnitOfWork.Repository<RandomizerList>().Update(item);
            await _UnitOfWork.SaveChangesAsync();
            return r;
        }


        public async Task<RandomizerList> Update(object id, RandomizerList item)
        {
            var r = await _UnitOfWork.Repository<RandomizerList>().Update(id, item);
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
