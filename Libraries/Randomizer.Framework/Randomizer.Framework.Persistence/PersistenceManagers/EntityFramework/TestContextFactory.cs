using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Randomizer.Framework.Persistence.PersistenceManagers.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace Randomizer.Tests.Persistence.EntityFramework
{
    public class RandomizerContextFactory : IDisposable
    {
        private DbConnection _connection;

        private DbContextOptions<RandomizerContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<RandomizerContext>()
                .UseSqlite(_connection).Options;
        }

        public RandomizerContext CreateContext()
        {
            if (_connection == null)
            {
                try
                {
                    string dbPath = Path.Combine(FileSystem.AppDataDirectory, "randomizer.db");
                    _connection = new SqliteConnection($"Filename={dbPath}");

                }
                catch (Exception)
                {
                    Console.WriteLine("Couldn't create database file. Using local database.");
                    _connection = new SqliteConnection($"DataSource=.\\randomizer.db");
                }

                _connection.Open();

                var options = CreateOptions();
                using (var context = new RandomizerContext(options))
                {
                    context.Database.EnsureCreated();
                }
            }

            return new RandomizerContext(CreateOptions());
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
