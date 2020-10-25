using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Randomizer.Framework.ViewModels.Business.Items
{
    /// <summary>
    /// View model for a text randomizer item
    /// </summary>
    public class TextRandomizerItemVM : RandomizerItemVM
    {
        public string Name
        {
            get => (Model as TextRandomizerItem).Name;
            set => SetValueOnModel(value, Model as TextRandomizerItem);
        }

        public TextRandomizerItemVM(TextRandomizerItem model) : base(model)
        {
        }
    }
}
