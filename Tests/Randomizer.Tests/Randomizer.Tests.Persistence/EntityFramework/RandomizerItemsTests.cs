using FluentAssertions;
using Randomizer.Framework.Models;
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
