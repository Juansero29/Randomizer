using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Persistence.PersistenceManagers.EntityFramework;
using Randomizer.Tests.Persistence.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace Randomizer.Tests.Persistence.EntityFramework
{
    public class RandomizerItemsTests
    {


        [Fact]
        public async void AddItemsToNewListWithoutUnitOfWork()
        {
            using (var factory = new TestContextFactory())
            {

                await using (var context = factory.CreateContext())
                {
                    // ensure is a new database
                    await context.Database.EnsureDeletedAsync();

                    // ensure the database has been created
                    await context.Database.EnsureCreatedAsync();
                }

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

                await using (var context = factory.CreateContext())
                {


                    // adding the list to the RandomizerList set
                    l = context.Set<RandomizerList>().Add(l).Entity;
                    l.Should().NotBeNull();

                    // the three items should still be there
                    l.Items.Count.Should().Be(3);

                    // saving changes
                    await context.SaveChangesAsync();
                }

                await using (var context = factory.CreateContext())
                {


                    // the number of total lists in DB should now be three
                    (context.Set<RandomizerList>().Take(10)).Count().Should().Be(4);

                    // recovering the list in four different ways
                    var moreBeers = context.Set<RandomizerList>().Find(l.Id);
                    var moreBeers2 = context.Set<SimpleRandomizerList>().Include(l => l.Items).Where(i => i.Id == l.Id).FirstOrDefault();
                    var moreBeers3 = context.Set<RandomizerList>().Include(l => l.Items).ToList().Where(l => l.Id == moreBeers.Id).FirstOrDefault();
                    var moreBeers4 = context.Lists.Include(list => list.Items).ToList().FirstOrDefault(l => l.Id == moreBeers.Id);


                    // should return the same list with three items
                    moreBeers.Items.Count().Should().Be(3);
                    moreBeers2.Items.Count().Should().Be(3);
                    moreBeers3.Items.Count().Should().Be(3);
                    moreBeers4.Items.Count().Should().Be(3);
                }
            }

        }


        [Fact]
        public async void AddItemsToNewListWithUnitOfWork()
        {
            using var factory = new TestContextFactory();
            var context = factory.CreateContext();
            var unitOfWork = new EFUnitOfWork(context);

            // ensure is a new database
            await context.Database.EnsureDeletedAsync();

            // ensure the database has been created
            await context.Database.EnsureCreatedAsync();

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

            // item count should still be three
            l.Items.Count.Should().Be(3);

            // saving changes
            await unitOfWork.SaveChangesAsync();

            // total lists saved should now be 4
            (await unitOfWork.Repository<RandomizerList>().GetItems(0, 10)).Count().Should().Be(4);

            // recovering the list in three different ways
            var moreBeers = (await unitOfWork.Repository<RandomizerList>().Get(l.Id));
            var moreBeers2 = (await unitOfWork.Repository<RandomizerList>().Set.Include(l => l.Items).Where(i => i.Id == l.Id).FirstOrDefaultAsync());
            var moreBeers3 = await unitOfWork.Repository<SimpleRandomizerList>().Set.Include(l => l.Items).Where(i => i.Id == l.Id).FirstOrDefaultAsync();

            // should return the same list with three items
            moreBeers.Items.Count().Should().Be(3);
            moreBeers2.Items.Count().Should().Be(3);
            moreBeers3.Items.Count().Should().Be(3);
        }


        [Fact]
        public async void AddItemToPreExistantList()
        {
            using var factory = new TestContextFactory();
            using var context = factory.CreateContext();
            using var unitOfWork = new EFUnitOfWork(context);

            // ensure is a new database
            await context.Database.EnsureDeletedAsync();

            // ensure the database has been created
            await context.Database.EnsureCreatedAsync();

            RandomizerList list = (await unitOfWork.Repository<SimpleRandomizerList>().Get(1));
            list.AddItem(new TextRandomizerItem("Chouffe"));
            list.AddItem(new TextRandomizerItem("Goudale"));
            list.AddItem(new TextRandomizerItem("Leffe"));

            await unitOfWork.Repository<RandomizerList>().Update(list.Id, list);
            await unitOfWork.SaveChangesAsync();

            list = (await unitOfWork.Repository<RandomizerList>().Get(list.Id));
            list.Items.Count().Should().Be(0);

            list = (await unitOfWork.Repository<RandomizerList>().Set.Include(l => l.Items).Where(i => i.Id == list.Id).FirstOrDefaultAsync());
            list.Items.Count().Should().Be(3);

            (await unitOfWork.Repository<RandomizerItem>().GetItems(0, 10)).Count().Should().Be(3);
        }

    }
}
