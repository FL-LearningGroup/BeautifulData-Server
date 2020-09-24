using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.DataFactory
{
    [Serializable]
    public class ConnectFactory
    {
        private string _host;
        private string? _port;
        private string? _user;
        private string? _password;
        public string Host { get { return _host; } }
        public string Port { get { return _port; } }
        public string User { get { return _user; } }
        public string Password { get { return _password; } }
        public ConnectFactory(string host, string? port, string? user, string? password)
        {
            _host = host;
            _port = port;
            _user = user;
            _password = password;
        }
        public Dictionary<string, string>? config { get; set; }
    }
}
