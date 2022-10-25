using Outrage.Tenancy.Definition;

namespace Outrage.Tenancy
{
    public interface IBuiltTenantDefinition:IDisposable
    {
        TenancyDefinition Definition { get; }
    }
}
