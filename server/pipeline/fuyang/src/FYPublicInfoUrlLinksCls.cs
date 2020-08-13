namespace BDS.Pipeline.FuYang
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BDS.CollectData;
    /// <summary>
    /// Class that store all url links of Fu Yang public info.
    /// </summary>
    public class FYPublicInfoUrlLinksCls : Resource, IResource
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
        public List<FYPublicInfoUrlLinksDM> dataStore = new List<FYPublicInfoUrlLinksDM>();
        public List<string> GetResourceData()
        {
            List<string> urlList = new List<string>();
            foreach (FYPublicInfoUrlLinksDM item in this.dataStore)
            {
                urlList.Add(item.Url);
            }
            return urlList;
        }
        public System.Int64 StoreResourceData(List<string> data)
        {
            //Custom defined format of string that parse into filed datastore.
            foreach (string item in data)
            {
                string[] strArray = item.Split('@');
                foreach (string url in strArray)
                {
                    this.dataStore.Add(new FYPublicInfoUrlLinksDM(url));
                }
            }
            return this.dataStore.Count;
        }
    }
}
