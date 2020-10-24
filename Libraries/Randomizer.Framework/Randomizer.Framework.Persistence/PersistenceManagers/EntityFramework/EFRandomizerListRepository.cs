using Microsoft.EntityFrameworkCore;
using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Randomizer.Framework.Persistence.PersistenceManagers.EntityFramework
{
    public class EFRandomizerListRepository : EFGenericRepository<RandomizerList>
    {
        private DbContext _DbContext;
        private DbSet<RandomizerList> _Set;

        public EFRandomizerListRepository(DbContext dbContext) : base(dbContext)
        {
            _DbContext = dbContext;
            _Set = _DbContext?.Set<RandomizerList>();
        }

        public override async Task<RandomizerList> Add(RandomizerList item)
        {
            var result = await _Set.AddAsync(item);
            return result.Entity;
        }

        public override async Task<RandomizerList> Get(object id)
        {
            var entity = await _Set.FindAsync(id);
            return entity;
        }
    }
}
