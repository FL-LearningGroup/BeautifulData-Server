using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.CollectData.DataStructure
{
    /// <summary>
    /// The data of element of web page.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Obsolete("Not used any more", true)]
    public class WBElementData
    {
        private List<string> _data;
        public List<string> Data { get { return _data; }  set { _data = value; } }

        public WBElementData()
        {
            _data = new List<string>();
        }

        public WBElementData(List<string> data)
        {
            _data = data;
        }

    }
}
