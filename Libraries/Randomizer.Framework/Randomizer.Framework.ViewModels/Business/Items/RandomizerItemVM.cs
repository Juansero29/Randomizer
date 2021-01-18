using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Randomizer.Framework.ViewModels.Business.Items
{
    public class RandomizerItemVM : BaseViewModel<RandomizerItem>
    {
        public string Id
        {
            get
            {
                if (Model == null) return string.Empty;
                return Model.Id.ToString();
            }

            set
            {
                SetValueOnModel(value, Model);
            }
        }

        public RandomizerListVM Parent
        {
            get => new RandomizerListVM(Model.Parent);
        }

        public RandomizerItemVM(RandomizerItem model) : base(model)
        {
        }
    }
}
