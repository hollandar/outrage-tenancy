using Outrage.Tenancy.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outrage.Tenancy.Definition
{
    public sealed class FeatureCollection: IDisposable
    {
        private readonly Dictionary<Type, ITenancyFeature> features = new();

        public void Set<TTenancyFeature>(TTenancyFeature feature) where TTenancyFeature: ITenancyFeature
        {
            this.features.Add(typeof(TTenancyFeature), feature);
        }

        public TTenancyFeature Get<TTenancyFeature>() where TTenancyFeature: ITenancyFeature
        {
            if (features.TryGetValue(typeof(TTenancyFeature), out var tenancyFeature))
            {
                return (TTenancyFeature)tenancyFeature;
            }

            throw new FeatureNotFoundException(typeof(TTenancyFeature).FullName);
        }

        public void Dispose()
        {
            foreach (var feature in features)
            {
                if (feature.Value is IDisposable)
                {
                    var disposable = (IDisposable)feature.Value;
                    disposable.Dispose();
                }
            }
        }
    }
}
