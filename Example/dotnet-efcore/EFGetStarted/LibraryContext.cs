using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFGetStarted
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Book { get; set; }

        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<PipelineAssemblyConfig> PipelineAssembliesConfig { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=172.29.167.165;database=bdstest;user=bds;password=Ya0di+2019");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Publisher>(entity =>
        //    {
        //        entity.HasKey(e => e.ID);
        //        entity.Property(e => e.Name).IsRequired();
        //    });

        //    modelBuilder.Entity<Book>(entity =>
        //    {
        //        entity.HasKey(e => e.ISBN);
        //        entity.Property(e => e.Title).IsRequired();
        //        entity.HasOne(d => d.Publisher)
        //          .WithMany(p => p.Books);
        //    });
        //}
    }
}
