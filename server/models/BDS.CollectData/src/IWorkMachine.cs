namespace BDS.CollectData
{
    using BDS.CollectData.Models;
    using System.Collections.Generic;
    /// <Summary> Work machine </Summary>
    public interface IWorkMachine
    {
        /// <summary>
        /// Get result data
        /// </summary>
        /// <param name="input">Input data</param>
        /// <param name="output">Output data</param>
        /// <param name="workFilter">Data filter</param>
        /// <returns>Work site status</returns>
        WorkSiteStatus Worker(IWorkSiteInput input, IWorkSiteOutput output, List<IWorkFilter> workFilter);
    }
    
}