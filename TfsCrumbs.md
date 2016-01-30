Backup dbs - TFS2008
```


SET NOCOUNT ON

DECLARE @DateSuffix nvarchar(30),
        @DateTimeSuffix nvarchar(30),
        @RootPath nvarchar(200),
        @BackupName nvarchar(100),
        @BackupFile nvarchar(255),
        @TodaysDirectory nvarchar(30),
        @DBName nvarchar(30)

DECLARE @databases table (DBName nvarchar(30))
-- Add an insert here for any new database to backup
INSERT INTO @databases (DBName) VALUES (N'ReportServer')
INSERT INTO @databases (DBName) VALUES (N'WSS_AdminContent')
INSERT INTO @databases (DBName) VALUES (N'WSS_Config')
INSERT INTO @databases (DBName) VALUES (N'WSS_Content')
INSERT INTO @databases (DBName) VALUES (N'TfsActivityLogging')
INSERT INTO @databases (DBName) VALUES (N'TfsBuild')
INSERT INTO @databases (DBName) VALUES (N'TfsIntegration')
INSERT INTO @databases (DBName) VALUES (N'TfsVersionControl')
INSERT INTO @databases (DBName) VALUES (N'TfsWarehouse')
INSERT INTO @databases (DBName) VALUES (N'TfsWorkItemTracking')
INSERT INTO @databases (DBName) VALUES (N'TfsWorkItemTrackingAttachments')
-- Set day and time based parameters
SET @DateSuffix = N'_t'
SET @DateTimeSuffix = N'_n'
SET @RootPath = N'C:\dbbackups' --N'E:\DBBackups\'
SET @TodaysDirectory = @RootPath -- + @DateSuffix + N'\'

-- Setup and run a cursor
DECLARE CursorOverDatabases CURSOR FOR SELECT DBName FROM @databases

OPEN CursorOverDatabases

FETCH NEXT FROM CursorOverDatabases INTO @DBName

WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @BackupName = @DBName + N'_' + @DateTimeSuffix
        SET @BackupFile = @TodaysDirectory + @BackupName + N'.bak'

        BACKUP DATABASE @DBName TO  DISK = @BackupFile
            WITH NOFORMAT, NOINIT,  NAME = @BackupName, SKIP, REWIND, NOUNLOAD,  STATS = 10

        DECLARE @backupSetId as int
        SELECT @backupSetId = position from msdb..backupset where database_name=@DBName and backup_set_id=(select max(backup_set_id) from msdb..backupset where database_name=@DBName)
        IF @backupSetId is null
            BEGIN RAISERROR(N'Verify failed. Backup information for database %s not found.', 16, 1, @DBName) END

        RESTORE VERIFYONLY FROM  DISK = @BackupFile
            WITH  FILE = @backupSetId,  NOUNLOAD,  NOREWIND

        FETCH NEXT FROM CursorOverDatabases INTO @DBName
    END
CLOSE CursorOverDatabases
DEALLOCATE CursorOverDatabases 
```

## On assemblies versioning in  TFS 2010 ##

http://blogs.msdn.com/b/jimlamb/archive/2010/02/12/how-to-create-a-custom-workflow-activity-for-tfs-build-2010.aspx