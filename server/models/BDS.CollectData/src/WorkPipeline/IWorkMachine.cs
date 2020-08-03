namespace BDS.CollectData
{
    using BDS.CollectData.Models;
    using System.Collections.Generic;
    /// <Summary> Work machine </Summary>
    public interface IWorkMachine
    {
        WorkSiteStatus Worker(IWorkSiteInput input, IWorkSiteOutput output, List<IWorkFilter> workFilter);
    }
    
}