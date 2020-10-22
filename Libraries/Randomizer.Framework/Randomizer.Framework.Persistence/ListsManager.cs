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
    /// A manager for lists
    /// </summary>
    public class ListsManager
    {
        #region Private Fields
        private IDataManager<Models.Contract.RandomizerList> _DataManager;
        #endregion

        public Models.Contract.RandomizerList CurrentList { get; set; }

        #region Constructors

        public ListsManager() : this(new EFRandomizerDataManager()) // Api DataManager
        {

        }

        public ListsManager(IDataManager<Models.Contract.RandomizerList> dataManager)
        {
            _DataManager = dataManager;
        }
        #endregion

        #region Methods
        public Task<Models.Contract.RandomizerList> AddList(Models.Contract.RandomizerList list)
        {
            return _DataManager.Add(list);
        }

        public Task<Models.Contract.RandomizerList> GetList(Guid id)
        {
            return _DataManager.Get(id);
        }


        public Task<Models.Contract.RandomizerList> Update(int id, Models.Contract.RandomizerList item)
        {
            return _DataManager.Update(id, item);
        }


        public Task<bool> RemoveList(Models.Contract.RandomizerList list)
        {
            return _DataManager.Remove(list.Id);
        }

        public Task<IEnumerable<Models.Contract.RandomizerList>> GetLists(int startIndex, int count)
        {
            return _DataManager.GetItems(startIndex, count);
        }

        public async Task<bool> AddItemToList(Guid listId, RandomizerItem itemToAdd)
        {
            var list =  await _DataManager.Get(listId);
            list.AddItem(itemToAdd);
            return list.ContainsItem(itemToAdd);
        }

        public async Task<bool> RemoveItemFromList(Guid listId, RandomizerItem itemToRemove)
        {
            var list = await _DataManager.Get(listId);
            list.RemoveItem(itemToRemove);
            return !list.ContainsItem(itemToRemove);
        }
        #endregion
    }
}
