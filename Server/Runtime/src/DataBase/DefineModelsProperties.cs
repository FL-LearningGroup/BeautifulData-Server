using BDS.Runtime.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Runtime.DataBase
{
    /// <summary>
    /// Define model attributes instead of passing attributes
    /// </summary>
    internal class DefineModelsProperties
    {
        public static void DockPipeline(ModelBuilder modelBuilder)
        {
            #region Column type
            modelBuilder.Entity<DockPipeline>()
                .Property(e => e.Name)
                .HasColumnType("varchar(200)")
                .HasMaxLength(200);
            modelBuilder.Entity<DockPipeline>()
                .Property(e => e.Status)
                .HasColumnType("varchar(45)");
            modelBuilder.Entity<DockPipeline>()
                .Property(e => e.InvokeStatus)
                .HasColumnType("varchar(45)");
            modelBuilder.Entity<DockPipeline>()
                .Property(e => e.InvokeStatus)
                .HasColumnType("varchar(45)");
            modelBuilder.Entity<DockPipeline>()
                .Property(e => e.LoadPipelineDT)
                .HasColumnType("varchar(19)")
                .HasConversion(ValueTypeConverter.DateTimeConvertString);
            modelBuilder.Entity<DockPipeline>()
                .Property(e => e.UnloadPipelineDT)
                .HasColumnType("varchar(19)")
                .HasConversion(ValueTypeConverter.DateTimeConvertString);
            modelBuilder.Entity<DockPipeline>()
                .Property(e => e.ExecuteStartDT)
                .HasColumnType("varchar(19)")
                .HasConversion(ValueTypeConverter.DateTimeConvertString);
            modelBuilder.Entity<DockPipeline>()
                .Property(e => e.ExecuteEndDT)
                .HasColumnType("varchar(19)")
                .HasConversion(ValueTypeConverter.DateTimeConvertString);
            modelBuilder.Entity<DockPipeline>()
                .Property(e => e.ExecutionMessage)
                .HasColumnType("text")
                .HasConversion(ValueTypeConverter.StringBuilderConvertString);
            modelBuilder.Entity<DockPipeline>()
                .Property(e => e.LastExecuteDT)
                .HasColumnType("varchar(19)")
                .HasConversion(ValueTypeConverter.DateTimeConvertString);
            modelBuilder.Entity<DockPipeline>()
                .Property(e => e.NextExecuteDT)
                .HasColumnType("varchar(19)")
                .HasConversion(ValueTypeConverter.DateTimeConvertString);
            #endregion

            #region Table constraint
            modelBuilder.Entity<DockPipeline>().HasKey(e => e.Name);
            modelBuilder.Entity<DockPipeline>().HasIndex(e => e.LastExecuteDT);
            #endregion
        }
        public static void PipelineAssemblyConfig(ModelBuilder modelBuilder)
        {
            #region Column type
            modelBuilder.Entity<PipelineAssemblyConfig>()
                .Property(e => e.AssemblyKey)
                .HasColumnType("varchar(200)")
                .HasMaxLength(200);
            modelBuilder.Entity<PipelineAssemblyConfig>()
                .Property(e => e.AssemblyPath)
                .HasColumnType("varchar(1000)")
                .HasMaxLength(1000);
            modelBuilder.Entity<PipelineAssemblyConfig>()
                .Property(e => e.AssemblyStatus)
                .HasColumnType("varchar(45)")
                .HasMaxLength(45);
            modelBuilder.Entity<PipelineAssemblyConfig>()
                .Property(e => e.ScheduleTime)
                .HasColumnType("varchar(45)")
                .HasMaxLength(45);
            #endregion

            #region Table constraint
            modelBuilder.Entity<PipelineAssemblyConfig>().HasKey(c => c.AssemblyKey);
            modelBuilder.Entity<PipelineAssemblyConfig>().HasIndex(e => e.AssemblyKey);
            #endregion
        }
        public static void DockPipelineHistory(ModelBuilder modelBuilder)
        {
            #region Column type
            modelBuilder.Entity<DockPipelineHistory>()
                .Property(e => e.Name)
                .HasColumnType("varchar(200)")
                .HasMaxLength(200);
            modelBuilder.Entity<DockPipelineHistory>()
                .Property(e => e.Status)
                .HasColumnType("varchar(45)");
            modelBuilder.Entity<DockPipelineHistory>()
                .Property(e => e.InvokeStatus)
                .HasColumnType("varchar(45)");
            modelBuilder.Entity<DockPipelineHistory>()
                .Property(e => e.InvokeStatus)
                .HasColumnType("varchar(45)");
            modelBuilder.Entity<DockPipelineHistory>()
                .Property(e => e.LoadPipelineDT)
                .HasColumnType("varchar(19)")
                .HasConversion(ValueTypeConverter.DateTimeConvertString);
            modelBuilder.Entity<DockPipeline>()
                .Property(e => e.UnloadPipelineDT)
                .HasColumnType("varchar(19)")
                .HasConversion(ValueTypeConverter.DateTimeConvertString);
            modelBuilder.Entity<DockPipelineHistory>()
                .Property(e => e.ExecuteStartDT)
                .HasColumnType("varchar(19)")
                .HasConversion(ValueTypeConverter.DateTimeConvertString);
            modelBuilder.Entity<DockPipelineHistory>()
                .Property(e => e.ExecuteEndDT)
                .HasColumnType("varchar(19)")
                .HasConversion(ValueTypeConverter.DateTimeConvertString);
            modelBuilder.Entity<DockPipelineHistory>()
                .Property(e => e.ExecutionMessage)
                .HasColumnType("text")
                .HasConversion(ValueTypeConverter.StringBuilderConvertString);
            modelBuilder.Entity<DockPipelineHistory>()
                .Property(e => e.LastExecuteDT)
                .HasColumnType("varchar(19)")
                .HasConversion(ValueTypeConverter.DateTimeConvertString);
            modelBuilder.Entity<DockPipelineHistory>()
                .Property(e => e.NextExecuteDT)
                .HasColumnType("varchar(19)")
                .HasConversion(ValueTypeConverter.DateTimeConvertString);
            modelBuilder.Entity<DockPipelineHistory>()
                .Property(e => e.InsertRowDT)
                .HasColumnType("datetime")
                .HasDefaultValueSql("NOW()");
            #endregion

            #region Table constraint
            modelBuilder.Entity<DockPipelineHistory>().HasKey(c => new {c.Name, c.Status, c.NextExecuteDT });
            modelBuilder.Entity<DockPipelineHistory>(e => e.HasIndex(e => e.NextExecuteDT));
            #endregion
        }
    }
}
