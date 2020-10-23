using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Persistence.PersistenceManagers.EntityFramework;
using Randomizer.Tests.Persistence.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace Randomizer.Tests.Persistence.EntityFramework
{
    public class RandomizerItemsTests
    {

        [Fact]
        public async void AddItemToNewListWithoutUnitOfWork()
        {
            // instatiate a new context
            using var context = new TestContext();

            // ensure the database has been created
            context.Database.EnsureCreated();

            // Create new list
            RandomizerList l = new SimpleRandomizerList { Name = "More Beers" };

            // the items to add
            var chouffe = new TextRandomizerItem("Chouffe", l);
            var goudale = new TextRandomizerItem("Goudale", l);
            var leffe = new TextRandomizerItem("Leffe", l);

            // adding the items
            l.AddItem(chouffe);
            l.AddItem(goudale);
            l.AddItem(leffe);

            // list should now have 3 items
            l.Items.Count().Should().Be(3);

            // adding the list to the RandomizerList set
            l = context.Set<RandomizerList>().Add(l).Entity;
            l.Should().NotBeNull();

            // the three items should still be there
            l.Items.Count.Should().Be(3);

            // saving changes
            await context.SaveChangesAsync();

            // the number of total lists in DB should now be three
            (context.Set<RandomizerList>().Take(10)).Count().Should().Be(4);

            // recovering the list in three different ways
            var moreBeers = context.Set<SimpleRandomizerList>().Include(l => l.Items).Where(i => i.Id == l.Id).FirstOrDefault();
            var moreBeers2 = context.Set<RandomizerList>().Include(l => l.Items).ToList().Where(l => l.Id == moreBeers.Id).FirstOrDefault();
            var moreBeers3 = context.Lists.Include(list => list.Items).ToList().FirstOrDefault(l => l.Id == moreBeers.Id);

            // all ways should return the same list with three items
            moreBeers.Items.Count().Should().Be(3);
            moreBeers2.Items.Count().Should().Be(3);
            moreBeers3.Items.Count().Should().Be(3);
        }


        [Fact]
        public async void AddItemToNewListWithUnitOfWork()
        {
            using var context = new TestContext();
            using var unitOfWork = new EFUnitOfWork(context);

            // ensure the database has been created
            context.Database.EnsureCreated();

            // new list
            RandomizerList l = new SimpleRandomizerList { Name = "More Beers" };

            // the items to add
            var chouffe = new TextRandomizerItem("Chouffe", l);
            var goudale = new TextRandomizerItem("Goudale", l);
            var leffe = new TextRandomizerItem("Leffe", l);

            // adding the items
            l.AddItem(chouffe);
            l.AddItem(goudale);
            l.AddItem(leffe);

            // adding the list
            l = await unitOfWork.Repository<RandomizerList>().Add(l);

            l.Should().NotBeNull();

            // item cound should still be three
            l.Items.Count.Should().Be(3);

            // saving changes
            await unitOfWork.SaveChangesAsync();

            // total lists saved should now be 4
            (await unitOfWork.Repository<RandomizerList>().GetItems(0, 10)).Count().Should().Be(4);

            // recovering the list in three different ways
            var moreBeers = (await unitOfWork.Repository<RandomizerList>().Get(l.Id));
            var moreBeers2 = (await unitOfWork.Repository<RandomizerList>().Set.Include(l => l.Items).Where(i => i.Id == l.Id).FirstOrDefaultAsync());
            var moreBeers3 = await unitOfWork.Repository<SimpleRandomizerList>().Set.Include(l => l.Items).Where(i => i.Id == l.Id).FirstOrDefaultAsync();

            // all ways should return the same list with three items
            moreBeers.Items.Count().Should().Be(3);
            moreBeers2.Items.Count().Should().Be(3);
            moreBeers3.Items.Count().Should().Be(3);
        }


        [Fact]
        public async void AddItemToPreExistantList()
        {
            using var context = new TestContext();
            using var unitOfWork = new EFUnitOfWork(context);
            context.Database.EnsureCreated();

            var list = (await unitOfWork.Repository<SimpleRandomizerList>().Get(1));
            list.AddItem(new TextRandomizerItem("Chouffe"));
            list.AddItem(new TextRandomizerItem("Goudale"));
            list.AddItem(new TextRandomizerItem("Leffe"));

            await unitOfWork.Repository<SimpleRandomizerList>().Update(list.Id, list);
            await unitOfWork.SaveChangesAsync();

            list = (await unitOfWork.Repository<SimpleRandomizerList>().Get(list.Id));
            list.Items.Count().Should().Be(3);

            (await unitOfWork.Repository<TextRandomizerItem>().GetItems(0, 10)).Count().Should().Be(3);
        }


    }
}
