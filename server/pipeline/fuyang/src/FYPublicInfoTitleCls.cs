
namespace BDS.Pipeline.FuYang
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BDS.CollectData;
    /// <summary>
    /// Class: Defined Fu Yang public info title.
    /// </summary>
    public sealed class FYPublicInfoTitleCls : Resource, IResource, ITransferResourceData
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
        /// <summary>
        /// Get data from property dataStore.
        /// </summary>
        /// <returns>list string</returns>
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
        /// <summary>
        /// Store data to property dataStore
        /// </summary>
        /// <param name="data">Need data of store</param>
        /// <returns>data counr</returns>
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

        public bool TransferData(string type)
        {
            
            return true;
        }
    }
}
