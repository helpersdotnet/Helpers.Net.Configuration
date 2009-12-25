using System;
using System.Configuration;
using System.Configuration.Provider;

namespace Helpers.Net.Configuration.Provider
{
    /// <summary>
    /// Represents a collection of provider objects that inherit from <see cref="ProviderBase"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ProviderCollection<T> : ProviderCollection
        where T : Helpers.Net.Configuration.Provider.ProviderBase
    {
        #region Public Properties
        /// <summary>
        /// Gets the <see cref="T:DataProvider"/> with the specified name.
        /// </summary>
        /// <value></value>
        public new T this[string name]
        {
            get { return (T)base[name]; }
        }

        /// <summary>
        /// Gets the cref="&lt;T&gt;"/> at the specified index.
        /// </summary>
        /// <value></value>
        public T this[int index]
        {
            get
            {
                int counter = 0;

                foreach (T provider in this)
                {
                    if (counter == index)
                    {
                        return provider;
                    }
                    counter++;
                }

                return null;
            }
        }
        #endregion Public Properties

        #region Public Methods
        /// <summary>
        /// Adds a provider to the collection.
        /// </summary>
        /// <param name="provider">The provider to be added.</param>
        /// <permissionSet class="System.Security.permissionSet" version="1">
        /// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
        /// </permissionSet>
        public override void Add(
            System.Configuration.Provider.ProviderBase provider)
        {
            if (!(provider is T))
            {
                throw new ArgumentException(
                    String.Format("The provider is not of type {0}.", typeof(T).ToString()));
            }

            this.Add((T)provider);
        }

        /// <summary>
        /// Adds the specified provider.
        /// </summary>
        /// <param name="provider">The provider.</param>
        public void Add(
            T provider)
        {
            if (!provider.IsInitialized)
            {
                provider.Initialize();
            }

            base.Add(provider);
        }
        #endregion Public Methods
    }
}
