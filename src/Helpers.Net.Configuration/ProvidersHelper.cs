using System;
using System.Configuration;
using System.Configuration.Provider;

namespace Helpers.Net.Configuration
{
    /// <remarks>
    /// This class actually resides in System.Web.Configuration, but is not supported for .NET 4 Client Profile.
    /// Inorder to support it, private ProvidersHelper class was created.
    /// </remarks>
    class ProvidersHelper
    {
        public static void InstantiateProviders(ProviderSettingsCollection configProviders, ProviderCollection providers, Type providerType)
        {
            if (!typeof(ProviderBase).IsAssignableFrom(providerType))
                throw new ConfigurationErrorsException(String.Format("type '{0}' must subclass from ProviderBase", providerType));

            foreach (ProviderSettings settings in configProviders)
                providers.Add(InstantiateProvider(settings, providerType));
        }

        public static ProviderBase InstantiateProvider(ProviderSettings providerSettings, Type providerType)
        {
            Type settingsType = Type.GetType(providerSettings.Type);

            if (settingsType == null)
                throw new ConfigurationErrorsException(String.Format("Could not find type: {0}", providerSettings.Type));
            if (!providerType.IsAssignableFrom(settingsType))
                throw new ConfigurationErrorsException(String.Format("Provider '{0}' must subclass from '{1}'", providerSettings.Name, providerType));

            ProviderBase provider = Activator.CreateInstance(settingsType) as ProviderBase;

            provider.Initialize(providerSettings.Name, providerSettings.Parameters);

            return provider;
        }
    }
}
