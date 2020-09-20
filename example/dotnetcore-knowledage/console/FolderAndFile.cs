using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BDS.DotNetCoreKnowledage
{
    public class FolderAndFile: IProcess
    {
        public void InvokeMain()
        {
            string path = @"c:\prorgam file\test\txt.1";
            string folder = path.Substring(0, path.LastIndexOf(Path.DirectorySeparatorChar));
            Console.WriteLine(folder);

        }
    }
}
