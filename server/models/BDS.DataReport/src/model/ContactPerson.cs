
namespace BDS.DataReport.model
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    [Serializable]
    public class ContactPerson
    {
        private string _name;
        private string _email;
        private string _phone;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
            }
        }
    }
}
