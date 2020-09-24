namespace BDS.CollectData
{
    using System;
    using System.Collections.Generic;
    using BDS.CollectData.Models;
    using BDS.CollectData.BDSException;
    
    /// <summary>
    /// Pipeline for resource work
    /// </summary>
    [Serializable]
    public class WorkPipeline
    {
        private WorkPipelineStatus _status = WorkPipelineStatus.Build;
        private LinkedList<IWorkSite> _workSiteLinked = new LinkedList<IWorkSite>();
        public string Name { get; set; }
        public string Description { get; set; }
        public WorkPipeline()
        {

        }
        /// <summary>
        /// Work status of status
        /// </summary>
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
        /// <summary>
        /// Site linked of workpipeline
        /// </summary>
        public LinkedList<IWorkSite> WorkSiteLinked
        {
            get
            {
                return this._workSiteLinked;
            }
        }
        /// <summary>
        /// Select site in work pipeline linked.
        /// </summary>
        /// <param name="identifier">identifier of work site</param>
        /// <returns>Work site</returns>
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

        /// <summary>
        /// Add work site in work pipeline linked.
        /// </summary>
        /// <param name="workSite">Work site</param>
        /// <returns>Count of work site in work pipeline</returns>
        public int AddWorkSite(IWorkSite workSite)
        {
            this._workSiteLinked.AddLast(workSite);
            return this._workSiteLinked.Count;
        }

        /// <summary>
        /// Insert new node before currentNode node.
        /// </summary>
        /// <param name="currentIdentifier">Current identifier of work site</param>
        /// <param name="insertWorkSite">Insert work site</param>
        /// <returns>Insert result</returns>
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

        /// <summary>
        /// Remove work site in work pipeline
        /// </summary>
        /// <param name="currentIdentifier">Current identifier of work site</param>
        /// <returns>Count of work site in  work pipline</returns>
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

        /// <summary>
        /// Update work site in work pipeline
        /// </summary>
        /// <param name="currentIdentifier">Current identifier of work site</param>
        /// <param name="newWorkSite">New work site</param>
        /// <returns>Update result</returns>
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
        /// <summary>
        /// Clear Work pipline.
        /// </summary>
        /// <returns>Count of work sitt in work pipeline</returns>
        public System.Int32 ClearWorkPipeline()
        {
            this._workSiteLinked.Clear();
            this.Status = WorkPipelineStatus.Build;
            return this._workSiteLinked.Count;
        }

        /// <summary>
        /// Check if executable of work site.
        /// </summary>
        /// <returns>Result check</returns>
        private string CheckWorkSiteExecutable()
        {
            foreach (IWorkSite workSite in this._workSiteLinked)
            {
                if((workSite.Status != WorkSiteStatus.Executable) && (workSite.Status != WorkSiteStatus.Success))
                {
                    return workSite.Identifier;
                }
            }
            return null;
        }
        /// <summary>
        /// Execute work site.
        /// </summary>
        /// <returns>Result execute.</returns>
        public bool Processor()
        {
            if (this.Status == WorkPipelineStatus.Executable)
            {
                string executable = CheckWorkSiteExecutable();
                if (!String.IsNullOrEmpty(executable))
                {
                    throw new WorkPipelineException(Name, String.Format(" The status of {0}(WorkSite) is not executable in work pipline.", executable));
                }
                foreach (IWorkSite workSite in this._workSiteLinked)
                {
                    workSite.Worker();
                    if (workSite.Status == WorkSiteStatus.Failed) 
                    {
                        Status = WorkPipelineStatus.Failed;
                    }
                }
                return true;
            }
            return false;  
        }
    }
}