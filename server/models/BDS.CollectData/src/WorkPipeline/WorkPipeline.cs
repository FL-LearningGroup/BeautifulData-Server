namespace BDS.CollectData
{
    using System;
    using System.Collections.Generic;
    using BDS.CollectData.Models;
    public class WorkPipeline
    {
        public WorkPipelineStatus status = WorkPipelineStatus.Build;
        private LinkedList<IWorkSite> _workSiteLinked = new LinkedList<IWorkSite>();
        public WorkPipeline()
        {

        }
        public LinkedList<IWorkSite> WorkSiteLinked
        {
            get
            {
                return this._workSiteLinked;
            }
        }
        public int AddWorkSite(IWorkSite workSite)
        {
            this._workSiteLinked.AddLast(workSite);
            return this._workSiteLinked.Count;
        }
        // Insert new node before current node.
        public bool InsertWorkSite(IWorkSite currentWorkSite, IWorkSite insertWorkSite)
        {
            //Linked list is null
            if(this._workSiteLinked.Count == 0)
            {
                return false;
            }
            LinkedListNode<IWorkSite> current = this._workSiteLinked.Find(currentWorkSite);
            //Not contain target work site.
            if(current == null)
            {
                return false;
            }
            this._workSiteLinked.AddBefore(current, insertWorkSite);
            return true;

        }

        public System.Int32 RemoveWorkSite(IWorkSite workSite)
        {
            this._workSiteLinked.Remove(workSite);
            return this._workSiteLinked.Count;
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