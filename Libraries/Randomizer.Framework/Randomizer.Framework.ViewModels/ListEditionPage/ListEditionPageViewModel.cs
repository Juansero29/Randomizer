using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Randomizer.Framework.ViewModels.ListEditionPage
{
    public class ListEditionPageViewModel : BaseViewModel
    {
        private IRandomizerList _Model;

        public Command AddItemCommand { get; }
        public Command SaveListCommand { get; }

        public ListEditionPageViewModel(IRandomizerList model)
        {
            _Model = model;
            AddItemCommand = new Command<string>(AddItem);
            SaveListCommand = new Command(SaveList);

        }

        #region Model properties

        public string Name
        {
            get => _Model.Name;
            set
            {
                _Model.Name = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<IRandomizerItem> Items => _Model.Items;

        #endregion

        #region View properties

        private string _ToolbarTitle = "New List";
        public string ToolbarTitle
        {
            get => _ToolbarTitle;
            set => SetProperty(ref _ToolbarTitle, value);
        }

        private string _ItemEntryText;
        public string ItemEntryText
        {
            get => _ItemEntryText;
            set => SetProperty(ref _ItemEntryText, value);
        }

        private bool _IsEditMode;

        public bool IsEditMode
        {
            get => _IsEditMode;
            set => SetProperty(ref _IsEditMode, value);
        }

        #endregion

        #region Command methods

        private void AddItem(string item)
        {
            _Model.AddItem(new TextRandomizerItem { Name = item });
            ItemEntryText = "";
            OnPropertyChanged(nameof(Items));
        }

        private void SaveList()
        {
            ToolbarTitle = Name;
            IsEditMode = false;
        }

        #endregion

    }
}
