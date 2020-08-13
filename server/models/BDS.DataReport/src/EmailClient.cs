namespace BDS.DataReport
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using BDS.DataReport.Model;
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using MimeKit;
    using MimeKit.Text;

    public class EmailClient: IEmailClient
    {
        private string _host;
        private System.Int32 _port;
        private string _userName;
        private string _password;
        private EmailHostType _hostType;

        public EmailClient(EmailHostType hostType, string host, System.Int32 port, string userName, string password)
        {
            _hostType = hostType;
            _host = host;
            _port = port;
            _userName = userName;
            _password = password;
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
        public async Task<bool> SendEamilAsync(EmailContext emailContext)
        {
            MimeMessage email = new MimeMessage();
            foreach(ContactPerson person in emailContext.FromPerson)
            {
                email.From.Add(new MailboxAddress(person.Name, person.Email));
            }
            foreach (ContactPerson person in emailContext.ToPerson)
            {
                email.To.Add(new MailboxAddress(person.Name, person.Email));
            }
            foreach (ContactPerson person in emailContext.CCPerson)
            {
                email.Cc.Add(new MailboxAddress(person.Name, person.Email));
            }
            email.Subject = emailContext.Subject;
            email.Body = emailContext.Body;
            try
            {
                using (SmtpClient client = new SmtpClient())
                {
                    if (_hostType == EmailHostType.Outlook)
                    {
                        client.Connect(_host, _port, SecureSocketOptions.StartTls);
                    }
                    if (_hostType == EmailHostType.QQ)
                    {
                        client.Connect(_host, _port, false);
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
        public void SendEamil(EmailContext emailContext)
        {
            SendEamilAsync(emailContext).Wait();
        }
    }
}
