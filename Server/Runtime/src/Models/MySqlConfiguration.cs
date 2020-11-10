using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Runtime.Models
{
    internal class MySqlConfiguration
    {
        public static string Host { get; set; }
        public static string DB { get; set; }
        public static string User { get; set; }
        public static string Passwd { get; set; }
    }
}
