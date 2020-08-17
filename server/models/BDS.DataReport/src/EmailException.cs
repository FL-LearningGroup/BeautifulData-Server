namespace BDS.DataReport
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    /// <summary>
    /// The exception that is throw hen there send email failed.
    /// </summary>
    [Serializable]
    public class EmailException: Exception
    {
        /// <summary>
        /// Represent who caused email exception.
        /// </summary>
        private string _context;
        /// <summary>
        /// Exception message
        /// </summary>
        private string _exMessage;

        /// <summary>
        /// Represent who caused email exception.
        /// </summary>
        public string Context { get { return _context; } }
        /// <summary>
        /// Exception message
        /// </summary>
        public string ExMessage { get { return _exMessage; } }
        public EmailException()
        {

        }
        public EmailException(string context, string message): base(String.Format("Send email failed[:]{0}[:]{1}", context, message))
        {
            _context = context;
            _exMessage = message;
        }

    }
}
