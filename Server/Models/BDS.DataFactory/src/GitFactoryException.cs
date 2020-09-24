using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.DataFactory
{
    public class GitFactoryException: Exception
    {
        private string _context;
        private string _exMessage;
        public string Context { get { return _context; } }
        public string ExMessage { get { return _exMessage; } }

        public GitFactoryException()
        {

        }
        public GitFactoryException(string context, string message): base(String.Format("Git Factory[:]<context>{0}[:]<message>{1}", context, message))
        {
            _context = context;
            _exMessage = message;
        }

    }
}
