namespace BDS.CollectData
{
    using System;
    using System.Collections.Generic;
    using BDS.CollectData.Models;
    public class WorkPipeline
    {
        public WorkPipelineStatus status = WorkPipelineStatus.Build;
        LinkedList<IWorkSite> _workSiteLinked = new LinkedList<IWorkSite>();
        public void AddWorkSite(IWorkSite workSite)
        {
            this._workSiteLinked.AddLast(workSite);
        }
        public void InsertWorkSite(IWorkSite currentWorkSite, IWorkSite insertWorkSite)
        {
            LinkedListNode<IWorkSite> current = this._workSiteLinked.Find(currentWorkSite);
            this._workSiteLinked.AddAfter(current, insertWorkSite);
        }

        public void RemoveWorkSite(IWorkSite workSite)
        {
            this._workSiteLinked.Remove(workSite);
        }

        public void UpdateWorkSite(IWorkSite oldWorkSite, IWorkSite newWorkSite)
        {
            LinkedListNode<IWorkSite> current = this._workSiteLinked.Find(oldWorkSite);
            LinkedListNode<IWorkSite> preCurrent = current.Previous;
            this._workSiteLinked.Remove(current);
            this._workSiteLinked.AddAfter(preCurrent, newWorkSite);
        }
        public void ClearWorkPipeline()
        {
            this.status = WorkPipelineStatus.Build;
            this._workSiteLinked.Clear();
        }

        public void SetWorkPipelineStatus(WorkPipelineStatus status)
        {
            this.status = status;
        }
        public bool Processor()
        {
            if(this.status == WorkPipelineStatus.Executable)
            {
                foreach (IWorkSite workSite in this._workSiteLinked)
                {
                    try
                    {
                        workSite.Worker();
                        if(workSite.Status == WorkSiteStatus.Failed) 
                        {
                            this.status = WorkPipelineStatus.Failed;
                            throw new FailedWorkSiteException(workSite.Identifier, workSite.GetType().Name, "Work process failed.");
                        }
                    }
                    catch(FailedWorkSiteException ex)
                    {
                        // Log: Record failed work site.
                        Console.WriteLine(ex.Message);
                        return false;
                    }
                }
                return true;
            }
            return false;  
        }
    }
}