using System;
using System.IO;
using System.IO.Compression;

namespace Tools.Common.Utils
{
    /// <summary>
    /// Encapsulates commonly invoked IO operations.
    /// <created by="Mark Morgan" date="26-Mar-2007" />
    /// <modified by="SD" date="20-Aug-2007">
    /// Class renamed to IOUtility to comply with the naming guidelines.
    /// TouchFile method added to &quot;touch&quot; the file.
    /// </modified>
    /// </summary>
    public static class IOUtility
    {
        #region Public Methods

        /// <summary>
        /// Compresses a stream.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void Compress(Stream source, Stream destination)
        {
            using (DeflateStream output = new DeflateStream(destination, CompressionMode.Compress))
            {
                Pump(source, destination);
            }
        }

        /// <summary>
        /// Gets the byte buffer from the passed input stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        public static byte[] GetBufferFromStream(Stream source)
        {
            byte[] sourceBuffer = new byte[source.Length];
            source.Read(sourceBuffer, 0, (int)source.Length);
            return sourceBuffer;
        }
        
        /// <summary>
        /// Gets the zipped byte buffer from the file.
        /// </summary>
        /// <param name="filePath">The path to the file being deserialized.</param>
        public static byte[] GetBufferFromFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                throw new ArgumentException("A non-existent file cannot be zipped.");

            // Unpack the file.
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return GetBufferFromStream(fs);
            }
        }

        /// <summary>
        /// Gets the zipped byte buffer from the file.
        /// </summary>
        /// <param name="sourceBuffer">The source buffer.</param>
        /// <returns></returns>
        public static byte[] GetCompressedBuffer(byte[] sourceBuffer)
        {
            if (sourceBuffer == null)
                throw new ArgumentNullException("sourceBuffer");

            // Zip and unpack the zipped stream.
            using (MemoryStream destination = new MemoryStream())
            {
                Compress(new MemoryStream(sourceBuffer), destination);
                return destination.ToArray();
            }
        }
        
        /// <summary>
        /// Gets the compressed byte buffer from the file.
        /// </summary>
        /// <param name="filePath">The path to the file being deserialized.</param>
        public static byte[] GetCompressedBufferFromFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                throw new ArgumentException("A non-existent file cannot be zipped.");

            return GetCompressedBuffer(GetBufferFromFile(filePath));
        }

        /// <summary>
        /// Gets the decompressed buffer from the passed compressed buffer.
        /// </summary>
        /// <param name="compressedBuffer">The compressed buffer.</param>
        /// <param name="decompressedByteLength">The decompressed length.</param>
        public static byte[] GetDecompressedBuffer(byte[] compressedBuffer,
                                                   int decompressedByteLength)
        {
            byte[] decompressedBuffer = new byte[decompressedByteLength];
            using (DeflateStream input = new DeflateStream(new MemoryStream(compressedBuffer), CompressionMode.Decompress, true))
            {
                input.BaseStream.Read(decompressedBuffer, 0, decompressedByteLength);
            }
            return decompressedBuffer;
        }

        /// <summary>
        /// Modifies the file last write time.
        /// </summary>
        /// <param name="filePath"></param>
        public static void TouchFile(string filePath)
        {
            (new FileInfo(filePath)).LastWriteTimeUtc = DateTime.UtcNow;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Copues one stream to another.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <param name="output">The output stream.</param>
        private static void Pump(Stream input, Stream output)
        {
            input.Position = 0;
            byte[] bytes = new byte[4096];
            int n;
            while ((n = input.Read(bytes, 0, bytes.Length)) != 0)
            {
                output.Write(bytes, 0, n);
            }
        }

        #endregion Private Methods


    }
}
