using FluentAssertions;
using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Persistence.PersistenceManagers.EntityFramework;
using Randomizer.Tests.Persistence.EntityFramework;
using System;
using System.Linq;
using Xunit;

namespace Randomizer.Tests.Persistence.EntityFramework
{
    public class RandomizerListsTests
    {
        [Fact]
        public async void GetTest()
        {
            using var factory = new TestContextFactory();
            using var context = factory.CreateContext();
            using var unitOfWork = new EFUnitOfWork(context);
            context.Database.EnsureCreated();

            (await unitOfWork.Repository<SimpleRandomizerList>().Get(3)).Name.Should().Be("People");
            (await unitOfWork.Repository<SimpleRandomizerList>().Get(4)).Should().BeNull();
        }

        [Fact]
        public async void GetItemsTest()
        {
            using var factory = new TestContextFactory();
            using var context = factory.CreateContext();
            using var unitOfWork = new EFUnitOfWork(context);
            context.Database.EnsureCreated();

            (await unitOfWork.Repository<RandomizerList>().GetItems(0, 10)).Count().Should().Be(3);
            (await unitOfWork.Repository<RandomizerList>().GetItems(0, 2)).Count().Should().Be(2);
            (await unitOfWork.Repository<RandomizerList>().GetItems(1, 2)).Count().Should().Be(1);
        }

        [Fact]
        public async void AddTest()
        {
            using var factory = new TestContextFactory();
            using var context = factory.CreateContext();
            using var unitOfWork = new EFUnitOfWork(context);
            context.Database.EnsureCreated();

            RandomizerList l = new SimpleRandomizerList { Name = "My list" };

            l = await unitOfWork.Repository<RandomizerList>().Add(l);
            l.Should().NotBeNull();
            await unitOfWork.SaveChangesAsync();
            (await unitOfWork.Repository<RandomizerList>().GetItems(0, 10)).Count().Should().Be(4);
        }

        [Fact]
        public async void AddRangeTest()
        {
            using var factory = new TestContextFactory();
            using var context = factory.CreateContext();
            using var unitOfWork = new EFUnitOfWork(context);
            context.Database.EnsureCreated();

            RandomizerList l = new SimpleRandomizerList { Name = "Ma liste incroyable", };
            RandomizerList l2 = new SimpleRandomizerList { Name = "My list" };
            RandomizerList l3 = new SimpleRandomizerList { Name = "Mi lista genial" };

            var result = await unitOfWork.Repository<RandomizerList>().AddRange(l, l2, l3);
            result.Should().BeTrue();
            await unitOfWork.SaveChangesAsync();

            (await unitOfWork.Repository<RandomizerList>().GetItems(0, 10)).Count().Should().Be(6);
        }

        [Fact]
        public async void UpdateTest()
        {
            using var factory = new TestContextFactory();
            using var context = factory.CreateContext();
            using var unitOfWork = new EFUnitOfWork(context);
            context.Database.EnsureCreated();

            var myList = await unitOfWork.Repository<RandomizerList>().Get(1);
            
            myList.Name = "My list!";
            
            await unitOfWork.Repository<RandomizerList>().Update(myList);
            await unitOfWork.SaveChangesAsync();

            (await unitOfWork.Repository<RandomizerList>().Get(1)).Name.Should().Be("My list!");
            (await unitOfWork.Repository<RandomizerList>().GetItems(0, 10)).Where(n => n.Name.Equals("Beers")).Should().BeEmpty();
        }

        [Fact]
        public async void Delete()
        {
            using var factory = new TestContextFactory();
            using var context = factory.CreateContext();
            using var unitOfWork = new EFUnitOfWork(context);
            context.Database.EnsureCreated();

            var chewie = await unitOfWork.Repository<RandomizerList>().Get(1);
            await unitOfWork.Repository<RandomizerList>().Remove(chewie);
            await unitOfWork.SaveChangesAsync();
            Assert.Empty((await unitOfWork.Repository<RandomizerList>().GetItems(0, 10)).Where(n => n.Name.Equals("Beers")));
        }

        [Fact]
        public async void RejectChangesTest()
        {
            using var factory = new TestContextFactory();
            using var context = factory.CreateContext();
            using var unitOfWork = new EFUnitOfWork(context);
            context.Database.EnsureCreated();

            await unitOfWork.Repository<RandomizerList>().Remove(1);
            await unitOfWork.CancelChangesAsync();
            await unitOfWork.SaveChangesAsync();
            (await unitOfWork.Repository<RandomizerList>().GetItems(0, 10)).Count().Should().Be(3);
        }
    }
}
