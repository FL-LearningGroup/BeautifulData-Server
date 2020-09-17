namespace BDS.CollectData
{
    using System.Collections.Generic;
    /// <summary>
    /// Interface work site inpout
    /// </summary>
    public interface IWorkSiteInput
    {
        /// <summary>
        /// Get data from resource
        /// </summary>
        /// <param name="item">Nanmed of data</param>
        /// <returns>Data list</returns>
         List<string> GetResourceData(string item);
    }
}