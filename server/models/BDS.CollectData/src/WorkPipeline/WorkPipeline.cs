namespace BDS.CollectData
{
    using System;
    using System.Collections.Generic;
    using BDS.CollectData.Models;
    using BDS.CollectData.BDSException;
    public class WorkPipeline
    {
        private WorkPipelineStatus _status = WorkPipelineStatus.Build;
        private LinkedList<IWorkSite> _workSiteLinked = new LinkedList<IWorkSite>();
        public WorkPipeline()
        {

        }
        public WorkPipelineStatus Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }
        public LinkedList<IWorkSite> WorkSiteLinked
        {
            get
            {
                return this._workSiteLinked;
            }
        }
        public IWorkSite SelectWorkSite(string identifier)
        {
            foreach(IWorkSite workSite in WorkSiteLinked)
            {
                if(workSite.Identifier == identifier)
                {
                    return workSite;
                }
            }
            return null;
        }
        public int AddWorkSite(IWorkSite workSite)
        {
            this._workSiteLinked.AddLast(workSite);
            return this._workSiteLinked.Count;
        }
        // Insert new node before currentNode node.
        public bool InsertWorkSite(string currentIdentifier, IWorkSite insertWorkSite)
        {
            IWorkSite workSite =  SelectWorkSite(currentIdentifier);
            //Linked list is null
            if (this._workSiteLinked.Count == 0)
            {
                return false;
            }
            LinkedListNode<IWorkSite> currentNode = this._workSiteLinked.Find(workSite);
            //Not contain target work site.
            if(workSite == null)
            {
                return false;
            }
            this._workSiteLinked.AddBefore(currentNode, insertWorkSite);
            return true;

        }

        public System.Int32 RemoveWorkSite(string currentIdentifier)
        {
            IWorkSite workSite = SelectWorkSite(currentIdentifier);
            if(workSite == null)
            {
                return -1;
            }
            this._workSiteLinked.Remove(workSite);
            return this._workSiteLinked.Count;
        }

        public bool UpdateWorkSite(string currentIdentifier, IWorkSite newWorkSite)
        {
            IWorkSite oldWorkSite = SelectWorkSite(currentIdentifier);
            if(oldWorkSite == null)
            {
                return false;
            }
            LinkedListNode<IWorkSite> currentNode = this._workSiteLinked.Find(oldWorkSite);
            LinkedListNode<IWorkSite> preNode = currentNode.Previous;
            // First node
            if(preNode == null)
            {
                LinkedListNode <IWorkSite> nextNode = currentNode.Next;
                this._workSiteLinked.Remove(currentNode);
                this._workSiteLinked.AddBefore(nextNode, newWorkSite);
                return true;
            }
            this._workSiteLinked.Remove(currentNode);
            this._workSiteLinked.AddAfter(preNode, newWorkSite);
            return true;
        }
        public System.Int32 ClearWorkPipeline()
        {
            this._workSiteLinked.Clear();
            this.Status = WorkPipelineStatus.Build;
            return this._workSiteLinked.Count;
        }

        private bool CheckWorkSiteExecutable()
        {
            foreach (IWorkSite workSite in this._workSiteLinked)
            {
                if((workSite.Status != WorkSiteStatus.Executable) && (workSite.Status != WorkSiteStatus.Success))
                {
                    return false;
                }
            }
            return true;
        }
        public bool Processor()
        {
            if(this.Status == WorkPipelineStatus.Executable)
            {
                bool executable = CheckWorkSiteExecutable();
                if(!executable)
                {
                    throw new System.Exception("Exception: The status of Work Site is not executable.");
                }
                foreach (IWorkSite workSite in this._workSiteLinked)
                {
                    workSite.Status = WorkSiteStatus.Running;
                    workSite.Worker();
                    if(workSite.Status == WorkSiteStatus.Failed) 
                    {
                        this.Status = WorkPipelineStatus.Failed;
                        throw new WorkSiteException(workSite.Identifier, workSite.GetType().Name, "Work process failed.");
                    }
                }
                return true;
            }
            return false;  
        }
    }
}