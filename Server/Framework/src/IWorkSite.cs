using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Framework
{
    public interface IWorkSite
    {
        /// <summary>
        /// The Work site identifier
        /// </summary>
        string Identifier { get; set; }
        
        /// <summary>
        /// The Work site name
        /// </summary>
        string Name { get; set; }
        
        /// <summary>
        /// The Work site description
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// The Work site status
        /// Build, 
        /// Executable,
        /// Running,
        /// Success,
        /// Failed
        /// </summary>
        WorkSiteStatus Status { get; set; }
        
        /// <summary>
        /// Work site input resource
        /// </summary>
        IWorkSiteInput InputResource { get; }

        /// <summary>
        /// Work siet output resource
        /// </summary>
        IWorkSiteOutput OutputResource { get; }

        /// <summary>
        /// Work site run
        /// </summary>
        IWorkSite Worker();

        /// <summary>
        /// Set or replace work site
        /// </summary>
        /// <param name="inputResource">Input resource </param>
        /// <returns>Work site</returns>
        public IWorkSite SetOrReplaceWorkSiteInput(IWorkSiteInput inputResource);

        /// <summary>
        /// Set or replace work site
        /// </summary>
        /// <param name="outputResource">Output resource</param>
        /// <returns>Work site</returns>
        public IWorkSite SetOrReplaceWorkSiteOutput(IWorkSiteOutput outputResource);

        /// <summary>
        /// Set or replace work machine
        /// </summary>
        /// <param name="workMachine">Work machine</param>
        /// <returns>Work site</returns>
        public IWorkSite SetOrReplaceWorkMachine(IWorkMachine workMachine);

        /// <summary>
        /// Add resource event handler in work site
        /// </summary>
        /// <param name="resourceEventHandle">Resource Evnet Handle</param>
        /// <returns>Work Site</returns>
        public IWorkSite AddResourceEventHandler(IResourceEventHandle resourceEventHandle);


        /// <summary>
        /// Remove resource event handler in work site
        /// </summary>
        /// <param name="resourceEventHandle">Resource Evnet Handle</param>
        /// <returns>Work Site</returns>
        public IWorkSite RemoveResourceEventHandler(IResourceEventHandle resourceEventHandle);

        /*
        /// <summary>
        /// Set or replace work filter
        /// </summary>
        /// <param name="workFilter">Work filter</param>
        /// <returns>Work site</returns>
        public IWorkSite SetOrReplaceWorkFilter(List<IWorkFilter> workFilter);
        */
    }
}
