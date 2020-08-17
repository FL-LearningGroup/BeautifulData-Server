using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.DataFactory
{
    [Serializable]
    public class FileFactoryException: Exception
    {
        private string _context;
        private string _exMessage;
        public string Context { get { return _context; } }
        public string ExMessage { get { return _exMessage; } }

        public FileFactoryException()
        {

        }
        public FileFactoryException(string context, string message): base(String.Format("Transfer data to file failed[:]<context>{0}[:]<message>{1}", context, message))
        {
            _context = context;
            _exMessage = message;
        }
    }
}
