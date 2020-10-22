using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Persistence.PersistenceManagers.EntityFramework;
using Randomizer.Tests.Persistence.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Randomizer.Tests.Persistence.EntityFramework
{
    public class RandomizerItemsTests
    {

        [Fact]
        public async void AddItemToNewList()
        {
            using var context = new TestContext();
            using var unitOfWork = new EFUnitOfWork(context);

            context.Database.EnsureCreated();

            RandomizerList l = new SimpleRandomizerList { Name = "More Beers" };

            l = await unitOfWork.Repository<RandomizerList>().Add(l);
            l.Should().NotBeNull();
            await unitOfWork.SaveChangesAsync();


            (await unitOfWork.Repository<RandomizerList>().GetItems(0, 10)).Count().Should().Be(4);


            var moreBeers = (await unitOfWork.Repository<RandomizerList>().Get(l.Id));

            var chouffe = new TextRandomizerItem("Chouffe", moreBeers);
            var goudale = new TextRandomizerItem("Goudale", moreBeers);
            var leffe = new TextRandomizerItem("Leffe", moreBeers);

            moreBeers.AddItem(chouffe);
            moreBeers.AddItem(goudale);
            moreBeers.AddItem(leffe);

            moreBeers.Items.Count().Should().Be(3);

            await unitOfWork.Repository<RandomizerItem>().AddRange(chouffe, goudale, leffe);
            await unitOfWork.Repository<RandomizerList>().Update(moreBeers.Id, moreBeers);

            await unitOfWork.SaveChangesAsync();


            // WHY DOESN'T THIS SHIT LOAD THE ITEMS IN EACH LIST. I'M FUCKING DONE WITH FUCKING ENTITY FRAMEWORK CORE.
            var x = context.Lists.Include(l => l.Items).ToList();

            (await unitOfWork.Repository<RandomizerItem>().GetItems(0, 10)).Count().Should().Be(3);

            foreach (var list in context.Lists)
            {
                if(list.Items.Count > 0)
                {
                    ;
                }
            }
            moreBeers = context.Lists.Include(list => list.Items).ToList().FirstOrDefault(l => l.Id == moreBeers.Id);
            // moreBeers = unitOfWork.Repository<RandomizerList>().Set.Where(l => l.Id == moreBeers.Id).Include("Items").First();
            // moreBeers = (await unitOfWork.Repository<RandomizerList>().Get(moreBeers.Id));
            
            moreBeers.Items.Count().Should().Be(3);

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
