using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Tools.Core.Utils
{
    public sealed class AssemblyInfoUtility
    {
        #region Constructors

        private AssemblyInfoUtility()
        {
        }

        #endregion Constructors

        #region Assembly Attibute Accessors

        public static string AssemblyTitle
        {
            get
            {
                // Get all Title attributes on this assembly
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof (AssemblyTitleAttribute),
                                                                                      false);
                // If there is at least one Title attribute
                if (attributes.Length > 0)
                {
                    // Select the first one
                    var titleAttribute = (AssemblyTitleAttribute) attributes[0];
                    // If it is not an empty string, return it
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
                return Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().CodeBase);
            }
        }

        public static string AssemblyVersion
        {
            get { return Assembly.GetEntryAssembly().GetName().Version.ToString(); }
        }

        public static string AssemblyDescription
        {
            get
            {
                // Get all Description attributes on this assembly
                object[] attributes =
                    Assembly.GetEntryAssembly().GetCustomAttributes(typeof (AssemblyDescriptionAttribute), false);
                // If there aren't any Description attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Description attribute, return its value
                return ((AssemblyDescriptionAttribute) attributes[0]).Description;
            }
        }

        public static string AssemblyProduct
        {
            get
            {
                // Get all Product attributes on this assembly
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(
                    typeof (AssemblyProductAttribute), false);
                // If there aren't any Product attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Product attribute, return its value
                return ((AssemblyProductAttribute) attributes[0]).Product;
            }
        }

        public static string AssemblyCopyright
        {
            get
            {
                // Get all Copyright attributes on this assembly
                object[] attributes =
                    Assembly.GetEntryAssembly().GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false);
                // If there aren't any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute) attributes[0]).Copyright;
            }
        }

        public static string AssemblyCompany
        {
            get
            {
                // Get all Company attributes on this assembly
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(
                    typeof (AssemblyCompanyAttribute), false);
                // If there aren't any Company attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Company attribute, return its value
                return ((AssemblyCompanyAttribute) attributes[0]).Company;
            }
        }

        #endregion

        #region Windows Logo Accessors

        public static string ApplicationSpecificDirectoryPart
        {
            get { return AssemblyCompany + @"\" + AssemblyProduct; }
        }

        public static string ApplicationSettingsCommonDirectory
        {
            get
            {
                CreateApplicationSettingsCommonDirectory();
                return
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                    @"\" + ApplicationSpecificDirectoryPart;
            }
        }

        #endregion

        private static void CreateApplicationSettingsCommonDirectory()
        {
            if (Directory.Exists
                (
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                @"\" + ApplicationSpecificDirectoryPart
                ))
                return;
            Directory.CreateDirectory
                (
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                @"\" + ApplicationSpecificDirectoryPart
                );
        }

        public static string DumpAssemblies()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var sb = new StringBuilder();
            sb.Append("<Assemblies>");

            foreach (Assembly a in assemblies)
            {
                sb.Append
                    (
                    "<Assembly FullName=\"" +
                    a.FullName + Environment.NewLine +
                    "\""
                    );

                try
                {
                    sb.Append
                        (
                        " codeBase=\"" + a.CodeBase + "\" location=\"" + a.Location + "\"/>"
                        );
                }
                catch (Exception ex)
                {
                    sb.Append
                        (
                        "> <Exception>" + ex + "</Exception></Assembly>"
                        );
                }
            }
            sb.Append("</Assemblies>");

            return sb.ToString();
        }
    }
}