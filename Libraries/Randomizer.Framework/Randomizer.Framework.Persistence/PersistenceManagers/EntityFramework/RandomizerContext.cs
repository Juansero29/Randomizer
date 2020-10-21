﻿using Microsoft.EntityFrameworkCore;
using Randomizer.Framework.Models;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Persistence.PersistenceManagers.EntityFramework.ModelEFLink;
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
        //public DbSet<RandomizerListEntity> Lists { get; set; }

        //public DbSet<RandomizerItemEntity> Items { get; set; }
        
        #endregion

        #region Constructor

        public RandomizerContext()
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
            modelBuilder.Entity<RandomizerListEntity>().ToTable("Lists");

            // Definition of primary key
            modelBuilder.Entity<RandomizerListEntity>().HasKey(l => l.Id);

            // Definition of the automatic generation of an Id
            modelBuilder.Entity<RandomizerListEntity>().Property(l => l.Id).ValueGeneratedOnAdd();

            #endregion


            #region Items Table Setup
            // the name of the items table
            modelBuilder.Entity<RandomizerItemEntity>().ToTable("Items");

            // Definition of the primary key
            modelBuilder.Entity<RandomizerItemEntity>().HasKey(i => i.Id);

            // Definition of the automatic generation of an Id
            modelBuilder.Entity<RandomizerItemEntity>().Property(i => i.Id).ValueGeneratedOnAdd();

            // Adding type discriminators
            modelBuilder.Entity<RandomizerItemEntity>().HasDiscriminator<string>("itemType").HasValue<RandomizerItemEntity>("baseItem").HasValue<TextRandomizerItemEntity>("textItem").HasValue<ImageRandomizerItemEntity>("imageItem");
            #endregion

            modelBuilder.Entity<RandomizerListEntity>().HasMany(l => l.Items).WithOne();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "randomizer.db");

            optionsBuilder.UseSqlite($"Filename={dbPath}");
        } 
        #endregion
    }
}
