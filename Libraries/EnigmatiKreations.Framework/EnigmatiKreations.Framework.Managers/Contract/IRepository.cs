using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnigmatiKreations.Framework.Managers.Contract
{
    /// <summary>
    /// What a repository has to be
    /// </summary>
    /// <typeparam name="TEntity">The entity the repository manages</typeparam>
    public interface IRepository<TEntity> : IDisposable, IDataManager<TEntity> where TEntity : class
    {
        /// <summary>
        /// The set that this repository holds
        /// </summary>
        IQueryable<TEntity> Set { get; }

        /// <summary>
        /// Update an item
        /// </summary>
        /// <param name="item">The item</param>
        Task<TEntity> Update(TEntity item);

        /// <summary>
        /// Remove an item
        /// </summary>
        /// <param name="entity">The item to remove</param>
        /// <returns>A task indicating if the remove was successful</returns>
        Task<bool> Remove(TEntity entity);

        /// <summary>
        /// Saves (persists) the current items in the Manager
        /// </summary>
        /// <returns>The number of saved records</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);


    }
}
