using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenancy.Models
{
    public sealed class TenantId
    {
        private readonly string id;

        public TenantId(string id)
        {
            this.id = id;
        }

        public string AsString { get { return this.id; } }
    }
}
