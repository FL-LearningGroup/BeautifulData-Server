using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BDS.Framework.Models
{
    [Serializable]
    public class MailContactPerson
    {

        public string Name { get; set; }

        [EmailAddress]
        public string Address { get; set; }

        public MailContactPerson(string name, string address)
        {
            Name = name;
            Address = address;
        }
    }
}
