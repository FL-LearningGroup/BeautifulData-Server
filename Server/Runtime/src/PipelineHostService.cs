using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BDS.Runtime.Models;
using BDS.Runtime.Respository;
using System.IO;
using System.Linq;
using log4net.Util;
using BDS.Framework;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Xml;
using BDS.Runtime.Respository.Models;

namespace BDS.Runtime
{
    /// <summary>
    /// Function:
    /// 1. Monitor assembly confg.
    /// 2. New pipeline
    /// </summary>
    internal class PipelineHostService : BackgroundService, IPielineHostService
    {
        private ServerConfig _serverConfig;
        private ServerConfigSwitch _configSwitch;
        private List<PipelineConfig> _pipelineConfigList;
        private List<PipelineControl> _pipelineControlList;
        public PipelineHostService()
        {
            _pipelineConfigList = new List<PipelineConfig>();
            _pipelineControlList = new List<PipelineControl>();
            _configSwitch = new ServerConfigSwitch();
        }

        private void InitialServer()
        {
            GlobalVariables.ServerConfig.Switch.ServerSwitchEvent += GetPipelineConfigs;
            ServerConfigOperation.SubscriptionServerConfigWatcher();
            CheckOrCreateDBSchema();
            GenerateTestData();
        }

        private void CheckOrCreateDBSchema()
        {
            using (MySqlContext context = new MySqlContext())
            {
                try
                {
                    bool isDataBase = context.Database.EnsureCreated();
                    if (isDataBase)
                    {
                        Logger.Info("Created database schema.");
                    }
                    else
                    {
                        Logger.Info("The database schema already existed.");
                    }
                }
                catch(Exception ex)
                {
                    Logger.Fatal(String.Format("Ensure created database failed. exception message: {0}", ex.Message));
                }
            }
        }

        public void GetPipelineConfigs(object source, EventArgs eventArgs)
        {
            using(MySqlContext context = new MySqlContext())
            {
                try
                {
                    _pipelineConfigList =  context.PipelineConfig.ToList();
                }
                catch(Exception ex)
                {
                    Logger.Error(String.Format("Cannot get all pipeline config from databases, exception message: {0}", ex.Message + ex.InnerException));
                }
            }
            CreatePipeline();
        }

        private void CreatePipeline()
        {
            if (_pipelineConfigList.Count == 0) return;
            PipelineConfigOperation.AddPipeline(_pipelineConfigList.FindAll(item => item.Status == PipelineConfigStatus.Add), _pipelineControlList);
            PipelineConfigOperation.StopPipeline(_pipelineConfigList.FindAll(item => item.Status == PipelineConfigStatus.Stop), _pipelineControlList);
            PipelineConfigOperation.RemovePipeline(_pipelineConfigList.FindAll(item => item.Status == PipelineConfigStatus.Remove), _pipelineControlList);
        }

        public PipelineConfig GetPipelineConfig(string pipelineName)
        {
            using (MySqlContext context = new MySqlContext())
            {
                try
                {
                    List<PipelineConfig> pipelineConfigs = context.PipelineConfig.ToList();
                    return pipelineConfigs.Find(item => item.Name == pipelineName);
                }
                catch (Exception ex)
                {
                    Logger.Fatal(String.Format("Cannot get a pipeline config from databases, exception message: {0}", ex.Message + ex.InnerException));
                    return null;
                }
            }
        }

        public void GenerateTestData()
        {
            PipelineConfig pipelineConfig = new PipelineConfig()
            {
                Name = "BDS.Pipeline.News.FuYang",
                Status = PipelineConfigStatus.Add,
                Type = PipelineReferenceType.DLL,
                PipelineReferenceAddress = @".\Resources\Pipelines\News\FuYang\BDS.Pipeline.News.FuYang.dll",
                LastExecuteDT = DateTime.Now.AddDays(-1),
                NextExecuteDT = DateTime.Now.AddDays(-1),
                ApartTimeType = PipelineScheduleApartTimeType.D,
                ApartTime = 1
            };
            using(MySqlContext context = new MySqlContext())
            {
                var config = context.PipelineConfig.FirstOrDefault(e => e.Name == pipelineConfig.Name);
                if (config == null)
                {
                    context.PipelineConfig.Add(pipelineConfig);
                    context.SaveChanges();
                }

            }
        }
        //private void DisplayPipelineAssemblies()
        //{
        //    Logger.Info("------------------Pipeline Assemblies------------------------------");
        //    Logger.Info("------------------Add Pipeline Assemblies--------------------------");
        //    _addPipelineAssemblies.ToList().ForEach(item => Logger.Info(String.Format("Add: AssemlyKey: {0}, AssemblyStatus: {1}", item.AssemblyKey, item.AssemblyStatus)));
        //    Logger.Info("------------------Remove Pipeline Assemblies-----------------------");
        //    _removePipelineAssemblies.ToList().ForEach(item => Logger.Info(String.Format("Remove: AssemlyKey: {0}, AssemblyStatus: {1}", item.AssemblyKey, item.AssemblyStatus)));
        //    Logger.Info("------------------Pipeline Assemblies------------------------------");
        //}

        //private void DisplayPipelines()
        //{
        //    Logger.Info("------------------Pipeline------------------------------");
        //    _pipelineList.ForEach(item => Logger.Info(String.Format("Pipeline: AssemlyKey: {0}", item.Name)));
        //    Logger.Info("------------------Pipeline------------------------------");
        //}

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Logger.Info("Start the BDS Pipeline Service");
            // Configure server
            InitialServer();
            while (!stoppingToken.IsCancellationRequested)
            {
                var snapshortPipelineControlList = _pipelineControlList;
                //Add or remove the pipeline from the pipeline collection.
                foreach (PipelineControl pipeline in snapshortPipelineControlList)
                {
                    Logger.Debug(String.Format("Already pipeline name {0}", pipeline.Resource.Name));
                    //if (pipeline.InvokeStatus == PipelineInvokeStatus.Invokeable && DateTime.Now >= pipeline.NextExecuteDT)
                    //{
                    //    //Set pipeline invoke unable that wait for pipeline execute complete.
                    //    pipeline.InvokeStatus = PipelineInvokeStatus.InvokeUnable;
                    //    Logger.Info(String.Format("Execute pipeline {0} ", pipeline.Name));
                    //    pipeline.ExecuteAsync();
                    //}
                    await Task.Delay(5000);
                }
            }
        }
    }
}
