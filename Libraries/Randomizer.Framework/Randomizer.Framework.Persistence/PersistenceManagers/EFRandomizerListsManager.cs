using EnigmatiKreations.Framework.Managers.Contract;
using Microsoft.EntityFrameworkCore;
using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Randomizer.Framework.Persistence
{
    public class EFRandomizerListsManager : IDataManager<IRandomizerList>
    {

        private RandomizerContext _Manager = new RandomizerContext();

        public Task<bool> Add(IRandomizerList item)
        {
            var r = _Manager.Add(item);
            return Task.FromResult(r.State == EntityState.Added);
        }

        public Task<IRandomizerList> Get(Guid id)
        {
            var r = _Manager.Find(typeof(IRandomizerList), id);
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
            return Task.FromResult(_Manager.Lists.ToList() as IEnumerable<IRandomizerList>);
        }

        public Task<bool> Remove(Guid id)
        {
            var list = _Manager.Find(typeof(IRandomizerList), id);
            var r = _Manager.Remove(list);
            return Task.FromResult(r.State == EntityState.Deleted);
        }

        public Task<int> Save()
        {
            return Task.FromResult(_Manager.SaveChanges());
        }

        public Task<Tuple<bool, IRandomizerList>> Update(IRandomizerList item)
        {
            var oldItem = _Manager.Find(typeof(IRandomizerList), item.Id) as IRandomizerList;
            var r = _Manager.Update(item);
            var tuple = new Tuple<bool, IRandomizerList>(r.State == EntityState.Modified, oldItem);
            return Task.FromResult(tuple);
        }
    }

    /// <summary>
    /// The context for the Randomizer app
    /// </summary>
    public class RandomizerContext : DbContext
    {
        public DbSet<IRandomizerList> Lists { get; set; }


        public RandomizerContext()
        {
            SQLitePCL.Batteries_V2.Init();
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "randomizer.db3");

            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }
}
