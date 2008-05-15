using System;
using System.IO;

namespace Tools.Common.Utils
{
	/// <summary>
	/// Utility for handling file and urn paths.
	/// </summary>
	public class PathUtility
	{
        /// <summary>
        /// Decodes the relative path to the full absolute path.
        /// </summary>
        /// <param name="path">The relative path.</param>
        /// <returns></returns>
        public static string DecodePath(string path)
        {
            string appBasePath =
                AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            // For some cases appBasePath would not contain the ending backslash,
            // (e.g. Test projests in VS2005). Thus checking for this and
            // adding if not present (SD).
            if (!appBasePath[appBasePath.Length - 1].Equals('\\'))
            {
                appBasePath += '\\';
            }
            
            return
                path.Replace
                (
                @".\",
                appBasePath
                ).Replace(@"~", appBasePath); ;
        }

        /// <summary>
        /// Surrounds path with quotes.
        /// </summary>
        /// <remarks></remarks>
        /// <param name="path">path to quote</param>
        /// <created by="SD, CG" date="8-Oct-2007" />
        /// <returns>Path surrounded with qoutes. Null if path is null. &quot;&quot; for empty string.</returns>
        public static string QuotePath(string path)
        {
            if (path == null) return path;

            if (path.Length == 0) return "\"\"";

            string retValue = path;

            if (!retValue.StartsWith("\""))
                retValue = "\"" + retValue;
            if (!retValue.EndsWith("\""))
                retValue = retValue + "\"";

            return retValue;
        }
	}
}
