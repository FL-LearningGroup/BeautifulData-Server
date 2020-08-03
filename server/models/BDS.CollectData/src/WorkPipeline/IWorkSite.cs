namespace BDS.CollectData
{
    using System;
    using BDS.CollectData.Models;
    public interface IWorkSite
    {
        string Identifier {get;}
        WorkSiteStatus Status{get;}
        void Worker();

    }
    
}