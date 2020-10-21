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

        public Task<bool> Add(IRandomizerList item)
        {
            var r = _Context.Add(item);
            return Task.FromResult(r.State == EntityState.Added);
        }

        public Task<bool> AddRange(params IRandomizerList[] items)
        {
            throw new NotImplementedException();
        }

        public Task<int> Count()
        {
            throw new NotImplementedException();
        }

        public Task<IRandomizerList> Get(object id)
        {
            var r = _Context.Find(typeof(IRandomizerList), id);
            if (r is IRandomizerList list)
            {
                return Task.FromResult(list);
            }
            else
            {
                return Task.FromResult(default(IRandomizerList));
            }
        }


        public Task<IEnumerable<IRandomizerList>> GetItems()
        {
            return Task.FromResult(_Context.Lists.ToList() as IEnumerable<IRandomizerList>);
        }

        public Task<IEnumerable<IRandomizerList>> GetItems(int index, int count)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(object id)
        {
            var list = _Context.Find(typeof(IRandomizerList), id);
            var r = _Context.Remove(list);
            return Task.FromResult(r.State == EntityState.Deleted);
        }

        public Task<int> Save()
        {
            return Task.FromResult(_Context.SaveChanges());
        }

        public Task<Tuple<bool, IRandomizerList>> Update(IRandomizerList item)
        {
            var oldItem = _Context.Find(typeof(IRandomizerList), item.Id) as IRandomizerList;
            var r = _Context.Update(item);
            var tuple = new Tuple<bool, IRandomizerList>(r.State == EntityState.Modified, oldItem);
            return Task.FromResult(tuple);
        }

        public Task<Tuple<bool, IRandomizerList>> Update(object id, IRandomizerList item)
        {
            throw new NotImplementedException();
        }
    }

}
