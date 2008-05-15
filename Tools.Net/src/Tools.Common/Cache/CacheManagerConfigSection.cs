using System;
using System.Configuration;


namespace Tools.Common.Cache
{
    /// <summary>
    /// Configuration section for the <see cref="CacheManager"/>
    /// </summary>
    public class CacheManagerConfigSection : ConfigurationSection
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheManagerConfigSection"/> class.
        /// </summary>
        public CacheManagerConfigSection()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CacheManager"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        [ConfigurationProperty("Enabled", DefaultValue = "true", IsRequired = true)]
        public bool Enabled
        {
            get
            {
                return (bool)this["Enabled"];
            }
            set
            {
                this["Enabled"] = value;
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [requires file watch].
        /// </summary>
        /// <value><c>true</c> if [requires file watch]; otherwise, <c>false</c>.</value>
        [ConfigurationProperty("RequiresFileWatch", DefaultValue = "false", IsRequired = false)]
        public bool RequiresFileWatch
        {
            get
            {
                return (bool)this["RequiresFileWatch"];
            }
            set
            {
                this["RequiresFileWatch"] = value;
            }
        }

        //[ConfigurationProperty("changePublisherEndPoint", IsRequired=false )]
        //public RemoteObjectEndPoint ChangePublisherEndPoint
        //{

        //    get
        //    {
        //        return base["changePublisherEndPoint"] as RemoteObjectEndPoint;
        //    }
        //}

        #endregion Properties

     }
}