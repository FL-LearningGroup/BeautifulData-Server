namespace BDS.CollectData {
    using BDS.CollectData.DataStructure;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design.Serialization;

    /// <summary>The collections of store resource.</summary>
    [Serializable]
    public class Resource: IResource {
        private WBData _data;
        /// <summary>
        /// Store data
        /// </summary>
        public WBData Data { get { return _data; }}

        public Resource()
        {
            _data = new WBData();
        }

        public Resource(WBData data)
        {
            _data = data;
        }
        /// <summary>
        /// Get resource data
        /// </summary>
        /// <param name="item">the named of data</param>
        /// <returns>data list</returns>
        public List<string> GetResourceData(string item)
        {
            return _data[item];
        }

        /// <summary>
        /// Store reosurce data
        /// </summary>
        /// <param name="item">The named of data</param>
        /// <param name="data">data list</param>
        /// <returns>Count data</returns>
        public int StoreResourceData(string item, List<string> data)
        {
            _data.StoreOrReplaceElementData(item, data);
            return _data[item].Count;
        }
    }
}