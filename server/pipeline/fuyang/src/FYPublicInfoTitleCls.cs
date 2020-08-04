using System;
using System.Collections.Generic;
using System.Text;
using BDS.CollectData;

namespace BDS.Pipeline.FuYang
{
    /// <summary>
    /// Class: Defined Fu Yang public info title.
    /// </summary>
    public sealed class FYPublicInfoTitleCls : Resource, IResource
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
        public List<FYPublicInfoTitleDM> dataStore = new List<FYPublicInfoTitleDM>();
        public List<string> GetResourceData()
        {
            List<string> urlList = new List<string>();
            string baseHost = "http://www.fy.gov.cn";
            foreach (FYPublicInfoTitleDM item in this.dataStore)
            {
                urlList.Add(baseHost + item.url);
            }
            return urlList;
        }
        public System.Int64 StoreResourceData(List<string> data)
        {
            //Custom defined format of string that parse into filed datastore.
            foreach (string item in data)
            {
                string[] strArray = item.Split('@');
                this.dataStore.Add(new FYPublicInfoTitleDM(strArray[0], strArray[1], strArray[2]));
            }
            return this.dataStore.Count;
        }
    }
}
