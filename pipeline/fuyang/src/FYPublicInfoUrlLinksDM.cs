namespace BDS.Pipeline.FuYang
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    /// <summary>
    /// Data Model: Defined data  structure of Fu Yang public info url links.
    /// </summary>
    internal class FYPublicInfoUrlLinksDM
    {
        public string Url { get; set; }
        public FYPublicInfoUrlLinksDM(string url)
        {
            this.Url = url;
        }
    }
}
