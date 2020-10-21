using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnigmatiKreations.Framework.Managers.Contract
{
    /// <summary>
    /// Acts as a manager for an special type of class
    /// </summary>
    /// <typeparam name="T">The type of the class</typeparam>
    /// <remarks>Generic type needs to be <see cref="IIdentifiable"/></remarks>
    public interface IDataManager<T> : IDisposable where T : class
    {
        /// <summary>
        /// Adds an item to the manager
        /// </summary>
        /// <param name="item">the item</param>
        /// <returns>A task that defines wether the action was completed succesfully or not</returns>
        Task<T> Add(T item);

        /// <summary>
        /// Add multiple items at once
        /// </summary>
        /// <param name="items">The items to add</param>
        /// <returns>Wether this was succesful or not</returns>
        Task<bool> AddRange(params T[] items);

        /// <summary>
        /// Gets an item from the manager
        /// </summary>
        /// <param name="id">The id of the item to get</param>
        /// <returns>The item</returns>
        Task<T> Get(object id);

        /// <summary>
        /// Updates the item in the manager with the informations of the one passed in the parameters.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>A task containing two objects, a boolean to know if the update was successful and if it was, the old item that was deleted</returns>
        /// <remarks>If the item doesn't exist in the manager or the update wasn't succesful, it will return false and a null old item</remarks>
        Task<T> Update(object id, T item);

        /// <summary>
        /// Removes the item from the manager
        /// </summary>
        /// <param name="id">The id of the item to remove</param>
        /// <returns>A task that defines wether the action was completed succesfully or not</returns>
        Task<bool> Remove(object id);



        /// <summary>
        /// Recovers all the items stored in the manager
        /// </summary>
        /// <param name="count">How many items starting at that index</param>
        /// <param name="index">The starting index to get the items</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all of the items in the manager</returns>
        Task<IEnumerable<T>> GetItems(int index, int count);

        /// <summary>
        /// Counts the number of items in the manager
        /// </summary>
        /// <returns>The items of elements</returns>
        Task<int> Count();

    }
}
