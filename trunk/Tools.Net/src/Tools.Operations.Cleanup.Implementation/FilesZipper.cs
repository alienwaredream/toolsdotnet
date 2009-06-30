using System;

using Tools.Coordination.Batch;
using System.IO;
using Tools.Core.Asserts;

namespace Tools.Operations.Cleanup.Implementation
{

    using SharpZip = ICSharpCode.SharpZipLib;
    using System.Text;

    internal class FilesZipper : ScheduleTaskProcessor
    {
        string directoryToArchive;
        int daysToArchive = 2;
        string zipDirectory;

        protected override void ExecuteSheduleTask()
        {
            Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Information, CleanupMessages.CleanupIterationStarted, "Started archival iteration");

            if (String.IsNullOrEmpty(directoryToArchive))
            {
                Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Information, CleanupMessages.ArchiveDirectoryDoesntExist, String.Format("Path to directory to archive is empty, no action will be taken."));
                return;
            }

            if (!Directory.Exists(directoryToArchive))
            {
                Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Information, CleanupMessages.ArchiveDirectoryDoesntExist, String.Format("Directory to archive doesn't exist: [{0}]. No action will be taken.", directoryToArchive));
                return;
            }

            string[] files = Directory.GetFiles(directoryToArchive);
            StringBuilder sb = new StringBuilder("Archivation results: \r\n");

            foreach (string file in files)
            {
                try
                {
                    FileInfo fi = new FileInfo(file);

                    if (fi.LastWriteTime < DateTime.Now.AddDays(-daysToArchive))
                    {
                        using (SharpZip.Zip.ZipFile target = SharpZip.Zip.ZipFile.Create(zipDirectory + @"\" + file + ".zip"))
                        {

                            target.BeginUpdate();

                            target.Add(file);

                            target.CommitUpdate();
                        }

                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}