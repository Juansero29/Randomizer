using EnigmatiKreations.Framework.Managers.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Randomizer.Framework.Persistence.PersistenceManagers.EntityFramework
{
    /// <summary>
    /// A Unit Of Work for Entity Framework
    /// </summary>
    /// <remarks>
    /// Probably useless since DbContext is already a Unit of Work / Repo by its own. I'm just trying shit out.
    /// </remarks>
    public class EFUnitOfWork : IUnitOfWork
    {
        #region Private Fields
        private readonly DbContext _Context;
        #endregion


        #region Properties
        /// <summary>
        /// Indicates wether we want to track the entries in this Unit Of Work or not
        /// </summary>
        bool NoTracking { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Builds a new Unit of Work for Entity Framework
        /// </summary>
        /// <param name="context">The context that it is tied to</param>
        /// <param name="noTracking">Wether we want to track or not</param>
        public EFUnitOfWork(DbContext context, bool noTracking = true)
        {
            _Context = context;
            NoTracking = noTracking;
            if (NoTracking)
            {
                _Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

        }
        #endregion
        #region Methods
        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return new EFRepository<TEntity>(_Context);
        }

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await _Context?.SaveChangesAsync(cancellationToken);
            foreach (var entity in _Context.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Detached))
            {
                entity.State = EntityState.Detached;
            }
            return result;
        }

        public virtual async Task CancelChangesAsync(CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                foreach (var entry in _Context.ChangeTracker.Entries()
                    .Where(e => e.State != EntityState.Unchanged))
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                        case EntityState.Deleted:
                            entry.Reload();
                            break;
                    }
                }
            });
        }

        public void Dispose()
        {
            _Context?.Dispose();
        } 
        #endregion

    }
}
