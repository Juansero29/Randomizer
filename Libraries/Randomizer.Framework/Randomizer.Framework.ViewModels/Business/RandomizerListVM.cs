using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace Randomizer.Framework.ViewModels.Business
{
    /// <summary>
    /// The ViewModel for a <see cref="Models.Contract.RandomizerList"/>
    /// </summary>
    public class RandomizerListVM : BaseViewModel<Models.Contract.RandomizerList>
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
        public IEnumerable<RandomizerItem> Items => _Model.Items;

        #endregion

        #region Constructor(s)

        public RandomizerListVM() : this(new Models.SimpleRandomizerList())
        {

        }

        public RandomizerListVM(Models.Contract.RandomizerList model) : base(model)
        {

        }

        #endregion

        #region Methods
        public void AddItem(RandomizerItem item)
        {
            Model.AddItem(item);
            OnPropertyChanged(nameof(Items));
        }

        public void RemoveItem(RandomizerItem item)
        {
            Model.RemoveItem(item);
            OnPropertyChanged(nameof(Items));
        }

        public void RemoveAllItems()
        {
            Model.Items.ForEach(i => Model.RemoveItem(i));
        }


        #endregion
    }
}
