using EnigmatiKreations.Framework.Managers.Contract;
using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Randomizer.Framework.Persistence
{
    /// <summary>
    /// A manager for lists and its items
    /// </summary>
    public class ListsManager
    {
        #region Private Fields
        private IDataManager<RandomizerList> _DataManager;
        #endregion

        public RandomizerList CurrentList { get; set; }

        #region Constructors

        public ListsManager() : this(new EFRandomizerDataManager()) // EFDatamanaer, Api DataManager, Tests DataManager, JsonDataManager...
        {

        }

        public ListsManager(IDataManager<RandomizerList> dataManager)
        {
            _DataManager = dataManager;
        }
        #endregion

        #region Methods
        public Task<RandomizerList> AddList(RandomizerList list)
        {
            return _DataManager.Add(list);
        }

        public Task<RandomizerList> GetList(object id)
        {
            return _DataManager.Get(id);
        }


        public Task<RandomizerList> Update(object id, RandomizerList item)
        {
            return _DataManager.Update(id, item);
        }


        public Task<bool> RemoveList(RandomizerList list)
        {
            return _DataManager.Remove(list.Id);
        }

        public Task<IEnumerable<RandomizerList>> GetLists(int startIndex, int count)
        {
            return _DataManager.GetItems(startIndex, count);
        }

        public async Task<bool> AddItemToList(object listId, RandomizerItem itemToAdd)
        {
            var list =  await _DataManager.Get(listId);
            list.AddItem(itemToAdd);
            return list.ContainsItem(itemToAdd);
        }

        public async Task<bool> RemoveItemFromList(object listId, RandomizerItem itemToRemove)
        {
            var list = await _DataManager.Get(listId);
            list.RemoveItem(itemToRemove);
            return !list.ContainsItem(itemToRemove);
        }


        public async Task<bool> UpdateItemInList(object listId, RandomizerItem itemToUpdate)
        {
            var list = await _DataManager.Get(listId);
            if (!list.ContainsItem(itemToUpdate)) return false;
            var r = list.UpdateItem(itemToUpdate);
            return r != null;
        }
        
        #endregion
    }
}
