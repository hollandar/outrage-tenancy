using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outrage.Tenancy.Options
{
    public sealed class HttpHostTenancyOptions
    {
        private readonly HashSet<int> _ignoredPorts = new() { 80, 443 };

        public HashSet<int> IgnoredPorts { get { return _ignoredPorts; } }

        public void IgnorePort(int port) => _ignoredPorts.Add(port);
    }
}
