using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Randomizer.Framework.ViewModels.ListEditionPage
{
    public class ListEditionPageViewModel<T> : BaseViewModel
    {
        private IRandomizerList<T> _Model;

        public Command AddItemCommand { get; }

        public ListEditionPageViewModel(IRandomizerList<T> model)
        {
            _Model = model;
            AddItemCommand = new Command<string>(AddItem);
            (_Model as IRandomizerList<string>).AddItem(new TextRandomizerItem { Value = "Hiii" });

        }

        public string Name {
            get => _Model.Name;
            set { _Model.Name = value;
                OnPropertyChanged();
            } 
        }

        private string _ItemEntryText;
        public string ItemEntryText
        {
            get => _ItemEntryText;
            set => SetProperty(ref _ItemEntryText, value);
        }

        private void AddItem(string itemName)
        {
            (_Model as IRandomizerList<string>).AddItem(new TextRandomizerItem { Value = itemName });
            ItemEntryText = "";
            OnPropertyChanged(nameof(Items));
        }

        public IEnumerable<IRandomizerItem<T>> Items
        {
            get => _Model.Items;
        } 

    }
}
