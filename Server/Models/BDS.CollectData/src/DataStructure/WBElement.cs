using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.CollectData.DataStructure
{
    /// <summary>
    /// The elemenet of web page
    /// </summary>
    /// <typeparam name="T">Generic T</typeparam>
    [Obsolete("Not used any more", true)]
    public class WBElement
    {
        //private int? _categoryId;
        public string _name;
        //public int? CategoryId { get { return _categoryId; } set { _categoryId = value; } }
        public string Name { get { return _name; }  set { _name = value; } }
        public WBElement(string name)
        {
            _name = name;

        }
    }
}
