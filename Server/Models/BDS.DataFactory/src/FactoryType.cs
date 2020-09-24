using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.DataFactory
{
    public enum FactoryType
    {
        JsonFile = 1,
        GitHub,
        /// <summary>
        /// Use Mysql 8.0 or greater. Because It support SQL and NOSQL.
        /// </summary>
        MySql,
        CloudMySql
    }
}
