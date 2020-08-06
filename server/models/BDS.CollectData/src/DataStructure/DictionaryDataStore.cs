namespace BDS.CollectData
{
    using System;
    using System.Collections.Generic;
    public class DictionaryDataStore
    {
        private Dictionary<string, string> storeSpace;

        public Dictionary<string, string> GetResourceData()
        {
            return this.storeSpace;
        }
        public System.Int64 StoreResourceData(Dictionary<string, string> data)
        {
            this.storeSpace = data;
            return this.storeSpace.Count;
        }     
    }
}