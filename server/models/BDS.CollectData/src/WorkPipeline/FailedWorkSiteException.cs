using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.CollectData
{
    [Serializable]
    public class FailedWorkSiteException: Exception
    {
        public FailedWorkSiteException()
        {

        }
        public FailedWorkSiteException(string id, string name, string message)
            : base(String.Format("Work Site failed: id-{0}, name-{1}, message-{2}", id, name, message))
        {

        }
    }
}
