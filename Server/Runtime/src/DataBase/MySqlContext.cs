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
        public DbSet<DockPipeline> DockPipeline { get; set; }
        public DbSet<PipelineAssemblyConfig> PipelineAssemblyConfig { get; set; }
        public DbSet<DockPipelineHistory> DockPipelineHistory { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DefineModelsProperties.DockPipelineHistory(modelBuilder);
            DefineModelsProperties.DockPipeline(modelBuilder);
            DefineModelsProperties.PipelineAssemblyConfig(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(String.Format("server={0};database={1};user={2};password={3}", MySqlConfiguration.Host, MySqlConfiguration.DB, MySqlConfiguration.User, MySqlConfiguration.Passwd));
        }
    }
}
