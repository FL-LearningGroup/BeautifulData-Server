namespace BDS.CollectData
{
    using System.Collections.Generic;
    /// <summary>
    /// Interface work site output
    /// </summary>
    public interface IWorkSiteOutput
    {
        /// <summary>
        /// Store resource data
        /// </summary>
        /// <param name="item">Named of data</param>
        /// <param name="data">data</param>
        /// <returns>Count of data</returns>
          int StoreResourceData(string item , List<string> data);
    }
}