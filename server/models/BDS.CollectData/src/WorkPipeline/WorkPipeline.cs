namespace BDS.CollectData.WorkPipeline
{
    using System;
    using System.Collections.Generic;
    using BDS.CollectData.WorkPipeline.Models;
    internal class WorkPipeline
    {
        public WorkPipelineStatus status = WorkPipelineStatus.Build;
        LinkedList<IWorkSite> workSiteLinked;
        public void AddWorkSite(IWorkSite workSite)
        {
            this.workSiteLinked.AddLast(workSite);
        }
        public void InsertWorkSite(IWorkSite currentWorkSite, IWorkSite insertWorkSite)
        {
            LinkedListNode<IWorkSite> current = this.workSiteLinked.Find(currentWorkSite);
            this.workSiteLinked.AddAfter(current, insertWorkSite);
        }

        public void RemoveWorkSite(IWorkSite workSite)
        {
            this.workSiteLinked.Remove(workSite);
        }

        public void UpdateWorkSite(IWorkSite oldWorkSite, IWorkSite newWorkSite)
        {
            LinkedListNode<IWorkSite> current = this.workSiteLinked.Find(oldWorkSite);
            LinkedListNode<IWorkSite> preCurrent = current.Previous;
            this.workSiteLinked.Remove(current);
            this.workSiteLinked.AddAfter(preCurrent, newWorkSite);
        }
        public void ClearWorkPipeline()
        {
            this.status = WorkPipelineStatus.Build;
            this.workSiteLinked.Clear();
        }

        public void SetWorkPipelineStatus(WorkPipelineStatus status)
        {
            this.status = status;
        }
        public bool Processor()
        {
            if(this.status == WorkPipelineStatus.Executable)
            {
                foreach (IWorkSite workSite in this.workSiteLinked)
                {
                        workSite.Worker();
                        if(workSite.Status == WorkSiteStatus.Failed) 
                        {
                            this.status = WorkPipelineStatus.Failed;
                            return false;
                        }
                }
                return true;
            }
            return false;  
        }
    }
}