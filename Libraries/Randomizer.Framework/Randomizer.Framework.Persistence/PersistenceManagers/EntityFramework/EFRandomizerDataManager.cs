using EnigmatiKreations.Framework.Managers.Contract;
using Microsoft.EntityFrameworkCore;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Persistence.PersistenceManagers.EntityFramework;
using Randomizer.Framework.Persistence.PersistenceManagers.EntityFramework.ModelEFLink;
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
    public class EFRandomizerDataManager : IDataManager<RandomizerListEntity>
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
            return _UnitOfWork.Repository<RandomizerListEntity>().Count();
        }
        public async Task<RandomizerListEntity> Add(RandomizerListEntity item)
        {
            var r = await _UnitOfWork.Repository<RandomizerListEntity>().Add(item);
            await _UnitOfWork.SaveChangesAsync();
            return r;
        }

        public async Task<bool> AddRange(params RandomizerListEntity[] items)
        {
            var r = await _UnitOfWork.Repository<RandomizerListEntity>().AddRange(items);
            await _UnitOfWork.SaveChangesAsync();
            return r;
        }



        public async Task<RandomizerListEntity> Get(object id)
        {
            var r = await _UnitOfWork.Repository<RandomizerListEntity>().Get(id);
            await _UnitOfWork.SaveChangesAsync();
            return r;
        }


        public async Task<IEnumerable<RandomizerListEntity>> GetItems(int index, int count)
        {
            var r = await _UnitOfWork.Repository<RandomizerListEntity>().GetItems(index, count);
            await _UnitOfWork.SaveChangesAsync();
            return r;
        }

        public async Task<bool> Remove(object id)
        {
            var r = await _UnitOfWork.Repository<RandomizerListEntity>().Remove(id);
            await _UnitOfWork.SaveChangesAsync();
            return r;
        }

        public async Task<RandomizerListEntity> Update(RandomizerListEntity item)
        {

            var r = await _UnitOfWork.Repository<RandomizerListEntity>().Update(item);
            await _UnitOfWork.SaveChangesAsync();
            return r;
        }


        public async Task<RandomizerListEntity> Update(object id, RandomizerListEntity item)
        {
            var r = await _UnitOfWork.Repository<RandomizerListEntity>().Update(id, item);
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
