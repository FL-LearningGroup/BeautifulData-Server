namespace BDS.CollectData.WorkPipeline
{
    using BDS.CollectData.Resource;
    using BDS.CollectData.WorkPipeline.Models;
    /// <Summary> Work machine </Summary>
    internal interface IWorkMachine
    {
        WorkSiteStatus worker(IResource sourceResource, IResource targetResource, IWorkFilter workFilter);
    }
    
}