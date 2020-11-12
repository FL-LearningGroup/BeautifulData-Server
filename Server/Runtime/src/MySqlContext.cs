using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BDS.Runtime.Models;
using BDS.Framework;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Org.BouncyCastle.Asn1;

namespace BDS.Runtime
{
    internal class MySqlContext : DbContext
    {
        public DbSet<DockPipeline> DockPipelines { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DockPipeline>()
                .Property(e => e.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (WorkPipelineStatus)Enum.Parse(typeof(WorkPipelineStatus), v)
                );
            modelBuilder.Entity<DockPipeline>()
                .Property(e => e.InvokeStatus)
                .HasConversion(
                    v => v.ToString(),
                    v => (PipelineInvokeStatus)Enum.Parse(typeof(PipelineInvokeStatus), v)
                );
            // Convert datetime to string
            modelBuilder.Entity<DockPipeline>()
                .Property(e => e.LastExecuteTime)
                .HasConversion(
                    v => v.ToString(GlobalConstant.ConvertDateTimeToStringFormat),
                    v => DateTime.Parse(v)
                ) ;
            modelBuilder.Entity<DockPipeline>()
                .Property(e => e.NextExecuteTime)
                .HasConversion(
                    v => v.ToString(GlobalConstant.ConvertDateTimeToStringFormat),
                    v => DateTime.Parse(v)
                );
            modelBuilder.Entity<DockPipeline>()
                .Property(e => e.ExecuteStartTime)
                .HasConversion(
                    v => v.ToString(GlobalConstant.ConvertDateTimeToStringFormat),
                    v => DateTime.Parse(v)
                );
            modelBuilder.Entity<DockPipeline>()
                .Property(e => e.ExecuteEndTime)
                .HasConversion(
                    v => v.ToString(GlobalConstant.ConvertDateTimeToStringFormat),
                    v => DateTime.Parse(v)
                );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(String.Format("server={0};database={1};user={2};password={3}", MySqlConfiguration.Host, MySqlConfiguration.DB, MySqlConfiguration.User, MySqlConfiguration.Passwd));
        }
    }
}
