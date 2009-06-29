using System;

namespace Tools.Zip
{
	/// <summary>
	/// This class contains ZipConstants used for gzip.
	/// </summary>
	public sealed class ZipConstants
	{
		private ZipConstants(){
		}
		public const byte DEFLATE = 4;	//Indicating deflate method of compression
		public const byte GZIP = 6;		//Indicating gzip method of compression

		public const int FixedHeaderSize = 30; //Fixed part of the header
		public const int SuperHeaderSize = 3;

		public const string AddFiles = "Add files";
		public const string NewArchive = "New archive";
		public const string OpenArchive = "Open archive";
		//Extensions for the files
		public const string Extension = "xip files (*.xip)|*.xip";
		public const string AllExtensions = "All files (*.*)|*.*";

		public const string BackSlash = "\\";
		public const string Dot = ".";

		public const string ExtractMessage = 
								"Extracted files to {0} successfully.";
		public const string OpenMessage = "Opened archive {0} for editing" ;
		public const string NewMessage = "Opened new archive {0} for editing";
		public const string AddMessage = "Added files to the archive.";

		public const string Title = "Compression sample";
		public const string GzipName = "GZip";
		public const string DeflateName = "Deflate";
		public const string ErrorName = "Error";

		public const string SeekError = "Attempted to seek before the beginning";
		public const string IOError = "An IO error has occured";
		public const string CloseError = "Stream already closed";
		public const string MemoryError = "Running out of memory";
		public const string ArgumentError = "Argument invalid";
		public const string FileNotFoundError = "File not found";
		public const string FileError = "Filename invalid";
		public const string CorruptedError = 
				"Trying to read a corrupted xip file";
		public const string FileExistsError = "File already exists";

		public const string FileReplace = 
			"The file chosen already exists. Do you want to replace it?";
		public const string Replace = "Replace";
	}
}
