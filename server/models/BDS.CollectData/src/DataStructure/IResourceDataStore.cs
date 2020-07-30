namespace BDS.CollectData
{
    using System.Collections.Generic;
    public interface IResourceDataStore
    {
        Dictionary<string, string> GetResourceData();
        System.Int64 StoreResourceData(Dictionary<string, string> data);
    }    
}