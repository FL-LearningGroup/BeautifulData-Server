using System;
using System.Collections.Generic;
using System.Text;
using MimeKit;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Threading.Tasks;

namespace BDS.DotNetCoreKnowledage
{
    public class SendEmail
    {
        static async Task SendEmailAsync()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("BDS-CollectData", "LucasYao93@outlook.com"));
            message.To.Add(new MailboxAddress("LucasYao", "LucasYao93@outlook.com"));
            message.Subject = "BDS-FuYangNew";

            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = @"<!DOCTYPE html>
                        <html>
                        <head>
                        <style>
                        table {
                            font-family: arial, sans-serif;
                            border-collapse: collapse;
                        }

                        td, th {
                            border: 1px solid #dddddd;
                            text-align: left;
                            padding: 8px;
                        }

                        tr:nth-child(even) {
                            background-color: #dddddd;
                        }
                        </style>
                        </head>
                        <body>

                        <h2>Fu Yang public infomation:</h2>

                        <table>
                            <tr>
                            <th>时间</th>
                            <th>标题</th>
                            <th>详细连接</th>
                            </tr>
                            <tr>
                            <td>2020/07/08</td>
                            <td>人事调整</td>
                            <td><a href=""https://www.runoob.com/"">访问菜鸟教程</a></td>
                            </tr>
                        </table>

                        </body>
                        </html>
                        "
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTls);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("LucasYao93@outlook.com", "yaodi@960903");

                await client.SendAsync(message);
                client.Disconnect(true);
            }
        }
		static async Task Main_Stop(string[] args)
		{
            Process.StartTag();
            await SendEmailAsync();
            Process.EndTag();
		}
	}
}
