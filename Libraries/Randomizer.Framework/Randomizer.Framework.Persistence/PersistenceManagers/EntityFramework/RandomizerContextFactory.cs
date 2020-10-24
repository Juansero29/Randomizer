//using Microsoft.Data.Sqlite;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Data.Common;
//using System.Text;

//namespace Randomizer.Framework.Persistence.PersistenceManagers.EntityFramework
//{
//    public class RandomizerContextFactory : IDisposable
//    {
//        private DbConnection _connection;

//        private DbContextOptions<RandomizerContext> CreateOptions()
//        {
//            return new DbContextOptionsBuilder<RandomizerContext>()
//                .UseSqlite(_connection).Options;
//        }

//        public RandomizerContext CreateContext() 
//        {
//            if (_connection == null)
//            {
//                _connection = new SqliteConnection("DataSource=:memory:");
//                _connection.Open();

//                var options = CreateOptions();
//                using (var context = new RandomizerContext(options))
//                {
//                    context.Database.EnsureCreated();
//                }
//            }

//            return new RandomizerContext(CreateOptions());
//        }

//        public void Dispose()
//        {
//            if (_connection != null)
//            {
//                _connection.Dispose();
//                _connection = null;
//            }
//        }
//    }
//}
