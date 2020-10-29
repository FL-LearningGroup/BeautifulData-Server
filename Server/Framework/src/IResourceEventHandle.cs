using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Framework
{
    public interface IResourceEventHandle
    {
        void ResourceDataEvent(object sender, WorkSiteStatusTriggerEventArgs e);
    }
}
