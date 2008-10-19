
SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
IF EXISTS (SELECT * FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#tmpErrors')) DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
IF NOT EXISTS (SELECT * FROM master.dbo.syslogins WHERE loginname = N'ToolsLogWriter')
CREATE LOGIN [ToolsLogWriter] WITH PASSWORD = 'p@ssw0rd'
GO
CREATE USER [ToolsLogWriter] FOR LOGIN [ToolsLogWriter] WITH DEFAULT_SCHEMA=[dbo]
GO
GRANT CONNECT TO [ToolsLogWriter]
GO
PRINT N'Creating role LogWriter'
GO
CREATE ROLE [LogWriter]
AUTHORIZATION [dbo]
GO
EXEC sp_addrolemember N'LogWriter', N'ToolsLogWriter'
GO
BEGIN TRANSACTION
GO
PRINT N'Creating schemata'
GO
if schema_id('Common') is null
execute('CREATE SCHEMA [Common] AUTHORIZATION [dbo]')
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [Common].[Log]'
GO
CREATE TABLE [Common].[Log]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[ActivityId] [uniqueidentifier] NULL,
[CorrelationId] [uniqueidentifier] NULL,
[Date] [datetime] NOT NULL,
[Message] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MessageId] [int] NULL,
[TypeId] [int] NULL,
[TypeName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Priority] [int] NULL,
[MachineName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ModulePath] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ModuleName] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ModuleVersion] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ThreadName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ThreadIdentity] [nvarchar](50) NULL,
	[OSIdentity] [nvarchar](50) NULL,
-- Extra fields	
	[JobId] [bigint] NULL,
	[RepId] [varchar](200) NULL,
	[IP] [varchar](30) NULL,
	[Extra] [nvarchar](200) NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Log] on [Common].[Log]'
GO
ALTER TABLE [Common].[Log] ADD CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [Common].[uspInsertLogMessage]'
GO
CREATE PROCEDURE [Common].[uspInsertLogMessage] 
	@ActivityId uniqueidentifier = NULL,
	@CorrelationId uniqueidentifier = NULL,
	@Date datetime = GETUTCDATE,
	@Message nvarchar(max) = NULL,
	@MessageId int = NULL,
	@TypeId int = NULL,
	@TypeName varchar(50) = NULL,
	@Priority int = NULL,
	@MachineName nvarchar(100) = NULL,
	@ModulePath nvarchar(500) = NULL,
	@ModuleName nvarchar(500) = NULL,
	@ModuleVersion varchar(50) = NULL,
	@ThreadName nvarchar(50) = NULL,
	@ThreadIdentity nvarchar(50) = NULL,
	@OSIdentity nvarchar(50) = NULL,
	-- Extra fields
	@JobId [bigint] = NULL,
	@RepId [varchar](200) = NULL,
	@IP [varchar](30) = NULL,
	@Extra [nvarchar](200) = NULL
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	INSERT INTO [Common].[Log]
           ([ActivityId]
           ,[CorrelationId]
           ,[Date]
           ,[Message]
           ,[MessageId]
           ,[TypeId]
           ,[TypeName]
           ,[Priority]
           ,[MachineName]
           ,[ModulePath]
           ,[ModuleName]
           ,[ModuleVersion]
           ,[ThreadName]
           ,[ThreadIdentity]
           ,[OSIdentity]
			-- Extra fields
			,[JobId]
			,[RepId]
			,[IP]
			,[Extra]			
			)
     VALUES
           (@ActivityId
           ,@CorrelationId
           ,@Date
           ,@Message
           ,@MessageId
           ,@TypeId
           ,@TypeName
           ,@Priority
           ,@MachineName
           ,@ModulePath
           ,@ModuleName
           ,@ModuleVersion
           ,@ThreadName
           ,@ThreadIdentity
           ,@OSIdentity
			-- Extra fields
			,@JobId
			,@RepId
			,@IP
			,@Extra
)

END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering permissions on [Common].[uspInsertLogMessage]'
GO
GRANT EXECUTE ON  [Common].[uspInsertLogMessage] TO [LogWriter]
GO
GRANT CONNECT TO [ToolsLogWriter]
IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT 'The database update succeeded'
COMMIT TRANSACTION
END
ELSE PRINT 'The database update failed'
GO
DROP TABLE #tmpErrors
GO
-- =============================================
-- Author:		Stanislav Dvoychenko, Jose Silva
-- Create date: -Mar-2007
-- Description:	Removes records from the log tables. Processes remove as a batch with a size which is passed
-- as a parameter. Also number of days to remove data for is a parameter.

-- Example:
-- EXEC [Common].[esc_usp_ClearLog] 10000, 1
-- Modification History
-- =============================================

CREATE PROCEDURE [Common].[esc_usp_ClearLog]
	@RecordsCount int = 0, -- Records count to delete
	@NumberOfDays int = 2
AS
	DECLARE @CeilingDate datetime;
	
	SET @CeilingDate = DATEADD(day, -@NumberOfDays, GETUTCDATE()) -- substructs days
	-- Identify the smallest value available
	DECLARE @CeilingId numeric(18,0)
	SELECT @CeilingId = MIN(Id) FROM [Common].[Log]
	-- Add the @RecordsCount
	SET @CeilingId = @CeilingId + @RecordsCount
	-- Delete the batch
	DELETE TOP(@RecordsCount) FROM [Common].[Log] WITH (PAGLOCK)
		WHERE Id < @CeilingId AND Date < @CeilingDate
		PRINT @CeilingDate
RETURN 0;