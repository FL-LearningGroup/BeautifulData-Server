using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Framework.Models
{
    public class MailInfo
    {
        public string Subject { get; set; }
        public List<MailContactPerson> FromPerson { get; set; }

        public List<MailContactPerson> ToPerson { get; set; }

        #nullable enable
        public List<MailContactPerson>? CCPerson { get; set; }
        #nullable disable

        public MailInfo(string subject, List<MailContactPerson> fromPerson, List<MailContactPerson> toPerson) 
        {
            Subject = subject;
            FromPerson = fromPerson;
            ToPerson = toPerson;
        }
    }
}
