using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using FluentAssertions;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.ViewModels.Business;
using Randomizer.Framework.ViewModels.Pages;
using Randomizer.Tests.CommonTestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xunit;

namespace Randomizer.Tests.ViewModels
{
    public class ListEditionPageViewModelTest : IDisposable
    {
        #region Fields
        HomePageViewModel _HomePageViewModel;
        ListEditionPageViewModel _ViewModel; 
        #endregion

        #region Lifecycle
        public ListEditionPageViewModelTest()
        {
            // Register (create) view models in the view model locator
            ViewModelLocator.RegisterDependencies(true);

            _HomePageViewModel = new HomePageViewModel();
            // Resolve the view model to be used for the current test
            _ViewModel = new ListEditionPageViewModel();

        }

        public void Dispose()
        {
            _HomePageViewModel.Destroy();
            _ViewModel.Destroy();
        } 
        #endregion

        #region Methods
        private void PrepareContext()
        {
            // A list should have been created
            _HomePageViewModel.Lists.Add(new RandomizerListVM() { Name = "Unit Test List" });

            // The only list should have been selected to arrive to edition page
            _HomePageViewModel.ListTappedCommand.Execute(_HomePageViewModel.Lists[0]);

        } 
        #endregion


        [Fact]
        private void ConstructorTest()
        {
            _ViewModel.Should().NotBeNull();
        }

        [Fact]
        private void InitializationTest()
        {   
            _ViewModel.AddItemCommand.Should().NotBeNull();
            _ViewModel.RemoveListItemCommand.Should().NotBeNull();
            _ViewModel.SaveListCommand.Should().NotBeNull();
            _ViewModel.DeleteListCommand.Should().NotBeNull();
            _ViewModel.RandomizeCommand.Should().NotBeNull();
            _ViewModel.IsNew.Should().BeFalse();
            _ViewModel.ItemEntryText.Should().BeNullOrEmpty();
        }


        [Fact]
        private void NewListParam_SetsIsNewProperty()
        {
            _ViewModel.IsNewParam = "true";
            _ViewModel.IsNew.Should().BeTrue();
        }

        #region Command Tests

        [Fact]
        private void AddItemCommand()
        {
            PrepareContext();
            string itemName = "Blup";
            _ViewModel.AddItemCommand.Execute(itemName);
            _ViewModel.ListVM.Items.Should().NotBeEmpty();
        }

        [Fact]
        private void RemoveListItemCommand()
        {
            PrepareContext();

            _ViewModel.ListVM.RemoveAllItems();
            _ViewModel.ListVM.Items.Should().BeEmpty();
            string itemName = "Blup";
            _ViewModel.AddItemCommand.Execute(itemName);
            _ViewModel.ListVM.Items.Should().NotBeEmpty();
            _ViewModel.RemoveListItemCommand.Execute(_ViewModel.ListVM.Items.First());
            _ViewModel.ListVM.Items.Should().BeEmpty();
        }


        [Fact]
        private void SaveListCommand()
        {
            PrepareContext();
            string oldName = _ViewModel.ListVM.Name;
            string listTitle = "My list" + DateTime.Now.ToString();
            _ViewModel.ListVM.Name = listTitle;
            _ViewModel.SaveListCommand.Execute(null);
            _HomePageViewModel.Lists.Should().Contain(l => l.Name == listTitle);
            _HomePageViewModel.Lists.Should().NotContain(l => l.Name == oldName);
        }

        [Fact]
        private void DeleteListCommand()
        {
            PrepareContext();
            var deletedList = _ViewModel.ListVM;
            _ViewModel.DeleteListCommand.Execute(null);
            _HomePageViewModel.Lists.Should().NotContain(deletedList);
        }


        #endregion Command Tests
    }
}
