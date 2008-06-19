using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;

namespace Tools.SwissKnife
{
    class SetFileModifiedDate
    {
        internal static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                System.Console.WriteLine(String.Format(CultureInfo.InvariantCulture,
                    "Usage: SetFileModifiedDate \"FilePath\""));
                return;

            }
            string filePath = args[0];

            if (!File.Exists(filePath))
            {
                System.Console.WriteLine("File: \"" + filePath + "\" doesn't exist!");
                return;

            }
            FileAttributes attr = File.GetAttributes(filePath);

            if ((attr & FileAttributes.ReadOnly) > 0)
            {
                System.Console.WriteLine("File: \"" + filePath + "\" is readonly!");
                return;
            }
            FileInfo fi = new FileInfo(filePath);
            fi.LastWriteTime = DateTime.Now.AddDays(-1);

            System.Console.WriteLine("Modified date is now set to: " + fi.LastWriteTime.ToString("ddMMMyyTHH:mm:ss fff"));
        }
    }
}
