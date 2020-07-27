namespace BDS.CollectData.WorkPipeline
{
    using System;
    using BDS.CollectData.WorkPipeline.Models;
    internal interface IWorkSite
    {
        Guid Identifier {get;}
        WorkSiteStatus Status{get;}
        void Worker();

    }
    
}