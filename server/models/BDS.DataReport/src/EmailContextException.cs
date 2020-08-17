namespace BDS.DataReport
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    [Serializable]
    public class EmailContextException: Exception
    {
        private string _context;
        private string _exMessage;

        public EmailContextException()
        {

        }
        public EmailContextException(string context, string message): base(String.Format("Email context failed[:]{0}[:]{1}", context, message))
        {
            _context = context;
            _exMessage = message;
        }
    }
}
