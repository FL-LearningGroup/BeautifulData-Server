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
        public string DataTime { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }

        public FYPublicInfoTitleDM01(string dataTime, string url, string title)
        {
            DataTime = dataTime;
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
            await TransferObjectToJsonFile.SerializationJsonAsync(fyDataList);
            var result =
                from data in fyDataList
                group data by data.DataTime into newData
                orderby newData.Key
                select newData
                ;
            foreach (var nameGroup in result)
            {
                Console.WriteLine($"Key: {nameGroup.Key}");
                await TransferObjectToJsonFile.SerializationJsonAsync(nameGroup);
                foreach (var info in nameGroup)
                {
                    Console.WriteLine($"\t{info.DataTime}, {info.Url}, {info.Title}");
                }
            }
            Process.EndTag();
        }
    }
}
