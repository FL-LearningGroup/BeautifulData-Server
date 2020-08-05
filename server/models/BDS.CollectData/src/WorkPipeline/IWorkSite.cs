namespace BDS.CollectData
{
    using System;
    using BDS.CollectData.Models;
    public interface IWorkSite
    {
        string Identifier {get;}
        WorkSiteStatus Status{ get; set; }
        IWorkSiteInput InputResource { get; }
        IWorkSiteOutput OutputResource { get; }
        void Worker();

    }
    
}