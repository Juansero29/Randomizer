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
        private IDataManager<IRandomizerList> _DataManager;
        #endregion

        public IRandomizerList CurrentList { get; set; }

        #region Constructors

        public ListsManager() : this(new EFRandomizerDataManager()) // Api DataManager
        {

        }

        public ListsManager(IDataManager<IRandomizerList> dataManager)
        {
            _DataManager = dataManager;
        }
        #endregion

        #region Methods
        public Task<IRandomizerList> AddList(IRandomizerList list)
        {
            return _DataManager.Add(list);
        }

        public Task<IRandomizerList> GetList(Guid id)
        {
            return _DataManager.Get(id);
        }


        public Task<IRandomizerList> Update(int id, IRandomizerList item)
        {
            return _DataManager.Update(id, item);
        }


        public Task<bool> RemoveList(IRandomizerList list)
        {
            return _DataManager.Remove(list.Id);
        }

        public Task<IEnumerable<IRandomizerList>> GetLists(int startIndex, int count)
        {
            return _DataManager.GetItems(startIndex, count);
        }

        public async Task<bool> AddItemToList(Guid listId, IRandomizerItem itemToAdd)
        {
            var list =  await _DataManager.Get(listId);
            list.AddItem(itemToAdd);
            return list.ContainsItem(itemToAdd);
        }

        public async Task<bool> RemoveItemFromList(Guid listId, IRandomizerItem itemToRemove)
        {
            var list = await _DataManager.Get(listId);
            list.RemoveItem(itemToRemove);
            return !list.ContainsItem(itemToRemove);
        }
        #endregion
    }
}
