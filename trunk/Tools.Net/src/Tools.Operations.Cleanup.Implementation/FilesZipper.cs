using System;

using Tools.Coordination.Batch;
using System.IO;
using Tools.Core.Asserts;

namespace Tools.Operations.Cleanup.Implementation
{

    using SharpZip = ICSharpCode.SharpZipLib;
    using System.Text;
    using System.Diagnostics;

    internal class FilesZipper : ScheduleTaskProcessor
    {
        string directoryToArchive;
        int daysToArchive = 2;
        string zipDirectory;
        string timestampFormat = "dd-MMM-yy_HH-mm-ss";

        protected override void ExecuteSheduleTask()
        {
            Guid archivationGuid = Guid.NewGuid();
            StringBuilder sb = new StringBuilder();
            string zipFilePath = null;
            DateTime dateThreshold = DateTime.Now.AddDays(-daysToArchive);

            Trace.CorrelationManager.ActivityId = archivationGuid;
            Log.TraceData(Log.Source, TraceEventType.Start, CleanupMessages.CleanupIterationStarted,
                String.Format(
                "Started archival iteration for directory [{0}] with modified date threshold [{1}]. Archivation target directory is [{2}]",
                directoryToArchive, dateThreshold.ToString(timestampFormat), zipDirectory));

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

            foreach (string file in files)
            {
                try
                {

                    FileInfo fi = new FileInfo(file);


                    if (fi.LastWriteTime < dateThreshold)
                    {

                        zipFilePath = zipDirectory.TrimEnd('\\') + @"\" + fi.Name +
                            "-" + fi.LastWriteTime.ToString(timestampFormat) + ".zip";

                        if (!File.Exists(zipFilePath))
                        {

                            using (SharpZip.Zip.ZipFile target = SharpZip.Zip.ZipFile.Create(zipFilePath))
                            {

                                target.BeginUpdate();

                                target.Add(file, fi.Name +
                                    "-" + fi.LastWriteTime.ToString(timestampFormat) + fi.Extension);

                                target.CommitUpdate();

                                if (sb.Length == 0)
                                    sb.Append("Archivation results: \r\n");
                                sb.Append(String.Format("File [{0}] archived to [{1}]", file, zipFilePath));
                                sb.Append(Environment.NewLine);
                            }

                            File.Delete(file);
                        }
                        else
                        {
                            Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Warning, CleanupMessages.ArchiveWithSameNameAlreadyExisted,
                                String.Format("Archive [{0}] for file [{1} (last modified date:{2})] already exists!",
                                zipFilePath, file, fi.LastWriteTime.ToString("dd-MMM-yy_HH-mm-ss")));
                        }

                    }
                }
                catch (Exception ex)
                {
                    Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Error, CleanupMessages.ErrorWhileArchivingTheFile,
                        String.Format("Error while archiving the file [{0}] to zip file [{1}]. \r\nException text: {2}", file, zipFilePath, ex.ToString()));

                }

            }
            if (sb.Length > 0)
            {
                Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Information, CleanupMessages.FilesArchived,
                    sb.ToString());
            }
            else
            {
                Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Information, CleanupMessages.NothingToArchive,
                    String.Format("There was nothing to archive in [{0}]. Modified date threshold was [{1}]",
                    directoryToArchive, dateThreshold.ToString(timestampFormat)));
            }
        }
    }
}