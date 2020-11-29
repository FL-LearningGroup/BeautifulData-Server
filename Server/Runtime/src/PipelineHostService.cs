using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BDS.Runtime.Models;
using BDS.Runtime.DataBase;
using System.IO;
using System.Linq;
using log4net.Util;
using BDS.Framework;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Xml;

namespace BDS.Runtime
{
    /// <summary>
    /// Function:
    /// 1. Monitor assembly confg.
    /// 2. New pipeline
    /// </summary>
    internal class PipelineHostService : BackgroundService, IPielineHostService
    {
        private ServerConfigSwitch _configSwitch;
        private List<PipelineConfig> _pipelineConfigList;
        private List<PipelineControl> _pipelineControlList;
        private XmlNodeList _serverConfig = null;
        public PipelineHostService()
        {
            _pipelineConfigList = new List<PipelineConfig>();
            _pipelineControlList = new List<PipelineControl>();
            _configSwitch = new ServerConfigSwitch();
        }

        private void InitialServer()
        {
            _configSwitch.ServerSwitchEvent += GetPipelineConfigs;
            CheckOrCreateDBSchema();
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
                //Add or remove the pipeline from the pipeline collection.
                foreach(PipelineControl pipeline in _pipelineControlList)
                {
                    //if (pipeline.InvokeStatus == PipelineInvokeStatus.Invokeable && DateTime.Now >= pipeline.NextExecuteDT)
                    //{
                    //    //Set pipeline invoke unable that wait for pipeline execute complete.
                    //    pipeline.InvokeStatus = PipelineInvokeStatus.InvokeUnable;
                    //    Logger.Info(String.Format("Execute pipeline {0} ", pipeline.Name));
                    //    pipeline.ExecuteAsync();
                    //}
                }
            }
        }
    }
}
