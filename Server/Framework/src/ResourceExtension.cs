using BDS.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MimeKit;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Reflection;
using System.Threading.Tasks;

namespace BDS.Framework
{
    public static class ResourceExtension
    {
        public static async Task SendEmailAsync<T>(this Resource<T> resource, MailServerLoginInfo mailServerLoginInfo, MailInfo mailInfo, MailBodyType mailBodyType = MailBodyType.Text)
        {
            StringBuilder messageBody;
            StringBuilder header = new StringBuilder(60);
            StringBuilder message = new StringBuilder(100);
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            List<string> propertiesName = (
                        from propertyInfo in propertyInfos
                        select propertyInfo.Name
                    ).ToList()
                    ;
            List<T> data = resource.Data;

            if (mailBodyType == MailBodyType.HTMLTable)
            {
                messageBody = new StringBuilder(MailBodyFormat.HTMLTable.Length, MailBodyFormat.MaxMailMessageSize);
                messageBody.Append(MailBodyFormat.HTMLTable);
                foreach (string propertyName in propertiesName)
                {
                    header.Append("<th>" + propertyName + "</th>");
                }
                foreach (T item in data)
                {
                    string row = @"<tr>";
                    string rowCells = String.Empty;
                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        var value = propertyInfo.GetValue(item, null);
                        rowCells += "<td>" + value + @"</td>";
                    }
                    row += rowCells + @"</tr>";
                    message.Append(row);
                }
                messageBody.Replace(MailBodyFormat.Header, header.ToString());
                messageBody.Replace(MailBodyFormat.Message, message.ToString());
            }
            else if(mailBodyType == MailBodyType.Text)
            {
                messageBody = new StringBuilder(100, MailBodyFormat.MaxMailMessageSize);
                string txt = String.Empty;
                foreach (T item in data)
                {
                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        var value = propertyInfo.GetValue(item, null);
                        txt += value;
                    }
                }
                messageBody.Append(txt);
            }
            else
            {
                messageBody = new StringBuilder();
                messageBody.Append("None");
            }
            MimeMessage emaliMessage = new MimeMessage();
            foreach (MailContactPerson mailContactPerson in mailInfo.FromPerson)
            {
                emaliMessage.From.Add(new MailboxAddress(mailContactPerson.Name, mailContactPerson.Address));
            }
            foreach (MailContactPerson mailContactPerson in mailInfo.ToPerson)
            {
                emaliMessage.To.Add(new MailboxAddress(mailContactPerson.Name, mailContactPerson.Address));
            }
            if (mailInfo.CCPerson != null)
            foreach (MailContactPerson mailContactPerson in mailInfo.CCPerson)
            {
                emaliMessage.Cc.Add(new MailboxAddress(mailContactPerson.Name, mailContactPerson.Address));
            }
            emaliMessage.Subject = mailInfo.Subject;
            if (mailBodyType == MailBodyType.HTMLTable)
            {
                emaliMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = messageBody.ToString()
                };
            }
            using (var client = new SmtpClient())
            {
                if (mailServerLoginInfo.SecureSocketOptions)
                {
                    client.Connect(mailServerLoginInfo.Host, mailServerLoginInfo.Port, SecureSocketOptions.StartTls);
                }
                else
                {
                    client.Connect(mailServerLoginInfo.Host, mailServerLoginInfo.Port);
                }

                // Note: only needed if the SMTP server requires authentication
                // Warning: The password not been enrption.
                client.Authenticate(mailServerLoginInfo.UserName, mailServerLoginInfo.Password);

                await client.SendAsync(emaliMessage);
                client.Disconnect(true);
            }
        }
    }
}
