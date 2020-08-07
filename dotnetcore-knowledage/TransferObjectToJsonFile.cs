using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BDS.DotNetCoreKnowledage
{
    [Serializable]
    public class FYPublicInfoTitleDM
    {
        private string _dataTime;
        private string _url;
        private string _title;
        public string DataTime
        {
            get
            {
                return _dataTime;
            }
            set
            {
                _dataTime = value;
            }
        }
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }
        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
            }
        }
        public FYPublicInfoTitleDM(string dataTime, string url, string title)
        {
            _dataTime = dataTime;
            _url = url;
            _title = title;
        }
    }
    public class TransferObjectToJsonFile
    {
        static List<FYPublicInfoTitleDM> dmList = new List<FYPublicInfoTitleDM>() {
            {new FYPublicInfoTitleDM("2020-08-01", "https://github.com/LucasYao93/azure-powershell.git", "测试") },
            {new FYPublicInfoTitleDM("2020-10-01", "https://github.com/LucasYao93/azure-powershell.git", "测试") }
        };
        static void SerializationJson()
        {
            //Json Serialization
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            options.Encoder = JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All);

            var jsonString = JsonSerializer.Serialize(dmList, options);
            //Write json file by byte
            //jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(fyDm, options);
            string jsonPath = @"D:\Serialization.json";
            if (!File.Exists(jsonPath))
            {
                File.Create(jsonPath);
            }
            
            File.WriteAllText(jsonPath, jsonString);


        }

        static void Main_Stop(string[] args)
        {
            Console.WriteLine("Start process");
            SerializationJsonAsync();
            Console.WriteLine("Please enter key to end the process");
            Console.ReadKey();
        }
        static async Task SerializationJsonAsync()
        {
            string json = string.Empty;
            string jsonPath = @"D:\SerializationAsync.json";
            using (var stream = new MemoryStream())
            {
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                options.Encoder = JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All);

                if (!File.Exists(jsonPath))
                {
                    using var file = File.Create(jsonPath);
                }
                await JsonSerializer.SerializeAsync(stream, dmList, dmList.GetType(), options);
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    await File.WriteAllTextAsync(jsonPath, await reader.ReadToEndAsync());
                }

            }
        }
    }
}
