using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Framework
{
    public class WorkPipeline: IWorkPipeline
    {
        private WorkPipelineStatus _status = WorkPipelineStatus.Build;
        private LinkedList<IWorkSite> _workSites = new LinkedList<IWorkSite>();
        public WorkPipeline()
        {

        }

        /// <summary>
        /// Work pipeline status:
        /// Build: the worke pipeline is creating, not excutable.
        /// Executable: the worke pipeline is executing.
        /// Forbid: the worke pipeline is forbid.
        /// Failed: the worke pipeline execution resule failed.
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
        /// The work sites linked list.
        /// </summary>
        public LinkedList<IWorkSite> WorkSites
        {
            get
            {
                return this._workSites;
            }
        }

        /// <summary>
        /// Work pipeline name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Work pipeline description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Get a work site by identifier in the work site linked.
        /// </summary>
        /// <param name="identifier">identifier</param>
        /// <returns>Work site</returns>
        public IWorkSite SelectWorkSite(string identifier)
        {
            foreach (IWorkSite workSite in _workSites)
            {
                if (workSite.Identifier == identifier)
                {
                    return workSite;
                }
            }
            return null;
        }

        /// <summary>
        /// Add a work site into the work site linked.
        /// </summary>
        /// <param name="workSite">The add of work site.</param>
        /// <returns>The count of the work site linked.</returns>
        public int AddWorkSite(IWorkSite workSite)
        {
            this._workSites.AddLast(workSite);
            return this._workSites.Count;
        }

        /// <summary>
        /// Insert a work site  before the current work site.
        /// </summary>
        /// <param name="currentIdentifier">current work site</param>
        /// <param name="insertWorkSite">Insert work site</param>
        /// <returns>Insert result true or false.</returns>
        public bool InsertWorkSite(string currentIdentifier, IWorkSite insertWorkSite)
        {
            IWorkSite workSite = SelectWorkSite(currentIdentifier);
            //Linked list is null
            if (this._workSites.Count == 0)
            {
                return false;
            }
            LinkedListNode<IWorkSite> currentNode = this._workSites.Find(workSite);
            //Not contain target work site.
            if (workSite == null)
            {
                return false;
            }
            this._workSites.AddBefore(currentNode, insertWorkSite);
            return true;

        }

        /// <summary>
        /// Remove a work site in work site linked by identifier.
        /// </summary>
        /// <param name="currentIdentifier">identifier</param>
        /// <returns>Removed work site</returns>
        public IWorkSite RemoveWorkSite(string currentIdentifier)
        {
            IWorkSite workSite = SelectWorkSite(currentIdentifier);
            if (workSite == null)
            {
                return null;
            }
            this._workSites.Remove(workSite);
            return workSite;
        }

        /// <summary>
        /// Update an existing work site.
        /// </summary>
        /// <param name="currentIdentifier">Identifier</param>
        /// <param name="newWorkSite">New work site</param>
        /// <returns>Update result true or false.</returns>
        public bool UpdateWorkSite(string currentIdentifier, IWorkSite newWorkSite)
        {
            IWorkSite oldWorkSite = SelectWorkSite(currentIdentifier);
            if (oldWorkSite == null)
            {
                return false;
            }
            LinkedListNode<IWorkSite> currentNode = this._workSites.Find(oldWorkSite);
            LinkedListNode<IWorkSite> preNode = currentNode.Previous;
            // First node
            if (preNode == null)
            {
                LinkedListNode<IWorkSite> nextNode = currentNode.Next;
                this._workSites.Remove(currentNode);
                this._workSites.AddBefore(nextNode, newWorkSite);
                return true;
            }
            this._workSites.Remove(currentNode);
            this._workSites.AddAfter(preNode, newWorkSite);
            return true;
        }

        /// <summary>
        /// Remove all work sites of the work site linked.
        /// </summary>
        /// <returns>Count of the work site linked.</returns>
        public int ClearWorkPipeline()
        {
            this._workSites.Clear();
            this.Status = WorkPipelineStatus.Build;
            return this._workSites.Count;
        }

        /// <summary>
        /// Check if executable of work site.
        /// </summary>
        /// <returns>Result check</returns>
        private string CheckWorkSiteExecutable()
        {
            foreach (IWorkSite workSite in this._workSites)
            {
                if ((workSite.Status != WorkSiteStatus.Executable) && (workSite.Status != WorkSiteStatus.Success))
                {
                    return workSite.Name + "-" + workSite.Identifier;
                }
            }
            return null;
        }

        /// <summary>
        /// Execute work pipeline.
        /// </summary>
        /// <returns>Execute resule successed or failed.</returns>
        public WorkPipelineStatus Processor()
        {
            if (this.Status == WorkPipelineStatus.Executable)
            {
                string workSiteName = CheckWorkSiteExecutable();
                if (!String.IsNullOrEmpty(workSiteName))
                {
                    throw new Exception(String.Format(" The worksite named {0} is not executable in work pipline. The ", workSiteName));
                }
                foreach (IWorkSite workSite in this._workSites)
                {
                    workSite.Worker();
                    if (workSite.Status == WorkSiteStatus.Failed)
                    {
                        Status = WorkPipelineStatus.Failed;
                        return WorkPipelineStatus.Failed;
                    }
                }
                Status = WorkPipelineStatus.Success;
                return WorkPipelineStatus.Success;
            }
            return WorkPipelineStatus.Failed;
        }
    }
}
