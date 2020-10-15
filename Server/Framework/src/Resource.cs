using BDS.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace BDS.Framework
{
    public abstract class Resource<T> :IWorkSiteInput, IWorkSiteOutput
    {
        public abstract List<T> Data { get; }
        public abstract List<string> TransferResourceDataToWorkSiteData();
        public abstract void TransferWorkSiteDataToResourceData(List<string> data);
        public MailServerLoginInfo mailServerLoginInfo;
        public MailInfo mailInfo;
        public MailBodyType mailBodyType;
    }
}
