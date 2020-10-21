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

                Assert.Equal("Ewok", (await unitOfWork.Repository<RandomizerList>().Get(3)).Name);
                Assert.Null((await unitOfWork.Repository<RandomizerList>().Get(4)));
            }
        }

        [Fact]
        public async void GetItemsTest()
        {
            using (var context = new TestContext())
            using (var unitOfWork = new EFUnitOfWork(context))
            {
                context.Database.EnsureCreated();

                Assert.Equal(3, (await unitOfWork.Repository<IRandomizerList>().GetItems(0, 10)).Count());
                Assert.Equal(2, (await unitOfWork.Repository<IRandomizerList>().GetItems(0, 2)).Count());
                Assert.Single((await unitOfWork.Repository<IRandomizerList>().GetItems(1, 2)));
            }
        }

        [Fact]
        public async void InsertTest()
        {
            using (var context = new TestContext())
            using (var unitOfWork = new EFUnitOfWork(context))
            {
                context.Database.EnsureCreated();

                IRandomizerList n = new RandomizerList { Name = "Jarjar" };

                n = await unitOfWork.Repository<IRandomizerList>().Add(n);
                Assert.NotNull(n);
                await unitOfWork.SaveChangesAsync();
                Assert.Equal(4, (await unitOfWork.Repository<IRandomizerList>().GetItems(0, 10)).Count());
            }
        }

        [Fact]
        public async void AddRangeTest()
        {
            using (var context = new TestContext())
            using (var unitOfWork = new EFUnitOfWork(context))
            {
                context.Database.EnsureCreated();

                IRandomizerList n = new RandomizerList { Name = "Jarjar",};
                IRandomizerList n2 = new RandomizerList { Name = "Porg" };
                IRandomizerList n3 = new RandomizerList { Name = "Chucky la poupée de sang" };

                var result = await unitOfWork.Repository<IRandomizerList>().AddRange(n, n2, n3);
                Assert.True(result);
                await unitOfWork.SaveChangesAsync();
                Assert.Equal(6, (await unitOfWork.Repository<IRandomizerList>().GetItems(0, 10)).Count());

            }
        }

        [Fact]
        public async void UpdateTest()
        {
            using (var context = new TestContext())
            using (var unitOfWork = new EFUnitOfWork(context))
            {
                context.Database.EnsureCreated();

                var chewie = await unitOfWork.Repository<IRandomizerList>().Get(1);
                chewie.Name = "Chewie";
                await unitOfWork.Repository<IRandomizerList>().Update(chewie);
                await unitOfWork.SaveChangesAsync();
                Assert.Equal("Chewie", (await unitOfWork.Repository<IRandomizerList>().Get(1)).Name);
                Assert.Empty((await unitOfWork.Repository<IRandomizerList>().GetItems(0, 10)).Where(n => n.Name.Equals("Chewbacca")));
            }
        }

        [Fact]
        public async void Delete()
        {
            using (var context = new TestContext())
            using (var unitOfWork = new EFUnitOfWork(context))
            {
                context.Database.EnsureCreated();

                var chewie = await unitOfWork.Repository<IRandomizerList>().Get(1);
                await unitOfWork.Repository<IRandomizerList>().Remove(chewie);
                await unitOfWork.SaveChangesAsync();
                Assert.Empty((await unitOfWork.Repository<IRandomizerList>().GetItems(0, 10)).Where(n => n.Name.Equals("Chewbacca")));
            }
        }

        [Fact]
        public async void RejectChangesTest()
        {
            using (var context = new TestContext())
            using (var unitOfWork = new EFUnitOfWork(context))
            {
                context.Database.EnsureCreated();

                await unitOfWork.Repository<IRandomizerList>().Remove(1);
                await unitOfWork.CancelChangesAsync();
                await unitOfWork.SaveChangesAsync();
                Assert.Equal(3, (await unitOfWork.Repository<IRandomizerList>().GetItems(0, 10)).Count());
            }
        }
    }
}
