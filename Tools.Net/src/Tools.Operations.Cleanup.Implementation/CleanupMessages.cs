using System;

namespace Tools.Operations.Cleanup.Implementation
{
    internal enum CleanupMessages
    {
        CleanupIterationStarted = 18001,
        ArchiveDirectoryDoesntExist = 18002,
        FilesArchived = 18003,
        ArchiveWithSameNameAlreadyExisted = 18004,
        NothingToArchive = 18005,

        ErrorWhileArchivingTheFile = 18051,
    }
}