using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.CollectData
{
    /// <summary>
    /// Filter condition
    /// </summary>
    [Serializable]
    public class WorkFilter: IWorkFilter
    {
        /// <summary>
        /// Tag name of web page
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// Tag id of web page
        /// </summary>
        public string TagId { get; set; }

        /// <summary>
        /// Tag class of web page
        /// </summary>
        public string TagClass { get; set; }
    }
}
