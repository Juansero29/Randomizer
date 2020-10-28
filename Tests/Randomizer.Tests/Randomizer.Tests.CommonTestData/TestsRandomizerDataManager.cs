using EnigmatiKreations.Framework.Managers.Contract;
using Microsoft.EntityFrameworkCore;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Persistence.PersistenceManagers.EntityFramework;
using Randomizer.Tests.Persistence.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;



namespace Randomizer.Tests.ViewModels
{
    /// <summary>
    /// An implementation of <see cref="IDataManager{T}"/> that uses <see cref="Microsoft.EntityFrameworkCore"/>
    /// </summary>
    public class TestsRandomizerDataManager : IDataManager<RandomizerList>
    {
        private TestContextFactory _Factory = new TestContextFactory();
        private TestContext Context;
        private EFUnitOfWork UnitOfWork;


        public TestsRandomizerDataManager()
        {
            Context = _Factory.CreateContext();
            UnitOfWork = new EFUnitOfWork(Context);
            // Create database
            Context.Database.EnsureCreated();
        }

        public Task<int> Count()
        {
            return UnitOfWork.Repository<RandomizerList>().Count();
        }
        public async Task<RandomizerList> Add(RandomizerList item)
        {
            try
            {
                var r = await UnitOfWork.Repository<RandomizerList>().Add(item);
                await UnitOfWork.SaveChangesAsync();
                return r;
            }
            catch(Exception)
            {
                return null;
            }

        }

        public async Task<bool> AddRange(params RandomizerList[] items)
        {
            var r = await UnitOfWork.Repository<RandomizerList>().AddRange(items);
            await UnitOfWork.SaveChangesAsync();
            return r;
        }



        public async Task<RandomizerList> Get(object id)
        {
            var r = await UnitOfWork.Repository<RandomizerList>().Get(id);
            await UnitOfWork.SaveChangesAsync();
            return r;
        }


        public async Task<IEnumerable<RandomizerList>> GetItems(int index, int count)
        {
            var r = await UnitOfWork.Repository<RandomizerList>().GetItems(index, count);
            await UnitOfWork.SaveChangesAsync();
            return r;
        }

        public async Task<bool> Remove(object id)
        {
            var r = await UnitOfWork.Repository<RandomizerList>().Remove(id);
            await UnitOfWork.SaveChangesAsync();
            return r;
        }

        public async Task<RandomizerList> Update(RandomizerList item)
        {

            var r = await UnitOfWork.Repository<RandomizerList>().Update(item);
            await UnitOfWork.SaveChangesAsync();
            return r;
        }


        public async Task<RandomizerList> Update(object id, RandomizerList item)
        {
            var r = await UnitOfWork.Repository<RandomizerList>().Update(id, item);
            await UnitOfWork.SaveChangesAsync();
            return r;
        }

        public void Dispose()
        {
            UnitOfWork?.Dispose();
            Context?.Dispose();
            _Factory?.Dispose();
        }

    }
}
