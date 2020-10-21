using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnigmatiKreations.Framework.Managers.Contract
{
    /// <summary>
    /// What a Unit of Work has to be
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// A method that creates a new instance of a <see cref="IRepository{TEntity}"/> to manage the specified entity
        /// </summary>
        /// <typeparam name="TEntity">The specified entity</typeparam>
        /// <returns>The repository</returns>
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;

        /// <summary>
        /// Saves the transaction as a whole
        /// </summary>
        /// <param name="cancellationToken">A cancellation token</param>
        /// <returns>An awaitable task with the number of records saved</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Cancels the transaction as a whole
        /// </summary>
        /// <param name="cancellationToken">A cancellation token</param>
        /// <returns>An awaitable task</returns>
        Task CancelChangesAsync(CancellationToken cancellationToken = default);
    }
}
