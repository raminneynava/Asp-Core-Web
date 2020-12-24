using Common.Utilities;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
   public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {

        }
        public DbSet<BlogCategory> BlogCategories { set; get; }
        public DbSet<Blog> Blogs { set; get; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    var EntitiesAssembly = typeof(IEntity).Assembly;
        //    modelBuilder.RegisterAllEntities<IEntity>(EntitiesAssembly);
        //    modelBuilder.RegisterEntityTypeConfiguration(EntitiesAssembly);

        //}
    }
}
