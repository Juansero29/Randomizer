using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Randomizer.Framework.Persistence.PersistenceManagers.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Randomizer.Tests.Persistence.EntityFramework
{
    public class TestContextFactory : IDisposable
    {
        private DbConnection _connection;

        private DbContextOptions<TestContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<TestContext>()
                .UseSqlite(_connection).Options;
        }

        public RandomizerContext CreateContext()
        {
            if (_connection == null)
            {
                _connection = new SqliteConnection("DataSource=:memory:");
                _connection.Open();

                var options = CreateOptions();
                using (var context = new TestContext(options))
                {
                    context.Database.EnsureCreated();
                }
            }

            return new TestContext(CreateOptions());
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
