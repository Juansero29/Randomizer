using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.ViewModels.Business;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Randomizer.Framework.ViewModels.Pages
{
    /// <summary>
    /// The model of a page that presents a <see cref="RandomizerListVM"/> in edition
    /// </summary>
    [QueryProperty(nameof(IsEditModeParam), "editmode")]
    [QueryProperty(nameof(IsNewParam), "new")]
    public class ListEditionPageViewModel : BasePageViewModel
    {
        #region Fields
        private RandomizerListVM _List;
        private string _ToolbarTitle = "New List";
        private string _ItemEntryText;
        private bool _IsEditMode;
        #endregion

        #region Query Parameters
        /// <summary>
        /// A parameter indicating if the list in this view model is a new list
        /// </summary>
        public string IsNewParam
        {
            get => IsNew.ToString();
            set
            {
                bool.TryParse(value, out bool res);
                IsNew = res;
            }
        }

        /// <summary>
        /// A parameter indicating if this view model is in edit mode
        /// </summary>
        public string IsEditModeParam
        {
            get => IsEditMode.ToString();
            set
            {
                bool.TryParse(value, out bool res);
                IsEditMode = res;
            }
        }
        #endregion

        #region Properties
        public RandomizerListVM List
        {
            get => _List;
            set => SetValue(ref _List, value);
        }

        /// <summary>
        /// The toolbar's title
        /// </summary>
        public string ToolbarTitle
        {
            get => _ToolbarTitle;
            set => SetValue(ref _ToolbarTitle, value);
        }

        /// <summary>
        /// The item's entry text
        /// </summary>
        public string ItemEntryText
        {
            get => _ItemEntryText;
            set => SetValue(ref _ItemEntryText, value);
        }

        /// <summary>
        /// Is this page in edit mode?
        /// </summary>
        public bool IsEditMode
        {
            get => _IsEditMode;
            set => SetValue(ref _IsEditMode, value, onChanged: () =>
            {
                if (value == false)
                    IsNew = false;
                OnPropertyChanged(nameof(IsNew));
                OnPropertyChanged(nameof(ShowDeleteListToolbarItem));
            });
        }

        /// <summary>
        /// Is this a new list?
        /// </summary>
        public bool IsNew
        {
            get;
            set;
        }

        /// <summary>
        /// Should we show the delete icon on the page?
        /// </summary>
        public bool ShowDeleteListToolbarItem => !IsNew && IsEditMode;

        #endregion

        #region Commands
        public ICommand AddItemCommand { get; }
        public ICommand SaveListCommand { get; }
        public ICommand EditListCommand { get; }
        public ICommand DeleteListCommand { get; }
        public ICommand RandomizeCommand { get; }
        #endregion

        #region Constructor(s)
        public ListEditionPageViewModel()
        {
            _List = new RandomizerListVM();

            #region InitCommands
            AddItemCommand = new Command<string>(OnAddItem);
            SaveListCommand = new Command(OnSaveList);
            EditListCommand = new Command(OnEnterListEditionMode);
            DeleteListCommand = new Command(OnDeleteList);
            RandomizeCommand = new Command(OnRandomize);
            #endregion
        }

        #endregion

        #region Command Methods

        private void OnAddItem(string item)
        {
            ItemEntryText = "";
            _List.AddItem(item);
        }

        private void OnSaveList()
        {
            ToolbarTitle = _List.Name;
            IsEditMode = false;
        }

        private void OnEnterListEditionMode()
        {
            IsEditMode = true;
        }

        private void OnDeleteList()
        {
            try
            {
                throw new NotImplementedException("TODO");
            }
            catch (Exception)
            {
                Application.Current.MainPage.DisplayAlert("Ooops!", "This feature hasn't been implemented yet. Try again later!", "OK");
            }
        }

        private void OnRandomize()
        {
            try
            {
                throw new NotImplementedException("TODO #9");
            }
            catch (Exception)
            {
                Application.Current.MainPage.DisplayAlert("Ooops!", "This feature hasn't been implemented yet. Try again later!", "OK");
            }
        }

        #endregion
    }
}
