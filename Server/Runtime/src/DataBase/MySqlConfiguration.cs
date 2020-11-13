using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Runtime.DataBase
{
    /// <summary>
    /// Define the parameters for connecting to mysql.
    /// </summary>
    internal class MySqlConfiguration
    {
        public static string Host { get; set; }
        public static string DB { get; set; }
        public static string User { get; set; }
        public static string Passwd { get; set; }
    }
}
