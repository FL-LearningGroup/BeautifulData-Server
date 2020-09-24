using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BDS.DataFactory
{
    /// <summary>
    /// Interface of data factory
    /// </summary>
    public interface IDataFactory
    {
        public Task<String> SelectDataAsync(string query);
        public string SelectData(string query);
        public Task InsertDataAsync(string data);
        public void InsertData(string data);
        public Task DeleteDataAsync(string query);
        public void DeleteData(string query);

        public Task UpdateDataAsync(string query, string data);
        public void UpdateData(string query, string data);
    }
}
