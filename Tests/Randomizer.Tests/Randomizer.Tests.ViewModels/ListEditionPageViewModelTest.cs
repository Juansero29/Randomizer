using FluentAssertions;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.ViewModels.BaseViewModels;
using Randomizer.Framework.ViewModels.Pages;
using Randomizer.Tests.CommonTestData;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Randomizer.Tests.ViewModels
{
    public class ListEditionPageViewModelTest
    {
        private ListEditionPageViewModel _ViewModel;

        public ListEditionPageViewModelTest()
        {
            ViewModelLocator.RegisterDependencies(true);
            _ViewModel = new ListEditionPageViewModel();
        }

        [Fact]
        private void ConstructorTest()
        {
            var vm = new ListEditionPageViewModel();
            vm.Should().NotBeNull();
        }

        [Fact]
        private void InitializationTest()
        {   
            _ViewModel.AddItemCommand.Should().NotBeNull();
            _ViewModel.RemoveListItemCommand.Should().NotBeNull();
            _ViewModel.SaveListCommand.Should().NotBeNull();
            _ViewModel.EditListCommand.Should().NotBeNull();
            _ViewModel.DeleteListCommand.Should().NotBeNull();
            _ViewModel.RandomizeCommand.Should().NotBeNull();

            _ViewModel.IsEditMode.Should().BeFalse();
            _ViewModel.IsNew.Should().BeFalse();
            _ViewModel.ItemEntryText.Should().BeNullOrEmpty();
        }

        [Fact]
        private void EditModeParam_SetsEditModeProperty()
        {
            _ViewModel.IsEditModeParam = "true";
            _ViewModel.IsEditMode.Should().BeTrue();
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
            string itemName = "Blup";

            _ViewModel.AddItemCommand.Execute(itemName);
            _ViewModel.List.Items.Should().NotBeEmpty();
        }

        [Fact]
        private void RemoveListItemCommand()
        {
            string itemName = "Blup";

            throw new NotImplementedException("TODO");
            _ViewModel.AddItemCommand.Execute(itemName);
            _ViewModel.RemoveListItemCommand.Execute(itemName);
            _ViewModel.List.Items.Should().BeEmpty();
        }

        [Fact]
        private void EditListCommand()
        {
            _ViewModel.EditListCommand.Execute(null);

            _ViewModel.IsEditMode.Should().BeTrue();
            _ViewModel.ShowDeleteListToolbarItem.Should().BeTrue();
        }

        [Fact]
        private void SaveListCommand()
        {
            string listTitle = "My list";

            _ViewModel.IsEditModeParam = "true";
            _ViewModel.List.Name = listTitle;
            _ViewModel.SaveListCommand.Execute(null);

            _ViewModel.ToolbarTitle.Should().Be(listTitle);
            _ViewModel.IsEditMode.Should().Be(false);
            _ViewModel.ShowDeleteListToolbarItem.Should().BeFalse();
        }

        [Fact]
        private void DeleteListCommand()
        {
            throw new NotImplementedException("TODO");
        }               

        #endregion Command Tests
    }
}
