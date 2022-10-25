using Microsoft.EntityFrameworkCore;

namespace Outrage.Tenancy.Features
{
    public interface IDbContextTenancyFeature<TDbContext>: ITenancyFeature where TDbContext : DbContext
    {
        TDbContext GetDbContext();
    }
}
