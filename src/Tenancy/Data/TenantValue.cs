using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenancy.Data
{
    public enum ValueEncodingEnum: int { UTF8, UTF8Crypt }
    public class TenantValue
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TenantId { get; set; }
        public string Key { get; set; } = string.Empty;
        public byte[] Value { get; set; } = Array.Empty<byte>();
        public ValueEncodingEnum Encoding { get; set; } = ValueEncodingEnum.UTF8;

        public virtual Tenant Tenant { get; set; }
    }
}
