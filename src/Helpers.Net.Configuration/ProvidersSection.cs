using System.Configuration;

namespace Helpers.Net.Configuration.Provider
{
    /// <summary>
    /// A configuration section for a collection of providers.
    /// </summary>
    public class ProvidersSection : ConfigurationSection
    {
        private const string m_DefaultProviderProperty = "defaultProvider";

        /// <summary>
        /// Gets the providers.
        /// </summary>
        /// <value>The providers.</value>
        [ConfigurationProperty("providers")]
        public ProviderSettingsCollection Providers
        {
            get { return (ProviderSettingsCollection)base["providers"]; }
        }

        /// <summary>
        /// Gets or sets the default provider.
        /// </summary>
        /// <value>The default provider.</value>
        [ConfigurationProperty(m_DefaultProviderProperty, IsRequired = false)]
        public string DefaultProvider
        {
            get { return (string)base[m_DefaultProviderProperty]; }
            set { base[m_DefaultProviderProperty] = value; }
        }

        [ConfigurationProperty("xmlns", IsRequired = false)]
        public string Xmlns
        {
            get { return (string)base["xmlns"]; }
        }
    }
}
