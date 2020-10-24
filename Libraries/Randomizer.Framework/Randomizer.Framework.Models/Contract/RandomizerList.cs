using EnigmatiKreations.Framework.Managers.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Randomizer.Framework.Models.Contract
{

    /// <summary>
    /// A list with a name that contains RandomizerItems and allows to manage them
    /// </summary>
    public abstract class RandomizerList
    {
        /// <summary>
        /// 
        /// The id for this list
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of this list
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The items on this list
        /// </summary>
        public ICollection<RandomizerItem> Items { get; set; }

        /// <summary>
        /// Method to add an item
        /// </summary>
        /// <param name="item"></param>
        public abstract void AddItem(RandomizerItem item);

        /// <summary>
        /// Method to remove an item
        /// </summary>
        /// <param name="item">The item to remove</param>
        /// <returns>Wether it was succesfully removed or not</returns>
        public abstract bool RemoveItem(RandomizerItem item);

        /// <summary>
        /// Method to see if the list contains an item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public abstract bool ContainsItem(RandomizerItem item);

        /// <summary>
        /// Recovers an item by its id
        /// </summary>
        /// <param name="id">The id of the item</param>
        /// <returns>The item that was found or null</returns>
        public abstract RandomizerItem GetItem(object id);

        /// <summary>
        /// Method to update an item in this list
        /// </summary>
        /// <param name="id">The id of the item to update</param>
        /// <param name="item">The item containing the new information</param>
        /// <returns></returns>
        public abstract RandomizerItem UpdateItem(object id, RandomizerItem item);

    }
}
