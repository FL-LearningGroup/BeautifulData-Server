namespace BDS.CollectData.BDSException
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [Serializable]
    public class WorkSiteException: Exception
    {
        private string _context;
        private string _id;
        private string _name;
        private string _exMessage;

        public string Context { get { return _context; } }
        public string Id { get { return _id; } }
        public string Name { get { return _name; } }
        public string ExMessage { get { return _exMessage; } }

        public WorkSiteException()
        {

        }
        public WorkSiteException(string context, string id, string name, string message)
            : base(String.Format("Work Site failed[:]<context>{0}[:]<id>{1}[:]<name>{2}[:]<message>{3}", context, id, name, message))
        {
            _context = context;
            _id = id;
            _name = name;
            _exMessage = message;
        }
    }
}
