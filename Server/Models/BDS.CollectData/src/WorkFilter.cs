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
        virtual public string TagName { get; set; }

        /// <summary>
        /// Tag id of web page
        /// </summary>
        virtual public string TagId { get; set; }

        /// <summary>
        /// Tag class of web page
        /// </summary>
        virtual public string TagClass { get; set; }
    }
}
