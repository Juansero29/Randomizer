using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Randomizer.Framework.Utils.DataTemplateSelectors
{

    /// <summary>
    /// Selects the template for a <see cref="IRandomizerList"/> so be shown in a list
    /// </summary>
    public class ListItemCardDTS : DataTemplateSelector
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var template = default(DataTemplate);
            if (!(container is CollectionView cv)) return template;
            if (!(item is IRandomizerList list)) return template;
            if (!(cv.ItemsSource is Collection<IRandomizerList> lists)) return template;

            if ((lists.IndexOf(list) % 2) == 0)
            {
                // It is a en even position
                template = Tools.TryFindResource("RandomizerListItemCardEvenTemplate") as DataTemplate;
            }
            else
            {
                // It is at an odd position
                template = Tools.TryFindResource("RandomizerListItemCardOddTemplate") as DataTemplate;
            }

            return template;
        }
    }
}
