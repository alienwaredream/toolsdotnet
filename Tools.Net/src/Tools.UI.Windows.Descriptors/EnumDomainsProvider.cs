using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.UI.Windows.Descriptors
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// That is quite controversial implementation, it is absolutely cohesive with
    /// the IDomainsProvider. Anyway that is an interesting turn to see how Enum itself as a type 
    /// is close to be the type IDomainsProvider.
    /// </remarks>
    public class EnumDomainsProvider<T> : IDomainsProvider<T>
    {
        private System.Type enumType = null;

        public EnumDomainsProvider(System.Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException
                    (
                    "value of the enumType parameter should of the Enum type, for " + enumType.Name
                    );
            }
            this.enumType = enumType;
        }

        #region IDomainsProvider<Enum> Members
/// <summary>
/// That inconsistency is happening here, we don;t actually need the value here.
/// To be thought later.
/// </summary>
/// <param name="type"></param>
/// <returns></returns>
        public string[] GetDomainValues(T type)
        {
            return (string[])Enum.GetValues(enumType);
        }

        public string[] GetDomainNames()
        {
            return Enum.GetNames(enumType);
        }

        public T GetNewDefaultInstance()
        {
            return (T)enumType.TypeInitializer.Invoke(null);
        }

        #endregion
}
}
