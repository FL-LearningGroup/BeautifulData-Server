namespace BDS.CollectData.BDSException
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [Serializable]
    public class WorkSiteException: Exception
    {
        public WorkSiteException()
        {

        }
        public WorkSiteException(string id, string name, string message)
            : base(String.Format("Work Site failed: id-{0}, name-{1}, message-{2}", id, name, message))
        {

        }
    }
}
