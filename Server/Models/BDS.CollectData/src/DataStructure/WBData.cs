using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BDS.CollectData.DataStructure
{
    /// <summary>
    /// Collection data of web page.
    /// </summary>
    [Obsolete("Not used any more", true)]
    [Serializable]
    public class WBData : IWBData
    {
        /// <summary>
        /// Storage collect data.
        /// </summary>
        private Dictionary<string, List<string>> _data;
        public Dictionary<string, List<string>> Data
        {
            get { return _data; }
            set { _data = value; }
        }
        public List<string> this[string element]
        {
            get
            {
                return _data[element];
            }
        }

        public WBData()
        {
            Data = new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// Store or replace data of element
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">data</param>
        /// <returns>Count of element data count</returns>
        public int StoreOrReplaceElementData(string element, List<string> data)
        {
            if (_data.Keys.Contains(element))
            {
                _data.Remove(element);
            }
            _data.Add(element, data);
            return _data.Count;
        }
        /// <summary>
        /// Get data by element name.
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>web page element data list</returns>
        public List<string> GeElementDataName(string element)
        {

            return _data[element];
        }

        /// <summary>
        /// Add data of element of web page.
        /// </summary>
        /// <param name="element">element</param>
        /// <param name="data">data</param>
        /// <returns>Count of element data count</returns>
        public int AddElementData(string element, string data)
        {
            _data[element].Add(data);
            return _data[element].Count;
        }
    }
}
