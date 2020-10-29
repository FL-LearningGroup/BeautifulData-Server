using System;
using System.Collections.Generic;

namespace BDS.Framework
{
    public interface IWorkPipeline
    {
        /// <summary>
        /// Work pipeline status:
        /// Build: the worke pipeline is creating, not excutable.
        /// Executable: the worke pipeline is executing.
        /// Forbid: the worke pipeline is forbid.
        /// Failed: the worke pipeline execution resule failed.
        /// </summary>
        public WorkPipelineStatus Status { get; set; }
        
        /// <summary>
        /// The work sites linked list.
        /// </summary>
        public LinkedList<IWorkSite> WorkSites { get;}
        
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
        public IWorkSite SelectWorkSite(string identifier);

        /// <summary>
        /// Add a work site into the work site linked.
        /// </summary>
        /// <param name="workSite">The add of work site.</param>
        /// <returns>The count of the work site linked.</returns>
        public int AddWorkSite(IWorkSite workSite);

        /// <summary>
        /// Insert a work site  before the current work site.
        /// </summary>
        /// <param name="currentIdentifier">current work site</param>
        /// <param name="insertWorkSite">Insert work site</param>
        /// <returns>Insert result true or false.</returns>
        public bool InsertWorkSite(string currentIdentifier, IWorkSite insertWorkSite);

        /// <summary>
        /// Remove a work site in work site linked by identifier.
        /// </summary>
        /// <param name="currentIdentifier">identifier</param>
        /// <returns>Removed work site</returns>
        public IWorkSite RemoveWorkSite(string currentIdentifier);

        /// <summary>
        /// Update an existing work site.
        /// </summary>
        /// <param name="currentIdentifier">Identifier</param>
        /// <param name="newWorkSite">New work site</param>
        /// <returns>Update result true or false.</returns>
        public bool UpdateWorkSite(string currentIdentifier, IWorkSite newWorkSite);

        /// <summary>
        /// Remove all work sites of the work site linked.
        /// </summary>
        /// <returns>Count of the work site linked.</returns>
        public int ClearWorkPipeline();

        /// <summary>
        /// Execute work pipeline.
        /// </summary>
        /// <returns>Execute resule successed or failed.</returns>
        public WorkPipelineStatus Processor();
    }
}
