using System;
using System.Collections.Generic;
using System.IO;

namespace Tools.Zip
{
    /// <summary>
    /// This class represents a Zip archive.  You can ask for the contained
    /// entries, or get an uncompressed file for a file entry.  
    /// </summary>
    public class ZipFile
    {
        List<ZipEntry> zipEntries;		// The collection of entries
        private ZipReader thisReader;
        private ZipWriter thisWriter;

        private Stream baseStream;		// Stream to which the writer writes 
        // both header and data, the reader
        // reads this
        private string zipName;

        /// <summary>
        /// Created a new Zip file with the given name.
        /// </summary>
        /// <param name="method"> Gzip or deflate</param>
        /// <param name="name"> Zip name</param>
        public ZipFile(string name, byte method, FileMode mode)
        {
            zipName = name;

            baseStream = new FileStream(zipName, mode);
            thisWriter = new ZipWriter(baseStream);
            thisWriter.Method = method;

            //New File
            thisWriter.WriteSuperHeader(0, method);

            int index1 = zipName.IndexOf(ZipConstants.Dot);
            int index2 = zipName.LastIndexOf(ZipConstants.BackSlash);
            thisReader = new ZipReader(baseStream, zipName.Substring(index2,
                    index1 - index2));

            zipEntries = thisReader.GetAllEntries();

        }

        /// <summary>
        /// Opens a Zip file with the given name.
        /// </summary>
        /// <param name="name"> Zip name</param>
        public ZipFile(string name)
        {
            zipName = name;
            baseStream = new FileStream(zipName, FileMode.Open);
            thisWriter = new ZipWriter(baseStream);

            int index1 = zipName.IndexOf(ZipConstants.Dot);
            int index2 = zipName.LastIndexOf(ZipConstants.BackSlash);
            thisReader = new ZipReader(baseStream, zipName.Substring(index2,
                    index1 - index2));

            zipEntries = thisReader.GetAllEntries();
            thisWriter.Method = thisReader.Method;
        }


        /// <summary>
        /// Gets offset to which the jump should be made by summing up 
        /// all the individual lengths.
        /// </summary>
        /// <returns>
        /// the offset from SeekOrigin.Begin
        /// </returns>
        private long GetOffset(int index)
        {
            if (index > zipEntries.Count)
                return -1;
            int jump = ZipConstants.SuperHeaderSize;
            int i;
            for (i = 0; i < index - 1; ++i)
            {
                ZipEntry entry = zipEntries[i];
                jump += ZipConstants.FixedHeaderSize + entry.NameLength
                    + entry.CompressedSize;
            }
            return jump;
        }

        public void Add(string fileName)
        {

            ZipEntry entry = new ZipEntry(fileName);
            thisWriter.Add(entry);

            zipEntries.Add(entry);
            thisWriter.CloseHeaders((Int16)zipEntries.Count);
        }

        /// <summary>
        /// Closes the ZipFile.  This also closes all input streams given by
        /// this class.  After this is called, no further method should be
        /// called.
        /// </summary>
        public void Close()
        {
            if (baseStream != null)
                baseStream.Close();
        }

        /// <summary>
        /// Gets the entries of compressed files.
        /// </summary>
        /// <returns>
        /// Collection of ZipEntries
        /// </returns>
        public List<ZipEntry> Entries
        {
            get
            {
                return zipEntries;
            }
        }

        public byte CompressionMethod()
        {
            return thisWriter.Method;
        }

        public int CheckFileExists(string fileName)
        {
            System.Globalization.CultureInfo ci =
                        System.Threading.Thread.CurrentThread.CurrentUICulture;
            int i = -1;
            foreach (ZipEntry eachEntry in zipEntries)
            {
                ++i;
                if (eachEntry.Name.ToLower(ci).Equals(fileName.ToLower(ci)))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}