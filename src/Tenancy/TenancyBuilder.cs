using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Outrage.Tenancy.Data;
using Outrage.Tenancy.Definition;
using Outrage.Tenancy.Options;
using Outrage.Tenancy.Providers;
using Outrage.Tenancy.Features;

namespace Outrage.Tenancy
{
    internal sealed class TenancyBuilder : ITenancyService
    {
        static HashSet<string> migratedTenancies = new HashSet<string>();
        
        private readonly ITenancyIdProvider? tenancyIdProvider;
        private readonly TenancyOptions tenancyOptions;
        private readonly ITenantBuilder tenancyBuilder;
        private readonly TenancyDbContext tenancyDbContext;
        private readonly Dictionary<string, IBuiltTenantDefinition> builtDefinitions = new();

        public TenancyBuilder(IServiceProvider serviceProvider)
        {
            this.tenancyIdProvider = serviceProvider.GetService<ITenancyIdProvider>();
            this.tenancyOptions = serviceProvider.GetService<TenancyOptions>() ?? new();
            this.tenancyBuilder = serviceProvider.GetService<ITenantBuilder>() ?? new DefaultTenantBuilder(serviceProvider);
            this.tenancyDbContext = serviceProvider.GetRequiredService<TenancyDbContext>();
        }

        public async Task<TBuiltTenantDefinition> GetTenantAsync<TBuiltTenantDefinition>() where TBuiltTenantDefinition : IBuiltTenantDefinition
        {
            var builtDefinition = await GetTenantAsync();

            if (typeof(TBuiltTenantDefinition) != builtDefinition.GetType())
                throw new TenantBuilderIncompatibleException($"Type {typeof(TBuiltTenantDefinition).FullName} does not match {builtDefinition.GetType().FullName}, have you registered a custom tenant builder?");

            return (TBuiltTenantDefinition)builtDefinition;
        }

        public async Task<IBuiltTenantDefinition> GetTenantAsync()
        {
            if (this.tenancyIdProvider == null)
            {
                throw new TenancyDependencyException($"There is no ITenancyIdProvider registered.");
            }

            var tenantId = this.tenancyIdProvider.GetTenantId();

            // Dont build a tenancy twice in the one session
            if (builtDefinitions.ContainsKey(tenantId.AsString))
            {
                return builtDefinitions[tenantId.AsString];
            }

            var tenantDefinition = new TenancyDefinition(tenantId);
            var tenantRecord = this.tenancyDbContext.Tenants.Where(r => r.TenantId == tenantDefinition.TenantId.AsString).FirstOrDefault();
            TenancyStore? tenancyStore = null;
            var key = Encoding.UTF8.GetBytes(this.tenancyOptions.Key);
            if (tenantRecord == null)
            {
                if (!this.tenancyOptions.AutocreateTenancies)
                {
                    throw new TenancyNotFoundException("A tenancy that does not exist could not be automatically created.");
                }

                tenantRecord = new Tenant();
                tenantRecord.Id = tenantDefinition.Id;
                tenantRecord.TenantId = tenantDefinition.TenantId.AsString;
                tenantRecord.Iv = tenantDefinition.Iv;
                this.tenancyDbContext.Tenants.Add(tenantRecord);

                // Only do the following if the tenancy never existed
                tenantDefinition.Store = new TenancyStore(this.tenancyDbContext, key, tenantDefinition);
                await this.tenancyBuilder.EstablishTenancyAsync(tenantDefinition);
                await this.tenancyDbContext.SaveChangesAsync();
            }
            else
            {
                tenantDefinition.Id = tenantRecord.Id;
                tenantDefinition.Iv = tenantRecord.Iv;
                tenantDefinition.Store = new TenancyStore(this.tenancyDbContext, key, tenantDefinition);
            }

            var builtTenancy = await this.tenancyBuilder.BuildTenancyAsync(tenantDefinition);

            if (!migratedTenancies.Contains(builtTenancy.Definition.TenantId.AsString))
            {
                await this.tenancyBuilder.MigrateTenancyAsync(builtTenancy);
                migratedTenancies.Add(builtTenancy.Definition.TenantId.AsString);
            }


            builtDefinitions.Add(builtTenancy.Definition.TenantId.AsString, builtTenancy);
            return builtTenancy;
        }
    }
}