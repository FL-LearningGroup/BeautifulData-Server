using System;
using Xunit;
using BDS.DataFactory;
using System.IO;
using System.Text;
using System.Linq;

namespace BDS.DataFactory.Tests
{
    public class OperateGitTests
    {
        private static Random random = new Random();
        [Fact]
        public void PushChangeTest()
        {
            GitFactory gitFactory = new GitFactory(@"D:\BDS-DataFactory", @"https://github.com/LucasYao93-DataBase/BDS-Data-FuYang.git", "LucasYao93-DataBase", "yaodi@960903", "LucasYao93@outlook.com");
            Assert.True(gitFactory.PullRepository());
            Assert.True(Directory.Exists(@"D:\BDS-DataFactory"));
            
            var rand = new Random();
            var bytes = new byte[5];
            rand.NextBytes(bytes);
            string filePath = @"D:\BDS-DataFactory\test-" + RandomString(6) + ".txt";
            using (FileStream fs = File.Create(filePath))
            {
                AddText(fs, "This is some text");
                AddText(fs, "This is some more text,");
                AddText(fs, "\r\nand this is on a new line");
                AddText(fs, "\r\n\r\nThe following is a subset of characters:\r\n");
                fs.Close();
            }
            var changeCount = gitFactory.AddStageChanges();
            Assert.Equal(1, changeCount);
            gitFactory.CommitChanges("Add file for test.");
            Assert.True(gitFactory.PushRepository());
            
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
    }
}
