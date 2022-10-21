using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outrage.Tenancy.Data
{
    public class Tenant
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TenantId { get; set; }
        public byte[] Iv { get; set; } 

        public virtual ICollection<TenantValue> Values { get; set; }
    }
}
