using Randomizer.Framework.Models;
using Randomizer.Framework.ViewModels.Business;
using System;
using Xamarin.Forms;
using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using Randomizer.Framework.Services.Resources;
using EnigmatiKreations.Framework.Services.Navigation;
using EnigmatiKreations.Framework.Services.Alerts;
using Randomizer.Framework.ViewModels.Commanding;
using System.Threading.Tasks;

namespace Randomizer.Framework.ViewModels.Pages
{
    /// <summary>
    /// The view model of a page that presents a <see cref="RandomizerListVM"/> in edition
    /// </summary>
    [QueryProperty(nameof(IsNewParam), "new")]
    public class ListEditionPageViewModel : BasePageViewModel
    {
        #region Fields
        private RandomizerListVM _ListVM;
        private string _ItemEntryText;
        private bool _IsNew;
        private ListsManagerVM Manager => Container.Resolve<ListsManagerVM>();
        #endregion

        #region Query Parameters
        /// <summary>
        /// A parameter indicating if the list in this view model is a new list
        /// </summary>
        public string IsNewParam
        {
            set
            {
                bool.TryParse(value, out bool res);
                IsNew = res;
            }
        }

        #endregion

        #region Properties



        /// <summary>
        /// The view model for the list we're editing
        /// </summary>
        public RandomizerListVM ListVM
        {
            get => _ListVM;
            set
            {
                SetValue(ref _ListVM, value);

                if (!(value is RandomizerListVM list)) return;
                if (IsNew) 
                {
                    Title = TextResources.NewListPageTitle;
                }
                else
                {
                    // If list isn't new, display the list title in the toolbar
                    Title = list.Name;
                }
            }
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
        /// Is this a new list?
        /// </summary>
        public bool IsNew
        {
            get => _IsNew;
            set
            {
                SetValue(ref _IsNew, value, onChanged: () =>
                {
                    if (value)
                    {
                        Title = TextResources.NewListPageTitle;
                        ListVM = new RandomizerListVM(new SimpleRandomizerList());
                        Manager.CurrentList = ListVM;
                    }
                    OnPropertyChanged(nameof(ShowDeleteListToolbarItem));
                });
            }
        }

        /// <summary>
        /// Should we show the delete icon on the page?
        /// </summary>
        public bool ShowDeleteListToolbarItem => !IsNew;

        #endregion

        #region Commands
        public ICommandAsync SaveListCommand { get; }
        public ICommandAsync DeleteListCommand { get; }
        public ICommandAsync RandomizeCommand { get; }

        #endregion

        #region Constructor(s)
        public ListEditionPageViewModel()
        {

            #region InitCommands
            SaveListCommand = new SimpleCommandAsync(SaveList, CanExecuteSaveList);
            DeleteListCommand = new SimpleCommandAsync(OnDeleteList, CanExecuteDeleteList);
            RandomizeCommand = new SimpleCommandAsync(OnRandomize, CanExecuteRandomize);
            #endregion


        }

        private void ListVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ListVM.Name):
                    (SaveListCommand as SimpleCommandAsync).RaiseCanExecuteChanged();
                    break;
            }
        }



        #endregion

        #region Command Methods



        private bool CanExecuteRandomize()
        {
            return true;
        }

        private bool CanExecuteDeleteList()
        { 
            return true;
        }

        private bool CanExecuteSaveList()
        {
            return ListVM?.Name.Length > 0;
        }

        public async Task SaveList()
        {
            if(IsNew)
            {
                await Manager.AddListCommand.ExecuteAsync(ListVM);
                IsNew = false;
            }
            else
            {
                await Manager.UpdateListCommand.ExecuteAsync(ListVM);
            }

            Manager.CurrentList = ListVM;
            
            await Container.Resolve<INavigationService>().GoBackAsync();
        }

        private async Task OnDeleteList()
        {
            await Manager.DeleteListCommand.ExecuteAsync(ListVM);
            await Container.Resolve<INavigationService>().GoBackAsync();
        }

        private async Task OnRandomize()
        {
            try
            {
                throw new NotImplementedException("TODO #9");
            }
            catch (Exception)
            {
                await Container.Resolve<IAlertsService>().ShowFeatureNotImplementedAlert();
            }
        }

        #endregion

        #region Methods

        public override async void Load(object parameter = null)
        {
            base.Load(parameter);
            ListVM = Manager.CurrentList;
            ListVM.PropertyChanged += ListVM_PropertyChanged;
            await ListVM.RefreshListCommand.ExecuteAsync();
        }

        public override void UnLoad(object parameter)
        {
            base.UnLoad(parameter);
        }

        public override void Destroy()
        {

            base.Destroy();
        }

        #endregion


    }
}
