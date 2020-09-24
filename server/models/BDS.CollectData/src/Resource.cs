namespace BDS.CollectData {
    using BDS.CollectData.DataStructure;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design.Serialization;
    using System.Linq;

    /// <summary>The collections of store resource.</summary>
    [Serializable]
    public class Resource: IResource {
        private Dictionary<string, List<string>> _data;
        /// <summary>
        /// Store data
        /// </summary>
        virtual public Dictionary<string, List<string>> Data { get { return _data; } set { _data = value; } }

        public Resource()
        {
            _data = new Dictionary<string, List<string>>();
        }

        public Resource(Dictionary<string, List<string>> data)
        {
            _data = data;
        }
        /// <summary>
        /// Get resource data
        /// </summary>
        /// <param name="item">the named of data</param>
        /// <returns>data list</returns>
        virtual public List<string> GetResourceData(string item)
        {
            return Data[item];
        }

        /// <summary>
        /// Store reosurce data
        /// </summary>
        /// <param name="item">The named of data</param>
        /// <param name="data">data list</param>
        /// <returns>Count data</returns>
        virtual public int StoreResourceData(string item, List<string> data)
        {
            if (Data.Keys.Contains(item))
            {
                Data.Remove(item);
            }
            Data.Add(item, data);
            return Data.Count;
        }
    }
}