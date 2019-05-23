using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Randomizer.Framework.ViewModels.Business
{
    /// <summary>
    /// The ViewModel for a <see cref="IRandomizerList"/>
    /// </summary>
    public class RandomizerListVM : BaseViewModel<IRandomizerList>
    {
        #region Properties
        /// <summary>
        /// The name of the list
        /// </summary>
        public string Name
        {
            get => Model.Name;
            set => SetValueOnModel(value, Model);
        }

        /// <summary>
        /// The list's items
        /// </summary>
        public IEnumerable<IRandomizerItem> Items => _Model.Items;

        #endregion

        #region Constructor(s)

        public RandomizerListVM() : this(new RandomizerList())
        {

        }

        public RandomizerListVM(IRandomizerList model) : base(model)
        {

        }

        #endregion

        #region Methods
        public void AddItem(IRandomizerItem item)
        {
            Model.AddItem(item);
            OnPropertyChanged(nameof(Items));
        }

        public void RemoveItem(IRandomizerItem item)
        {
            Model.RemoveItem(item);
            OnPropertyChanged(nameof(Items));
        }
        #endregion
    }
}
