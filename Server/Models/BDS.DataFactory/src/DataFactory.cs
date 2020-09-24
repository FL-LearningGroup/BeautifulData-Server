using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BDS.DataFactory
{
    public class DataFactory
    {
        private ConnectFactory _connectInfo;
        public ConnectFactory ConnectInfo { get { return _connectInfo; } }

        public IDataFactory factory;
        public DataFactory(FactoryType type, string host, string? port, string? user, string? password, Dictionary<string, string>? config)
        {
            _connectInfo = new ConnectFactory(host, port, user, password);
            _connectInfo.config = config;
            switch (type)
            {
                case FactoryType.JsonFile:
                    factory = new LocalFile(_connectInfo);
                    break;
                default:
                    break;

            }

        }
    }
}
