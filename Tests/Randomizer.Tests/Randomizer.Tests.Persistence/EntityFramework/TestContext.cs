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
    class TestContext : RandomizerContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            optionsBuilder.UseSqlite(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RandomizerList>().HasData(
                new RandomizerList { Id = 1, Name = "Chewbacca" },
                new RandomizerList { Id = 2, Name = "Yoda" },
                new RandomizerList { Id = 3, Name = "Ewok" });
        }
    }
}
