using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outrage.Tenancy.Definition
{
    public sealed class TenancyDefinition: IDisposable
    {
        private readonly FeatureCollection features = new();

        public TenancyDefinition(TenantId tenantId) {
            TenantId = tenantId;
            var random = new Random();
            Iv = new byte[16];
            random.NextBytes(Iv);
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public TenantId TenantId { get; set; }
        public byte[] Key { get; set; }
        public byte[] Iv { get; set; }
        public TenancyStore Store { get; set; }

        public FeatureCollection Features => this.features;

        public void Dispose()
        {
            features?.Dispose();
        }
    }
}
