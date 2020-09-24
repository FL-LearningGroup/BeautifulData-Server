using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace BDS.DataFactory
{
    /// <summary>
    /// Json file operation.
    /// </summary>
    internal class LocalFile: IDataFactory
    {
        private ConnectFactory _connectFactory;
        public LocalFile(ConnectFactory connectFactory)
        {
            _connectFactory = connectFactory;
            CheckAndCreatePath();
        }
        private void CheckAndCreatePath()
        {
            string folder = _connectFactory.Host.Substring(0, _connectFactory.Host.LastIndexOf(Path.DirectorySeparatorChar));
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            if (!File.Exists(_connectFactory.Host))
            {
                File.Create(_connectFactory.Host);    
            }
        }
        public async Task<string> SelectDataAsync(string query)
        {
            return await File.ReadAllTextAsync(_connectFactory.Host);
        }
        public string SelectData(string query)
        {
            return File.ReadAllText(_connectFactory.Host);
        }
        public async Task InsertDataAsync(string data)
        {
            using (FileStream fs = File.OpenWrite(_connectFactory.Host))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(data);
                await fs.WriteAsync(info, 0, info.Length);
            }
        }
        public void InsertData(string data)
        {
            InsertDataAsync(data).Wait();
        }

        [Obsolete("No use any anywhere", true)]
        public async Task DeleteDataAsync(string query)
        {
            await Task.Delay(2000);
        }

        [Obsolete("No use any anywhere", true)]
        public void DeleteData(string query)
        {
        }

        [Obsolete("No use any anywhere", true)]
        public async Task UpdateDataAsync(string query, string data)
        {
            await Task.Delay(2000);
        }

        [Obsolete("No use any anywhere", true)]
        public void UpdateData(string query, string data)
        {
        }
    }
}
