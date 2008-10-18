using System;
using System.Collections.Generic;
using System.Text;

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
    [Serializable()]
    public class Container <SettingsType, ContainedType> : Descriptor
        where SettingsType : new() 
        where ContainedType: new()
    {
        #region Globals

        private SettingsType _settings;
        private ContainedType _containerObject;
        
        #endregion

        #region Properties
        public SettingsType Settings
        {
            get { return _settings; }
            set { _settings = value; }
        }
        public ContainedType ContainerObject
        {
            get { return _containerObject; }
            set { _containerObject = value; }
        } 

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
            _settings = settings;
            _containerObject = containerObject;
        } 
        #endregion

    }
}
