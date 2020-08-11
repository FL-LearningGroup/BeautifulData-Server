

namespace BDS.DataReport
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BDS.DataReport.model;
    using MimeKit;

    [Serializable]
    public class EmailContext
    {
        private List<ContactPerson> _fromPerson = new List<ContactPerson>();
        private List<ContactPerson> _toPerson = new List<ContactPerson>();
        private List<ContactPerson> _ccPerson = new List<ContactPerson>();
        private string _subject;
        private string _message;
        private TextPart _body = new TextPart();
        private EmailContextType _contextType = EmailContextType.Text;
        public EmailContext(EmailContextType contextType)
        {

        }
        public EmailContext(List<ContactPerson> fromPerson, List<ContactPerson> toPerson, EmailContextType contextType)
        {
            _fromPerson = fromPerson;
            _toPerson = toPerson;
            _contextType = contextType;
        }
        public EmailContext(List<ContactPerson> fromPerson, List<ContactPerson> toPerson, EmailContextType contextType, List<ContactPerson> ccPerson, string subject, string message)
        {
            _fromPerson = fromPerson;
            _toPerson = toPerson;
            _contextType = contextType;
            _ccPerson = ccPerson;
            _subject = subject;
            _message = message;
            ConvertMessageToBody();
        }
        public List<ContactPerson> FromPerson
        {
            get { return _fromPerson; }
            set { _fromPerson = value; }
        }
        public List<ContactPerson> ToPerson
        {
            get { return _toPerson; }
            set { _toPerson = value; }
        }
        public List<ContactPerson> CCPerson
        {
            get { return _ccPerson; }
            set { _ccPerson = value; }
        }
        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }
        public string Message
        {
            get { return _message; }
            set
            { 
                _message = value; 
                ConvertMessageToBody();
            }
        }

        public TextPart Body
        {
            get { return _body; }
        }
        public EmailContextType ContextType
        {
            get { return _contextType; }
            set { _contextType = value; ConvertMessageToBody(); }
        }

        private void ConvertMessageToBody()
        {
            if (_contextType == EmailContextType.Html)
            {
                _body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = _message
                };
            }
            if (_contextType == EmailContextType.Text)
            {
                _body = new TextPart(MimeKit.Text.TextFormat.Text)
                {
                    Text = _message
                };
            }
        }

    }
}
