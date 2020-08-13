using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDS.DotNetCoreKnowledage
{
    [Serializable]
    public class FYPublicInfoTitleDM01
    {
        public string DateTime { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }

        public FYPublicInfoTitleDM01(string dateTime, string url, string title)
        {
            DateTime = dateTime;
            Url = url;
            Title = title;
        }
    }
    public class UseLINQ
    {
        static async Task Main(string[] args)
        {
            Process.StartTag();
            List<FYPublicInfoTitleDM01> fyDataList = new List<FYPublicInfoTitleDM01>()
            {
                { new FYPublicInfoTitleDM01("2020-01-01", "https://www.baidu.com","test") },
                { new FYPublicInfoTitleDM01("2020-01-01", "https://www.baidu.com","test") },
                { new FYPublicInfoTitleDM01("2020-01-02", "https://www.baidu.com","test") },
                { new FYPublicInfoTitleDM01("2020-01-02", "https://www.baidu.com","test") },
                { new FYPublicInfoTitleDM01("2020-03-01", "https://www.baidu.com","test") },
                { new FYPublicInfoTitleDM01("2020-03-02", "https://www.baidu.com","test") },
                { new FYPublicInfoTitleDM01("2021-01-01", "https://www.baidu.com","test") },
                { new FYPublicInfoTitleDM01("2021-01-02", "https://www.baidu.com","test") },
                { new FYPublicInfoTitleDM01("2020-05-01", "https://www.baidu.com","test") }
            };
            var titleGroupList =
                from data in fyDataList
                group data by data.DateTime into newData
                orderby newData.Key
                select newData
                ;
            string folder = @"D:\JsonFolder\";
            string jsonFile = String.Empty;
            string dateTime = String.Empty;
            string message = String.Empty;
            foreach (var titleGroup in titleGroupList)
            {
                Console.WriteLine($"Key: {titleGroup.Key}");
                message = string.Empty;
                jsonFile = folder + "json-" + titleGroup.Key;
                // await TransferObjectToJsonFile.SerializationJsonAsync(jsonFile, titleGroup);
                dateTime = titleGroup.Key;
                foreach (var title in titleGroup)
                {
                    message += string.Format(@"
                                                <tr>
                                                <td>{0}</td>
                                                <td>{1}</td>
                                                <td><a href=""{2}"">{1}</a></td>
                                                </tr>
                                            ", title.DateTime, title.Title, title.Url);
                    //Console.WriteLine($"\t{title.DateTime}, {title.Url}, {title.Title}");
                }
                
                Console.WriteLine("DateTime: {0}\t{1}", dateTime, message);
            }
            string messageTitle = "Test-Title";
            string snetMessage = String.Format(@"<!DOCTYPE html>
                                    <html>
                                    <head>
                                    <style>
                                    table {{
                                        font-family: arial, sans-serif;
                                        border-collapse: collapse;
                                    }}

                                    td, th {{
                                        border: 1px solid #dddddd;
                                        text-align: left;
                                        padding: 8px;
                                    }}

                                    tr:nth-child(even) {{
                                        background-color: #dddddd;
                                    }}
                                    </style>
                                    </head>
                                    <body>

                                    <h2>{0}</h2>

                                    <table>
                                        <tr>
                                        <th>时间</th>
                                        <th>标题</th>
                                        <th>详细连接</th>
                                        </tr>
                                        {1}
                                    </table>
                                    </body>
                                    </html>
                                    ", messageTitle, message);
            Process.EndTag();
        }
    }
}
