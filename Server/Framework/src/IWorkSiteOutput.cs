using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Framework
{
    public interface IWorkSiteOutput
    {
        /// <summary>
        /// Store resource data
        /// </summary>
        /// <param name="data">data</param>
        /// <returns>Count of data</returns>
        void TransferWorkSiteDataToResourceData(List<string> data);
    }
}
