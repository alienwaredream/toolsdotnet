using System;
using System.Text;
using System.IO;
using System.IO.Compression;
using Tools.Core.Asserts;

namespace Tools.Core.Utils
{
    public static class CompressionUtility
    {
        public static void CompressFile(string sourceFilePath, string targetFilePath)
        {
            ErrorTrap.AddAssertion(File.Exists(sourceFilePath), String.Format("Source file [{0}] doesn't exist!", sourceFilePath));
            ErrorTrap.AddAssertion(!File.Exists(targetFilePath), String.Format("Target file [{0}] already exists!", sourceFilePath));

            ErrorTrap.RaiseTrappedErrors<InvalidOperationException>();

            using (FileStream targetFileStream = new FileStream(targetFilePath, FileMode.CreateNew, FileAccess.Write))
            {
                using (Stream zipStream = new GZipStream(targetFileStream, CompressionMode.Compress, true))
                {
                    using (FileStream sourceFileStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
                    {

                        byte[] buffer = new byte[sourceFileStream.Length];

                        sourceFileStream.Read(buffer, 0, buffer.Length);

                        zipStream.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }
    }
}