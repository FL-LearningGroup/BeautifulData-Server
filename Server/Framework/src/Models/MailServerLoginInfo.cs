using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BDS.Framework.Models
{
    public class MailServerLoginInfo
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool SecureSocketOptions { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ICredentials Credentials { get; set; }
        public Encoding MaileEncoding { get; set; }
    }
}
