﻿using EnigmatiKreations.Framework.Managers.Contract;
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
    /// A repository for Entity Framework
    /// </summary>
    /// <typeparam name="TEntity">The entity this repository will manage</typeparam>
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Private Fields
        private readonly DbContext _DbContext;
        private readonly DbSet<TEntity> _Set;
        #endregion

        #region Properties
        public IQueryable<TEntity> Set => _Set;

        #endregion

        #region Constructor
        public EFRepository(DbContext dbContext)
        {
            _DbContext = dbContext;
            _Set = _DbContext?.Set<TEntity>();
        }
        #endregion

        #region Methods
        public async Task<int> Count()
        {
            return await _Set.CountAsync();
        }

        public virtual async Task<TEntity> Get(object id)
        {
            var entity = await _Set.FindAsync(id);
            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> GetItems(int index, int count)
        {
            var result = await Task.Run(() => _Set.Skip(count * index).Take(count));
            return result;
        }

        public virtual async Task<bool> Add(TEntity item)
        {
            var result = await _Set.AddAsync(item);
            return result.State == EntityState.Added;
        }

        public virtual async Task<bool> AddRange(params TEntity[] items)
        {
            await _Set.AddRangeAsync(items);
            return true;
        }

        public virtual async Task<TEntity> Update(TEntity item)
        {
            var result = await Task.Run(() => _Set.Update(item));
            return result.Entity;
        }

        public virtual async Task<TEntity> Update(object id, TEntity item)
        {
            var result = await Update(item);
            return result;
        }

        public virtual async Task<bool> Remove(TEntity entity)
        {
            var r = await Task.Run(() => _Set.Remove(entity));
            return r.State == EntityState.Deleted;
        }

        public virtual async Task<bool> Remove(object id)
        {
            var entity = await Get(id);
            return await Remove(entity);
        }

        public virtual async Task Clear()
        {
            var allEntities = _Set.AsEnumerable();
            await Task.Run(() => _Set.RemoveRange(allEntities));
        }



        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int result = await _DbContext?.SaveChangesAsync(cancellationToken);
            return result;
        }

        public void Dispose()
        {
            _DbContext?.Dispose();
        } 
        #endregion

    }
}
