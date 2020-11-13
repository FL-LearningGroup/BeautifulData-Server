using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BDS.Runtime.Models;
using BDS.Framework;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Org.BouncyCastle.Asn1;

namespace BDS.Runtime.DataBase
{
    /// <summary>
    /// MySql context inherit DbContext
    /// </summary>
    internal class MySqlContext : DbContext
    {
        public DbSet<DockPipeline> DockPipelines { get; set; }
        public DbSet<DockPipelineHistory> DockPipelineHistories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Configuration DockPipeline Model 
            //modelBuilder.Entity<DockPipeline>()
            //    .Property(e => e.Status)
            //    .HasConversion(ValueTypeConverter.WorkPipelineStatusConvertString);
            //modelBuilder.Entity<DockPipeline>()
            //    .Property(e => e.InvokeStatus)
            //    .HasConversion(ValueTypeConverter.PipelineInvokeStatusConvertString);
            // Convert datetime to string
            //modelBuilder.Entity<DockPipeline>()
            //    .Property(e => e.LastExecuteTime)
            //    .HasConversion(ValueTypeConverter.DateTimeConvertString)
            //    .HasMaxLength(GlobalConstant.ConvertDateTimeToStringFormat.Length);
            //modelBuilder.Entity<DockPipeline>()
            //    .Property(e => e.NextExecuteTime)
            //    .HasConversion(ValueTypeConverter.DateTimeConvertString)
            //    .HasMaxLength(GlobalConstant.ConvertDateTimeToStringFormat.Length)
            //    ;
            //modelBuilder.Entity<DockPipeline>()
            //    .Property(e => e.ExecuteStartTime)
            //    .HasConversion(ValueTypeConverter.DateTimeConvertString)
            //    .HasMaxLength(GlobalConstant.ConvertDateTimeToStringFormat.Length)
            //    ;
            //modelBuilder.Entity<DockPipeline>()
            //    .Property(e => e.ExecuteEndTime)
            //    .HasConversion(ValueTypeConverter.DateTimeConvertString);
            modelBuilder.Entity<DockPipeline>()
                .Property(e => e.ExecutionMessage)
                .HasConversion(ValueTypeConverter.StringBuilderConvertString);
            modelBuilder.Entity<DockPipelineHistory>()
                .Property(e => e.ExecutionMessage)
                .HasConversion(ValueTypeConverter.StringBuilderConvertString);
            #endregion
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(String.Format("server={0};database={1};user={2};password={3}", MySqlConfiguration.Host, MySqlConfiguration.DB, MySqlConfiguration.User, MySqlConfiguration.Passwd));
        }
    }
}
