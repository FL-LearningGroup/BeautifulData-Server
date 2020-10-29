using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.DotNetCoreKnowledage
{
    public class OverrideEquls
    {
        public string Name { get; set; }

        public OverrideEquls(string name)
        {
            Name = name;
        }

        public OverrideEquls(DriverClass driverClass)
        {
            Name = driverClass.Name;
        }

        public override bool Equals(Object obj)
        {
            return Name == ((OverrideEquls)obj).Name;
        }
    }

    public class DriverClass
    {
        public string Name { get; set; }

        public DriverClass(string name)
        {
            Name = name;
        }
    }
}
