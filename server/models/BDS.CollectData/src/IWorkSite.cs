namespace BDS.CollectData
{
    using System;
    using BDS.CollectData.Models;
    /// <summary>
    /// Work site
    /// </summary>
    public interface IWorkSite
    {
        /// <summary>
        /// Work site identifier
        /// </summary>
        string Identifier { get; set; }
        /// <summary>
        /// Work site name
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Work site description
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Work site status
        /// </summary>
        WorkSiteStatus Status{ get; set; }
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
        void Worker();

    }
    
}