using BDS.Runtime.DataBase;
using BDS.Runtime.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace BDS.Runtime
{
    /// <summary>
    /// Operate pipeline collection according the pipeline config information.
    /// </summary>
    internal class PipelineConfigOperation
    {
        public static int UpdateStatus(List<PipelineConfig> updateList, PipelineConfigStatus status)
        {
            if (updateList.Count <= 0) return 0;
            try
            {
                // Update status of the pipeline config
                using (MySqlContext context = new MySqlContext())
                {
                    updateList.ForEach(item => item.Status = status);
                    context.PipelineConfig.UpdateRange(updateList);
                    return context.SaveChanges();
                }                 
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to update status of the pipeline config to the database before add pipeline collection.detail error msg: {0}", ex.ToString()));
                return -1;
            }
        }
        /// <summary>
        /// Adds pipeline into the pipeline collection.
        /// </summary>
        public static int AddPipeline(List<PipelineConfig> addList, List<PipelineControl> sourceList)
        {
            // Add element of specified collection to the source collection.
            if (addList.Count <= 0) return 0;
            List<PipelineControl> sourceSnapshotList = new List<PipelineControl>();
            foreach(PipelineConfig additem in addList)
            {
                string dllAddress = GlobalVariables.WorkFolder + GlobalVariables.ResourcesFolder + "Pipeline" + Path.DirectorySeparatorChar + additem.Name + Path.DirectorySeparatorChar + additem.Name + ".dll";
                if (additem.Type == PipelineReferenceType.DLL && additem.AddressType == PipelineReferenceAddressType.Local)
                {
                    if (!String.IsNullOrEmpty(additem.PipelineReferenceAddress))
                    { 
                        dllAddress = GlobalVariables.WorkFolder + additem.PipelineReferenceAddress;
                    }
                }
                sourceSnapshotList.Add(new PipelineControl(new DockPipelineOperations(additem.Name, dllAddress), PipelineConfigStatus.Wait, additem.LastExecuteDT, additem.NextExecuteDT, new PiplelineScheduleTimeOperation(additem.ApartTimeType, additem.ApartTime), new CancellationTokenSource()));
            }
            if (UpdateStatus(addList, PipelineConfigStatus.Wait) > 0)
            {
                sourceList.AddRange(sourceSnapshotList);
            }

            return addList.Count;
        }
        /// <summary>
        /// Stops pipeline of the pipeline collection.
        /// </summary>
        public static int StopPipeline(List<PipelineConfig> stopList, List<PipelineControl> sourceList)
        {
            if (stopList.Count <= 0) return 0;
            // Befor update database then update the pipeline collection. Because the connection of db is easy to throw error
            if (UpdateStatus(stopList, PipelineConfigStatus.Stopped) > 0)
            {
                foreach (PipelineConfig pipelineConfig in stopList)
                {
                    sourceList.ForEach(item =>
                    {
                        if (item.Resource.Name == pipelineConfig.Name)
                        {
                            item.CancelTakenSource.Cancel();
                            // Wait: Consider whether Updates stop to stopped in the piepline with cancel event of the task.
                            item.Status = PipelineConfigStatus.Stopped;
                        }
                    }
                    );
                }
            }
            return stopList.Count;
        }

        /// <summary>
        /// Remove pipeline of the pipeline collection.
        /// </summary>
        public static int RemovePipeline(List<PipelineConfig> removeList, List<PipelineControl> sourceList)
        {
            if (removeList.Count <= 0) return 0;
            List<PipelineConfig> stopSnapshotList = new List<PipelineConfig>();
            List<PipelineControl> removeSnapshotList = new List<PipelineControl>();
            foreach (PipelineConfig pipelineConfig in removeList)
            {
                sourceList.ForEach(item =>
                    {
                        if (item.Resource.Name == pipelineConfig.Name)
                        {
                            if (item.Status != PipelineConfigStatus.Stopped)
                            {
                                stopSnapshotList.Add(pipelineConfig);
                            }
                            else
                            {
                                removeSnapshotList.Add(item);
                            }
                        }
                    }
                );
            }
            if (stopSnapshotList.Count > 0)
            {
                if (StopPipeline(stopSnapshotList, sourceList) > 0)
                {
                    foreach(PipelineConfig pipelineConfig in stopSnapshotList)
                    {
                        sourceList.ForEach(item => {
                            if (item.Resource.Name == pipelineConfig.Name)
                                removeSnapshotList.Add(item);
                            });
                    }
                };
            }
            // Befor update database then update the pipeline collection. Because the connection of db is easy to throw error
            if (UpdateStatus(removeList, PipelineConfigStatus.Removed) > 0)
            {
                removeSnapshotList.ForEach(item => sourceList.Remove(item));
                return removeSnapshotList.Count;
            }
            return 0;
        }
        /// <summary>
        /// Restart pipeline of the pipeline collection.
        /// </summary>
        public static int RestartPipeline(List<PipelineConfig> restartList, List<PipelineControl> sourceList)
        {
            if (restartList.Count <= 0) return 0;
            List<PipelineConfig> stopSnapshotList = new List<PipelineConfig>();
            List<PipelineControl> restartSnapshotList = new List<PipelineControl>();

            foreach (PipelineConfig pipelineConfig in restartList)
            {
                sourceList.ForEach(item =>
                    {
                        if (item.Resource.Name == pipelineConfig.Name)
                        {
                            if (item.Status != PipelineConfigStatus.Stopped)
                            {
                                stopSnapshotList.Add(pipelineConfig);
                            }
                            else
                            {
                                restartSnapshotList.Add(item);
                            }
                        }
                    }
                );
            }

            if (stopSnapshotList.Count > 0)
            {
                if (StopPipeline(stopSnapshotList, sourceList) > 0)
                {
                    foreach (PipelineConfig pipelineConfig in stopSnapshotList)
                    {
                        sourceList.ForEach(item => {
                            if (item.Resource.Name == pipelineConfig.Name)
                                restartSnapshotList.Add(item);
                        });
                    }
                };
            }
            // Befor update database then update the pipeline collection. Because the connection of db is easy to throw error
            if (UpdateStatus(restartList, PipelineConfigStatus.Wait) > 0)
            {
                foreach (PipelineConfig pipelineConfig in restartList)
                {
                    sourceList.ForEach(item =>
                    {
                        if (item.Resource.Name == pipelineConfig.Name)
                        {
                            item.Status = PipelineConfigStatus.Wait;
                        }
                    });
                }
                return restartSnapshotList.Count;
            }
            return 0;
        }
    }
}
