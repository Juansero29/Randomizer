using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Persistence.PersistenceManagers.EntityFramework;
using Randomizer.Tests.Persistence.EntityFramework;
using System;
using System.Linq;
using Xunit;

namespace Randomizer.Tests.Persistence
{
    public class StubContextTest
    {
        [Fact]
        public async void GetTest()
        {
            using (var context = new TestContext())
            using (var unitOfWork = new EFUnitOfWork(context))
            {
                context.Database.EnsureCreated();

                Assert.Equal("Ewok", (await unitOfWork.Repository<Framework.Models.SimpleRandomizerList>().Get(3)).Name);
                Assert.Null((await unitOfWork.Repository<Framework.Models.SimpleRandomizerList>().Get(4)));
            }
        }

        [Fact]
        public async void GetItemsTest()
        {
            using (var context = new TestContext())
            using (var unitOfWork = new EFUnitOfWork(context))
            {
                context.Database.EnsureCreated();

                Assert.Equal(3, (await unitOfWork.Repository<Framework.Models.Contract.RandomizerList>().GetItems(0, 10)).Count());
                Assert.Equal(2, (await unitOfWork.Repository<Framework.Models.Contract.RandomizerList>().GetItems(0, 2)).Count());
                Assert.Single((await unitOfWork.Repository<Framework.Models.Contract.RandomizerList>().GetItems(1, 2)));
            }
        }

        [Fact]
        public async void InsertTest()
        {
            using (var context = new TestContext())
            using (var unitOfWork = new EFUnitOfWork(context))
            {
                context.Database.EnsureCreated();

                Framework.Models.Contract.RandomizerList n = new Framework.Models.SimpleRandomizerList { Name = "Jarjar" };

                n = await unitOfWork.Repository<Framework.Models.Contract.RandomizerList>().Add(n);
                Assert.NotNull(n);
                await unitOfWork.SaveChangesAsync();
                Assert.Equal(4, (await unitOfWork.Repository<Framework.Models.Contract.RandomizerList>().GetItems(0, 10)).Count());
            }
        }

        [Fact]
        public async void AddRangeTest()
        {
            using (var context = new TestContext())
            using (var unitOfWork = new EFUnitOfWork(context))
            {
                context.Database.EnsureCreated();

                Framework.Models.Contract.RandomizerList n = new Framework.Models.SimpleRandomizerList { Name = "Jarjar",};
                Framework.Models.Contract.RandomizerList n2 = new Framework.Models.SimpleRandomizerList { Name = "Porg" };
                Framework.Models.Contract.RandomizerList n3 = new Framework.Models.SimpleRandomizerList { Name = "Chucky la poupée de sang" };

                var result = await unitOfWork.Repository<Framework.Models.Contract.RandomizerList>().AddRange(n, n2, n3);
                Assert.True(result);
                await unitOfWork.SaveChangesAsync();
                Assert.Equal(6, (await unitOfWork.Repository<Framework.Models.Contract.RandomizerList>().GetItems(0, 10)).Count());

            }
        }

        [Fact]
        public async void UpdateTest()
        {
            using (var context = new TestContext())
            using (var unitOfWork = new EFUnitOfWork(context))
            {
                context.Database.EnsureCreated();

                var chewie = await unitOfWork.Repository<Framework.Models.Contract.RandomizerList>().Get(1);
                chewie.Name = "Chewie";
                await unitOfWork.Repository<Framework.Models.Contract.RandomizerList>().Update(chewie);
                await unitOfWork.SaveChangesAsync();
                Assert.Equal("Chewie", (await unitOfWork.Repository<Framework.Models.Contract.RandomizerList>().Get(1)).Name);
                Assert.Empty((await unitOfWork.Repository<Framework.Models.Contract.RandomizerList>().GetItems(0, 10)).Where(n => n.Name.Equals("Chewbacca")));
            }
        }

        [Fact]
        public async void Delete()
        {
            using (var context = new TestContext())
            using (var unitOfWork = new EFUnitOfWork(context))
            {
                context.Database.EnsureCreated();

                var chewie = await unitOfWork.Repository<Framework.Models.Contract.RandomizerList>().Get(1);
                await unitOfWork.Repository<Framework.Models.Contract.RandomizerList>().Remove(chewie);
                await unitOfWork.SaveChangesAsync();
                Assert.Empty((await unitOfWork.Repository<Framework.Models.Contract.RandomizerList>().GetItems(0, 10)).Where(n => n.Name.Equals("Chewbacca")));
            }
        }

        [Fact]
        public async void RejectChangesTest()
        {
            using (var context = new TestContext())
            using (var unitOfWork = new EFUnitOfWork(context))
            {
                context.Database.EnsureCreated();

                await unitOfWork.Repository<Framework.Models.Contract.RandomizerList>().Remove(1);
                await unitOfWork.CancelChangesAsync();
                await unitOfWork.SaveChangesAsync();
                Assert.Equal(3, (await unitOfWork.Repository<Framework.Models.Contract.RandomizerList>().GetItems(0, 10)).Count());
            }
        }
    }
}
