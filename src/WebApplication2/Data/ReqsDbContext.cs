using Microsoft.EntityFrameworkCore;
using Outrage.Tenancy;
using Outrage.Tenancy.Definition;

namespace WebApplication2.Data
{
    public class ReqsDbContext : DbContext
    {
        private readonly TenancyDefinition tenantDefinition;

        public ReqsDbContext(TenancyDefinition tenantDefinition) : base()
        {
            this.tenantDefinition = tenantDefinition;
        }

        protected override async void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(tenantDefinition.Store.GetValue<string>("ConnectionString"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
