using Microsoft.EntityFrameworkCore;
using Outrage.Tenancy.Definition;
using Outrage.Tenancy.Features;
using WebApplication2.Data;

namespace WebApplication2.Tenancy
{
    internal sealed class DbContextTenancyFeature : ITenancyFeature, IDbContextTenancyFeature<ReqsDbContext>, IDisposable
    {
        private ReqsDbContext? dbContext = null;
        private readonly TenancyDefinition tenantModel;

        public DbContextTenancyFeature(TenancyDefinition tenantModel)
        {
            this.tenantModel = tenantModel;
        }

        public ReqsDbContext GetDbContext()
        {
            if (dbContext == null)
            {
                dbContext = new ReqsDbContext(tenantModel);
            }

            return dbContext;
        }

        public void Dispose()
        {
            this.dbContext?.Dispose();
        }
    }
}
