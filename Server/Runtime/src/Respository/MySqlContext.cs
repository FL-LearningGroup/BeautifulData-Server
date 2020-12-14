using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BDS.Runtime.Models;
using BDS.Framework;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Org.BouncyCastle.Asn1;
using BDS.Runtime.Respository.Models;

namespace BDS.Runtime.Respository
{
    /// <summary>
    /// MySql context inherit DbContext
    /// </summary>
    internal class MySqlContext : DbContext
    {
        public DbSet<DockPipeline> DockPipeline { get; set; }
        // public DbSet<PipelineAssemblyConfig> PipelineAssemblyConfig { get; set; }
        public DbSet<DockPipelineHistory> DockPipelineHistory { get; set; }
        public DbSet<PipelineConfig> PipelineConfig { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DefineModelsProperties.DockPipelineHistory(modelBuilder);
            DefineModelsProperties.DockPipeline(modelBuilder);
            DefineModelsProperties.PipelineConfig(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(String.Format("server={0};database={1};user={2};password={3}", MySqlConfiguration.Host, MySqlConfiguration.DB, MySqlConfiguration.User, MySqlConfiguration.Passwd));
        }
    }
}
