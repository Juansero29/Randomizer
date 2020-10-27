using Microsoft.EntityFrameworkCore;
using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace Randomizer.Framework.Persistence.PersistenceManagers.EntityFramework
{

    /// <summary>
    /// The Entity Framework <see cref="DbContext"/> for the Randomizer app
    /// </summary>
    public class RandomizerContext : DbContext
    {
        #region Sets
        public virtual DbSet<SimpleRandomizerList> Lists { get; set; }
        public virtual DbSet<TextRandomizerItem> TextItems { get; set; }
        public virtual DbSet<ImageRandomizerItem> ImageItems { get; set; }

        #endregion

        #region Constructor

        public RandomizerContext()
        {
            // needed setup for Xamarin
            SQLitePCL.Batteries_V2.Init();
        }

        public RandomizerContext(DbContextOptions options) : base(options)
        {
            // needed setup for Xamarin
            SQLitePCL.Batteries_V2.Init();
        }

        #endregion

        #region Overrides
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Lists Table Setup

            // The name of the lists table 
            modelBuilder.Entity<RandomizerList>().ToTable("Lists");

            // Definition of primary key
            modelBuilder.Entity<RandomizerList>().HasKey(l => l.Id);

            // Definition of the automatic generation of an Id
            modelBuilder.Entity<RandomizerList>().Property(l => l.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<RandomizerList>()
                .HasDiscriminator<string>("listType")
                .HasValue<SimpleRandomizerList>("simpleList");

            #endregion

            #region Items Table Setup

            // The name of the lists table 
            modelBuilder.Entity<RandomizerItem>().ToTable("Items");


            // Definition of the primary key
            modelBuilder.Entity<RandomizerItem>().HasKey(i => i.Id);

            // Definition of the automatic generation of an Id
            modelBuilder.Entity<RandomizerItem>().Property(i => i.Id).ValueGeneratedOnAdd();

            // Adding type discriminator for text item
            modelBuilder.Entity<RandomizerItem>()
                .HasDiscriminator<string>("itemType")
                .HasValue<RandomizerItem>("baseItem")
                .HasValue<TextRandomizerItem>("textItem")
                .HasValue<ImageRandomizerItem>("imageItem");

            #endregion

            modelBuilder.Entity<RandomizerItem>().Property<int>("ListId");

            modelBuilder.Entity<RandomizerList>()
                .HasMany(l => l.Items)
                .WithOne(i => i.Parent)
                .HasForeignKey("ListId")
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                string dbPath = Path.Combine(FileSystem.AppDataDirectory, "randomizer.db");
                //optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.UseSqlite($"Filename={dbPath}");
            }
            catch(Exception)
            {
                Console.WriteLine("Couldn't create database file. Using local database.");
                optionsBuilder.UseSqlite($"DataSource=.\\randomizer.db");
            }

        }
        #endregion
    }
}
