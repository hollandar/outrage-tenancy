using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outrage.Tenancy.Models
{
    public sealed class TenantModel
    {
        public TenantModel(TenantId tenantId) {
            TenantId = tenantId;
            var random = new Random();
            Iv = new byte[16];
            random.NextBytes(Iv);
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public TenantId TenantId { get; set; }
        public byte[] Key { get; set; }
        public byte[] Iv { get; set; }

    }
}
