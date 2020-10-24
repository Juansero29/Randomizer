using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Persistence.PersistenceManagers.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Randomizer.Tests.Persistence.EntityFramework
{
    public class TestContext : RandomizerContext
    {

        public TestContext() : base()
        {
        }

        public TestContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //var connection = new SqliteConnection("DataSource=D:\\dev\\randomizer.db");
            //optionsBuilder.UseLazyLoadingProxies().UseSqlite(connection);
            // optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=randomizer;Trusted_Connection=True;");
            //var connection = new SqliteConnection("DataSource=:memory:");
            //connection.Open();
            //optionsBuilder.UseSqlite(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SimpleRandomizerList>().HasData(
                new SimpleRandomizerList { Id = 1, Name = "Beers" },
                new SimpleRandomizerList { Id = 2, Name = "Albums" },
                new SimpleRandomizerList { Id = 3, Name = "People" });
        }
    }
}
