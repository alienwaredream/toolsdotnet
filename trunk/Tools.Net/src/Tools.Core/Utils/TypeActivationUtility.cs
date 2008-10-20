using System;
using System.Configuration;
using System.Reflection;
using Tools.Core.Configuration;

namespace Tools.Core.Utils
{
    /// <summary>
    /// Summary description for TypeActivationUtility.
    /// </summary>
    public class TypeActivationUtility
    {
        private TypeActivationUtility()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static object CreateInstance(TypeActivationSource source)
        {
#warning Very ad-hoc, change the bellow to provide bin paths !!!! (SD)
            string appBasePath =
                AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            // For some cases appBasePath would not contain the ending backslash,
            // (e.g. Test projests in VS2005). Thus checking for this and
            // adding if not present (SD).
            if (!appBasePath[appBasePath.Length - 1].Equals('\\'))
            {
                appBasePath += '\\';
            }
            string activationPath =
                source.TypeLocator.Path.Replace
                    (
                    @".\",
                    appBasePath
                    );

            // TODO: Check if path exists! (SD)

            if (String.IsNullOrEmpty(activationPath))
            {
                // TODO: Apply better message, use name of the handler config section!
                throw new ConfigurationErrorsException
                    (
                    String.Format
                        (
                        "Path attribute of the TypeActivationSource can't be empty or null." +
                        "Element contents:{0}",
                        SerializationUtility.Serialize2String(source)
                        ));
            }
            if (String.IsNullOrEmpty(source.TypeLocator.Type))
            {
                // TODO: Apply better message, use name of the handler config section!						
                throw new Exception
                    (
                    String.Format
                        (
                        "Type attribute of the TypeActivationSource can't be empty or null." +
                        "Element contents:{0}",
                        SerializationUtility.Serialize2String(source)
                        ));
            }

            object[] args = null;

            if (source.Arguments != null && source.Arguments.Count > 0)
            {
                args = new object[source.Arguments.Count];
                for (int i = 0; i < args.Length; i++)
                {
                    // TODO: enable hash args here as well! (SD) 
                    args[i] = source.Arguments[i].ToString();
                }
            }
            // TODO: put into try catch block here (SD)
            return
                Activator.CreateInstanceFrom
                    (
                    activationPath,
                    source.TypeLocator.Type,
                    true,
                    BindingFlags.CreateInstance,
                    null,
                    args,
                    null,
                    null,
                    null
                    ).Unwrap();
        }
    }
}