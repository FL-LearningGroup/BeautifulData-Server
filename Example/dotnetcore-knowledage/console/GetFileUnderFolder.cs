using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BDS.DotNetCoreKnowledage
{
    class GetFileUnderFolder
    {
        static void GetFile()
        {
            string path = @"D:\Lucas\git\BeautifulData-Server\server\pipeline";
            string[] filePaths = Directory.GetFiles(path, "BDS.Pipeline*.dll", SearchOption.AllDirectories);
            var tmp = 10;
        }
        static void Main_Stop()
        {
            Process.StartTag();
            GetFile();
            Process.EndTag();
        }
    }
}
