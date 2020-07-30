namespace BDS.CollectData
{
    using System;
    using BDS.CollectData.Models;
    public interface IWorkSite
    {
        Guid Identifier {get;}
        WorkSiteStatus Status{get;}
        void Worker();

    }
    
}