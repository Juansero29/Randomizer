using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using EnigmatiKreations.Framework.Utils;
using Randomizer.Framework.ViewModels.Business;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Randomizer.DataTemplateSelectors
{

    /// <summary>
    /// Selects the template for a <see cref="IRandomizerList"/> so be shown in a list
    /// </summary>
    public class ListItemCardDTS : DataTemplateSelector
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {

            var list = CastOrThrow<RandomizerListVM>(item);
            Collection<RandomizerListVM> lists;

            if (container is CollectionView cv)
            {
                lists = CastOrThrow<Collection<RandomizerListVM>>(cv.ItemsSource);
            }
            else if (container is ListView lv)
            {
                lists = CastOrThrow<Collection<RandomizerListVM>>(lv.ItemsSource);
            }
            else
            {
                throw new InvalidOperationException
                                   ("Unable to load DataTemplate. Please use a CollectionView or a ListView as container.");
            }

            var isEvenPosition = (lists.IndexOf(list) % 2) == 0;
            string templateResource = null;

            if (isEvenPosition)
            {
                if (container is CollectionView)
                    templateResource = "RandomizerListItemCardEven_CollectionViewTemplate";
                else if (container is ListView)
                    templateResource = "RandomizerListItemCardEven_ListViewTemplate";
            }
            else
            {
                if (container is CollectionView)
                    templateResource = "RandomizerListItemCardOdd_CollectionViewTemplate";
                else if (container is ListView)
                    templateResource = "RandomizerListItemCardOdd_ListViewTemplate";
            }

            var dataTemplate = Tools.TryFindResource(templateResource) as DataTemplate;
            return dataTemplate;
        }

        private T CastOrThrow<T>(object item)
        {
            try
            {
                return (T)item;
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException($"Bad BindableObject. Unable to cast {item} to {typeof(T)}");
            }
        }
    }
}
