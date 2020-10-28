using System;
using System.Collections.Generic;
using System.Text;
using Randomizer.Framework.Persistence;
using Randomizer.Framework.Services.Alerts;
using Randomizer.Framework.Services.Navigation;
using Randomizer.Framework.ViewModels.Business;
using Randomizer.Framework.ViewModels.Business.Items;
using Randomizer.Tests.CommonTestData;
using Xunit;
using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using EnigmatiKreations.Framework.Services.Navigation;
using EnigmatiKreations.Framework.Services.Alerts;
using Randomizer.Framework.Models;
using FluentAssertions;
using Randomizer.Tests.Common.ViewModels;
using System.Threading.Tasks;

namespace Randomizer.Tests.ViewModels.Business
{
    public class ListsManagerVMTests
    {

        public ListsManagerVMTests()
        {
            RegisterServicesInContainer();
        }

        private void RegisterServicesInContainer()
        {
            do
            {
                Container.PrepareNewBuilder();
                Container.RegisterDependency(new AlertsMockService(), typeof(IAlertsService), true);
            } while(!Container.BuildContainer());
        }

        [Fact]
        private void ConstructorTest()
        {
            var model = new ListsManager(new TestsRandomizerDataManager());
            var vm = new ListsManagerVM(model);
            vm.Should().NotBeNull();
        }

        [Theory]
        [ClassData(typeof(RandomizerListVMTestData))]
        private async Task AddListTest(RandomizerListVM list)
        {
            // Actual test code here.
            var model = new ListsManager(new TestsRandomizerDataManager());
            var vm = new ListsManagerVM(model);
            vm.ListsVM.Should().BeEmpty();
            await vm.AddListCommand.ExecuteAsync(list);
            await vm.RefreshLists();
            vm.ListsVM.Should().Contain(l => l.Equals(list));
        }

        [Theory]
        [ClassData(typeof(RandomizerListVMTestData))]
        private async Task AddListTwiceTest(RandomizerListVM list)
        {
         
            var model = new ListsManager(new TestsRandomizerDataManager());
            var vm = new ListsManagerVM(model);
            vm.ListsVM.Should().BeEmpty();
            await vm.AddListCommand.ExecuteAsync(list);
            vm.RefreshLists().Wait();
            await vm.AddListCommand.ExecuteAsync(list);
            await vm.RefreshLists();
            vm.ListsVM.Should().OnlyHaveUniqueItems();
        }

        [Theory]
        [ClassData(typeof(RandomizerListVMTestData))]
        private async Task UpdateListTest(RandomizerListVM list)
        {
            var model = new ListsManager(new TestsRandomizerDataManager());
            var vm = new ListsManagerVM(model);
            vm.ListsVM.Should().BeEmpty();

            await vm.AddListCommand.ExecuteAsync(list);
            await vm.RefreshLists();
            vm.ListsVM.Should().Contain(l => l.Equals(list));


            list.Name = "Updated Name";

            await vm.UpdateListCommand.ExecuteAsync(list);
            await vm.RefreshLists();
            vm.ListsVM.Should().Contain(l => l.Equals(list));
        }

        [Theory]
        [ClassData(typeof(RandomizerListVMTestData))]
        private async Task DeleteListTest(RandomizerListVM list)
        {
            var model = new ListsManager(new TestsRandomizerDataManager());
            var vm = new ListsManagerVM(model);
            vm.ListsVM.Should().BeEmpty();

            await vm.AddListCommand.ExecuteAsync(list);
            await vm.RefreshLists();
            vm.ListsVM.Should().Contain(l => l.Equals(list));


            await vm.DeleteListCommand.ExecuteAsync(list);
            await vm.RefreshLists();
            vm.ListsVM.Should().NotContain(l => l.Equals(list));
        }
    }
}