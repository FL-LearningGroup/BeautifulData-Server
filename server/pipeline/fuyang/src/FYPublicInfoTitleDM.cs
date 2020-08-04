using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Pipeline.FuYang
{
    /// <summary>
    /// Data Model: Defined data structure of Fu Yang public info title 
    /// </summary>
    public class FYPublicInfoTitleDM
    {
        public string dataTime;
        public string url;
        public string title;

        public FYPublicInfoTitleDM(string dataTime, string url, string title)
        {
            this.dataTime = dataTime;
            this.url = url;
            this.title = title;
        }
    }
}
