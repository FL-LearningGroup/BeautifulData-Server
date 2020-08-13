using System;
using Xunit;
using BDS.DataReport;
using BDS.DataReport.Model;
using System.Threading.Tasks;

namespace BDS.DataReport.Tests
{
    public class SendOutlookEamilTests
    {
        [Fact]
        public async void SendTextContext()
        {
            EmailContext emailContext = new EmailContext(EmailContextType.Text);
            emailContext.FromPerson.Add(new ContactPerson() { Name = "BDS-CollectData", Email = "LucasYao93@outlook.com" });
            emailContext.FromPerson.Add(new ContactPerson() { Name = "BDS-CollectData01", Email = "LucasYao93@outlook.com" });
            emailContext.ToPerson.Add(new ContactPerson() { Name = "LucasYao", Email = "LucasYao93@outlook.com" });
            emailContext.ToPerson.Add(new ContactPerson() { Name = "LucasYao01", Email = "LucasYao93@outlook.com" });
            emailContext.CCPerson.Add(new ContactPerson() { Name = "LucasYaoCC", Email = "LucasYao93@outlook.com" });
            emailContext.CCPerson.Add(new ContactPerson() { Name = "LucasYaoCC01", Email = "LucasYao93@outlook.com" });
            emailContext.Subject = "test-text";
            emailContext.Message = "Test for context";

            EmailClient outlookClient = new EmailClient(EmailHostType.Outlook, "smtp-mail.outlook.com", 587, "LucasYao93@outlook.com", "yaodi@960903");
            bool result = await outlookClient.SendEamilAsync(emailContext);
            Assert.True(result);

            emailContext.Subject = "test-html";
            emailContext.Message = @"<!DOCTYPE html>
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
                                    ";
            emailContext.ContextType = EmailContextType.Html;
            result = await outlookClient.SendEamilAsync(emailContext);
            Assert.True(result);
            //EmailClient qqClient = new EmailClient(EmailHostType.QQ, "smtp.qq.com", 587, "1427019394@qq.com", "yaodi@960903");
            //result = await qqClient.SendEamilAsync(emailContext); //need setting in qq eamil.
            //Assert.True(result);
        }
    }
}
