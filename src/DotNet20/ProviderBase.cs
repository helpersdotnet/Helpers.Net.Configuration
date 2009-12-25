using System;
using System.Collections.Specialized;

namespace Helpers.Net.Configuration.Provider
{
    /// <summary>
    /// Provides a base implementation for the extensible provider model.
    /// </summary>
    /// <example>
    ///     Following is an example on how to implement the Abstract Provider.
    ///     <code lang="CS" title="Helerps.Net Base Configuration Provider">
    /// public abstract class GeographyProvider : HelpersDotNet.Configuration.Provider.ProviderBase
    /// {
    ///     //List of services to provide.
    ///     public abstract string LocateCountry(string ipAddress);
    ///     public abstract string LocateCountryCode(string ipAddress); 
    /// }
    /// </code>
    /// </example>
    /// <keywords>Provider|ProviderBase|ProviderCollection|System.Configuration.Provider</keywords>
    /// <seealso cref="!:http://www.danrigsby.com/blog/index.php/2008/02/22/how-to-extend-the-provider-model-to-make-it-easy-to-use/">[New Link]</seealso>
    /// <remarks>
    /// This abstract class services as the base for your provider. This usually contains
    /// all abstract methods and properties or overloaded methods and implemntation details
    /// that can be shared by all providers.
    /// </remarks>
    public class ProviderBase : System.Configuration.Provider.ProviderBase
    {
        #region Private Properties

        private NameValueCollection _configuration = new NameValueCollection();
        private bool _sInitialized = false;
        private string _name = String.Empty;

        #endregion Private Properties

        #region Public Properties
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public NameValueCollection Configuration
        {
            get { return _configuration; }
        }

        /// <summary>
        /// Gets or sets the friendly name used to refer to the provider during configuration.
        /// </summary>
        /// <value></value>
        /// <returns>The friendly name used to refer to the provider during configuration.</returns>
        public new string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitialized
        {
            get { return _sInitialized; }
        }
        #endregion Public Properties

        #region Overrides
        /// <summary>
        /// Initializes the provider.
        /// </summary>
        public void Initialize()
        {
            Initialize(String.Empty);
        }

        /// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param>
        public void Initialize(
            string name)
        {
            Initialize(name, new NameValueCollection());
        }

        /// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param>
        /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
        public override void Initialize(
            string name,
            NameValueCollection config)
        {
            if (config == null)
            {
                config = new NameValueCollection();
            }

            // Get default name if empty or null
            if (String.IsNullOrEmpty(name))
            {
                if (!String.IsNullOrEmpty(_name))
                {
                    name = _name;
                }
                else
                {
                    name = this.GetType().Name;
                }
            }
            _name = name;

            // Store configuration values
            _configuration.Clear();
            foreach (string key in config.Keys)
            {
                _configuration.Add(key, config[key]);
            }


            // Call the base class's Initialize method
            base.Initialize(name, config);

            _sInitialized = true;
        }
        #endregion Overrides
    }
}
