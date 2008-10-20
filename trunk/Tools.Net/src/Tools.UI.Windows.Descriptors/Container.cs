using System;
using Tools.Core;

namespace Tools.UI.Windows.Descriptors
{
    // TODO: subject to move to different, more abstract package. (SD)
    /// <summary>
    /// Contains the object of ContainedType and some generic settings of 
    /// type SettingsType. POC at the moment to be renamed once hierachy is more 
    /// stable.
    /// </summary>
    /// <typeparam name="SettingsType"></typeparam>
    /// <typeparam name="ContainedType"></typeparam>
    [Serializable]
    public class Container<SettingsType, ContainedType> : Descriptor
        where SettingsType : new()
        where ContainedType : new()
    {
        #region Globals

        #endregion

        #region Properties

        public SettingsType Settings { get; set; }

        public ContainedType ContainerObject { get; set; }

        #endregion

        #region Constructors

        public Container
            (
            )
            : this
                (
                "GenericContainerName",
                "GenericContainerDescription",
                new SettingsType(),
                new ContainedType()
                )
        {
        }

        public Container
            (
            string name,
            string description,
            SettingsType settings,
            ContainedType containerObject
            )
            :
                base(name, description)
        {
            Settings = settings;
            ContainerObject = containerObject;
        }

        #endregion
    }
}