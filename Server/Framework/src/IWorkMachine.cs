using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Framework
{
    public interface IWorkMachine
    {
        /// <summary>
        /// Get result data
        /// </summary>
        /// <param name="input">Input data</param>
        /// <param name="output">Output data</param>
        /// <param name="workFilter">Data filter</param>
        /// <returns>Work site status</returns>
        WorkSiteStatus Worker(IWorkSiteInput input, IWorkSiteOutput output);
    }
}
