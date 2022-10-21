using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outrage.Tenancy.Data
{
    public sealed class TenancyDbContext: DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantValue> Values { get; set; }

        public TenancyDbContext(DbContextOptions options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>().HasKey(r => r.Id);
            modelBuilder.Entity<TenantValue>().HasKey(r => r.Id);
            modelBuilder.Entity<TenantValue>().HasOne(r => r.Tenant).WithMany(r => r.Values).HasForeignKey(r => r.TenantId);
            modelBuilder.Entity<TenantValue>().HasIndex(r => new { r.TenantId, r.Key });

            base.OnModelCreating(modelBuilder);
        }
    }
}
