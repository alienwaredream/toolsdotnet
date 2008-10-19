using System.Configuration;
using Tools.Core;

namespace Tools.Processes.Host
{
    public class ServiceHostInstallConfigSection : ConfigurationSection, IDescriptor
    {
        #region Constructors

        #endregion Constructors

        #region Properties

        #endregion Properties

        [ConfigurationProperty("displayName", DefaultValue = "Please, define a display name for the service!",
            IsRequired = true)]
        public string DisplayName
        {
            get { return (string) this["displayName"]; }
            set { this["displayName"] = value; }
        }

        #region IDescriptor Members

        [ConfigurationProperty("name", DefaultValue = "GenericServiceHost", IsRequired = true)]
        public string Name
        {
            get { return (string) this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("description", DefaultValue = "GenericServiceHost service", IsRequired = false)]
        public string Description
        {
            get { return (string) this["description"]; }
            set { this["description"] = value; }
        }

        #endregion
    }
}