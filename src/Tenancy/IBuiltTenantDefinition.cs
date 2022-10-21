using Outrage.Tenancy.Models;

namespace Outrage.Tenancy
{
    public interface IBuiltTenantDefinition
    {
           TenantModel Definition { get; }
    }
}
