namespace BDS.Pipeline.FuYang
{
    using System;
    using System.Collections.Generic;
    using BDS.CollectData;

    /// <summary>
    /// Store base url of Fu Yang public news info.
    /// </summary>
    public sealed class FYPublicInfoBaseUrl : Resource, IResource
    {
        private string type;
        override public string Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
        public string dataStore;
        public List<string> GetResourceData()
        {
            return new List<string>()
            {
                { this.dataStore }
            };
        }

        public System.Int64 StoreResourceData(List<string> data)
        {
            foreach (string item in data)
            {
                this.dataStore = item;
            }
            return 1;
        }

    }

}
