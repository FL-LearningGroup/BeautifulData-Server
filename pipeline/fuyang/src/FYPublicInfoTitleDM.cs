namespace BDS.Pipeline.FuYang
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    /// <summary>
    /// Data Model: Defined data structure of Fu Yang public info title 
    /// </summary>
    [Serializable]
    internal class FYPublicInfoTitleDM
    {
        public string DateTime { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }

        public FYPublicInfoTitleDM(string dateTime, string url, string title)
        {
            this.DateTime = dateTime;
            this.Url = url;
            this.Title = title;
        }
    }
}
