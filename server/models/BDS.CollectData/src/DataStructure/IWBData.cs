using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.CollectData.DataStructure
{
    [Obsolete("Not used any more", true)]
    public interface IWBData
    {
        /// <summary>
        /// Store data of element of web page.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="data"></param>
        /// <returns>Count of element</returns>
        public int StoreOrReplaceElementData(string element, List<string> data);
        /// <summary>
        /// Get data of element of web page.
        /// </summary>
        /// <param name="element">Element of web page</param>
        /// <returns>Data of element</returns>
        public List<string> GeElementDataName(string element);

        /// <summary>
        /// Add data of element of web page.
        /// </summary>
        /// <param name="element">element</param>
        /// <param name="data">data</param>
        /// <returns></returns>
        public int AddElementData(string element, string data);
    }
}
