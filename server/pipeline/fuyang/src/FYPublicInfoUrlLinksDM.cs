﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Pipeline.FuYang
{
    /// <summary>
    /// Data Model: Defined data  structure of Fu Yang public info url links.
    /// </summary>
    public class FYPublicInfoUrlLinksDM
    {
        public string url;
        public FYPublicInfoUrlLinksDM(string url)
        {
            this.url = url;
        }
    }
}