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
        /// <summary>
        /// Adds pipeline into the pipeline collection.
        /// </summary>
        public static int AddPipeline(List<PipelineConfig> addList, List<PipelineControl> sourceList)
        {
            // Add element of specified collection to the source collection.
            if (addList.Count == 0) return 0;
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
                PiplelineScheduleTimeOperation piplelineScheduleTime = new PiplelineScheduleTimeOperation() { StartTime = additem.StartDT, Model = additem.ApartTimeType, ApartTime = additem.ApartTime };
                sourceSnapshotList.Add(new PipelineControl(new DockPipelineOperations(additem.Name, dllAddress, piplelineScheduleTime), new CancellationTokenSource()));
            }
            try
            {
                // Update status of the pipeline config
                using (MySqlContext context = new MySqlContext())
                {
                    addList.ForEach(item => item.Status = PipelineConfigStatus.Wait);
                    context.PipelineConfig.UpdateRange(addList);
                    context.SaveChanges();
                }
                // Add pipeline after successed update the pipeline config.
                sourceList.AddRange(sourceSnapshotList);
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to update status of the pipeline config to the database before add pipeline collection.detail error msg: {0}", ex.ToString()));
            }

            return addList.Count;
        }
        /// <summary>
        /// Stops pipeline into the pipeline collection.
        /// </summary>
        public static int StopPipeline(List<PipelineConfig> stopList, List<PipelineControl> sourceList)
        {
            if (stopList.Count == 0) return 0;
            foreach (PipelineConfig pipelineConfig in stopList)
            {
                sourceList.ForEach(item => 
                    { 
                        if (item.Resource.Name == pipelineConfig.Name)
                        {
                            item.CancelTakenSource.Cancel();
                        }
                    }
                );
            }
            // Updates stop to stopped in the piepline with cancel event of the task.
            return stopList.Count;
        }

        /// <summary>
        /// Remove pipeline into the pipeline collection.
        /// </summary>
        public static int RemovePipeline(List<PipelineConfig> removeList, List<PipelineControl> sourceList)
        {
            if (removeList.Count == 0) return 0;
            List<PipelineControl> removeSnapshotList = new List<PipelineControl>();
            foreach (PipelineConfig pipelineConfig in removeList)
            {
                sourceList.ForEach(item =>
                {
                    if (item.Resource.Name == pipelineConfig.Name)
                    {
                        removeSnapshotList.Add(item);
                    }
                }
                );
            }
            removeSnapshotList.ForEach(item => sourceList.Remove(item));
            return removeSnapshotList.Count;
        }
    }
}
