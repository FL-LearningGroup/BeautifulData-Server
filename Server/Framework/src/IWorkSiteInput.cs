using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Framework
{
    public interface IWorkSiteInput
    {
        /// <summary>
        /// Get data from resource
        /// </summary>
        /// <returns>Data list</returns>
        List<string> TransferResourceDataToWorkSiteData();
    }
}
