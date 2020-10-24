using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Randomizer.Framework.ViewModels.Business
{
    public class RandomizerItemVM : BaseViewModel<RandomizerItem>
    {
        public int Id
        {
            get
            {
                if (Model == null) return -1;
                return Model.Id;
            }

            set
            {
                SetValueOnModel(value, Model);
            }
        }

        public RandomizerItemVM(RandomizerItem model) : base(model)
        {
        }
    }
}
