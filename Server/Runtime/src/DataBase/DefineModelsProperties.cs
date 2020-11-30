using BDS.Framework;
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
                .HasColumnType("varchar(45)")
                .HasConversion(ValueTypeConverter.EnumConvertString<WorkPipelineStatus>());
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
            #endregion

            #region Table constraint
            modelBuilder.Entity<DockPipeline>().HasKey(e => e.Name);
            #endregion
        }
        public static void PipelineConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PipelineConfig>()
                .Property(e => e.Name)
                .HasColumnType("varchar(45)");
            modelBuilder.Entity<PipelineConfig>()
                .Property(e => e.Status)
                .HasColumnType("varchar(45)")
                .HasConversion(ValueTypeConverter.EnumConvertString<PipelineConfigStatus>());
            modelBuilder.Entity<PipelineConfig>()
                .Property(e => e.Type)
                .HasColumnType("varchar(45)")
                .HasConversion(ValueTypeConverter.EnumConvertString<PipelineReferenceType>());
            modelBuilder.Entity<PipelineConfig>()
                .Property(e => e.AddressType)
                .HasColumnType("varchar(45)")
                .HasConversion(ValueTypeConverter.EnumConvertString<PipelineReferenceAddressType>());
            modelBuilder.Entity<PipelineConfig>()
                .Property(e => e.PipelineReferenceAddress)
                .HasColumnType("varchar(100)");
            modelBuilder.Entity<PipelineConfig>()
                .Property(e => e.ApartTimeType)
                .HasColumnType("varchar(45)")
                .HasConversion(ValueTypeConverter.EnumConvertString<PipelineScheduleApartTimeType>());
            modelBuilder.Entity<PipelineConfig>()
                .Property(e => e.ApartTime)
                .HasColumnType("int");

            modelBuilder.Entity<PipelineConfig>().HasKey(e => e.Name);
        }
        //public static void PipelineAssemblyConfig(ModelBuilder modelBuilder)
        //{
        //    #region Column type
        //    modelBuilder.Entity<PipelineAssemblyConfig>()
        //        .Property(e => e.AssemblyKey)
        //        .HasColumnType("varchar(200)")
        //        .HasMaxLength(200);
        //    modelBuilder.Entity<PipelineAssemblyConfig>()
        //        .Property(e => e.AssemblyPath)
        //        .HasColumnType("varchar(1000)")
        //        .HasMaxLength(1000);
        //    modelBuilder.Entity<PipelineAssemblyConfig>()
        //        .Property(e => e.AssemblyStatus)
        //        .HasColumnType("varchar(45)")
        //        .HasMaxLength(45);
        //    modelBuilder.Entity<PipelineAssemblyConfig>()
        //        .Property(e => e.ScheduleTime)
        //        .HasColumnType("varchar(45)")
        //        .HasMaxLength(45);
        //    #endregion

        //    #region Table constraint
        //    modelBuilder.Entity<PipelineAssemblyConfig>().HasKey(c => c.AssemblyKey);
        //    modelBuilder.Entity<PipelineAssemblyConfig>().HasIndex(e => e.AssemblyKey);
        //    #endregion
        //}
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
