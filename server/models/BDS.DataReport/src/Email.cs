namespace BDS.DataReport
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using BDS.DataReport.model;
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using MimeKit;
    using MimeKit.Text;

    public class Email: IEmail
    {
        private ContactPerson _contactPerson;
        private string _host;
        private System.Int32 _port;
        private string _userName;
        private string _password;
        private EmailMessageFormat _messageType;
        private string _subject;
        private string _message;
        private TextPart _body;
        private EmailHostType _hostType;

        public Email(EmailHostType hostType, string host, System.Int32 port, string userName, string password, EmailMessageFormat messageType)
        {
            _hostType = hostType;
            _host = host;
            _port = port;
            _userName = userName;
            _password = password;
            _messageType = messageType;
        }

        void initialize()
        {
            if(_messageType == EmailMessageFormat.Html)
            {
                _body = ConvertMessageToHtmlFormat();
            }
            if(_messageType == EmailMessageFormat.Text)
            {
                _body = ConvertMessageToTextFormat();
            }

        }
        public ContactPerson PersonlList
        {
            get
            {
                return _contactPerson;
            }
            set
            {
                _contactPerson = value;
            }
        }
        public EmailHostType HostType
        {
            get
            {
                return _hostType;
            }
            set
            {
                _hostType = value;
            }
        }
        public String Host
        {
            get
            {
                return _host;
            }
            set
            {
                _host = value;
            }
        }
        public System.Int32 Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }
        public EmailMessageFormat MessageType
        {
            get
            {
                return _messageType;
            }
            set
            {
                if (_messageType == EmailMessageFormat.Html)
                {
                    _body = ConvertMessageToHtmlFormat();
                }
                if (_messageType == EmailMessageFormat.Text)
                {
                    _body = ConvertMessageToTextFormat();
                }
                _messageType = value;
            }
        }
        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                _subject = value;
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
            }
        }
        private TextPart ConvertMessageToHtmlFormat()
        {
            TextPart body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = _message
            };
            return body;
        }
        private TextPart ConvertMessageToTextFormat()
        {
            TextPart body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = _message
            };
            return body;
        }
        public async Task<bool> SendEamilAsync(List<ContactPerson> fromPersonList, List<ContactPerson> toPersonList, List<ContactPerson> ccPersonList, string message = null)
        {
            MimeMessage email = new MimeMessage();
            foreach(ContactPerson person in fromPersonList)
            {
                email.From.Add(new MailboxAddress(person.Name, person.Email));
            }
            foreach (ContactPerson person in toPersonList)
            {
                email.To.Add(new MailboxAddress(person.Name, person.Email));
            }

            email.Subject = _subject;
            email.Body = _body;
            try
            {
                using (SmtpClient client = new SmtpClient())
                {
                    if (_hostType == EmailHostType.Outlook)
                    {
                        client.Connect(_host, _port, SecureSocketOptions.StartTls);
                    }
                    client.Authenticate(_userName, _password);

                    await client.SendAsync(email);
                    client.Disconnect(true);
                }
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }
        public void SendEamil(List<ContactPerson> fromPersonList, List<ContactPerson> toPersonList, List<ContactPerson> ccPersonList, string message = null)
        {
            SendEamilAsync(fromPersonList, toPersonList, ccPersonList, message).Wait();
        }
    }
}
