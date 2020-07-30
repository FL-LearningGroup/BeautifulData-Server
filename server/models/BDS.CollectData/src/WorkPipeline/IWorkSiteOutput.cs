namespace BDS.CollectData
{
    using System.Collections.Generic;
    public interface IWorkSiteOutput
    {
          System.Int64 StoreResourceData(List<string> data);
    }
}