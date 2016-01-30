# MS-SQL #
## Parameter sniffing ##

http://www.sqlpointers.com/2006/11/parameter-sniffing-stored-procedures.html

## CTEs ##

More: http://www.setfocus.com/TechnicalArticles/Articles/sql-server-2005-tsql-3.aspx

## Recursive CTE plus CSV from select samples ##
```
CREATE FUNCTION eng_GetNodeParents (@NodeId int)
-- Created by: Stanislav Dvoychenko @ 12-Aug-2010
-- Synopsis: Returns a table of NodeId, Title of parent nodes to the passed in NodeId
RETURNS TABLE
AS RETURN
(
WITH NodeCte (NodeId, Title, ParentNodeId) AS 
            -- Anchor query
            (SELECT hn1.NodeId, nl1.Title, hn1.ParentNodeId 
                    FROM HierarchyNode hn1 
						INNER JOIN NodeLocal nl1 ON hn1.NodeId = nl1.NodeId
						where hn1.NodeId = @NodeId --92161
                UNION ALL
                    -- Recursive query
                    SELECT hn2.NodeId, nl2.Title, hn2.ParentNodeId 
						FROM HierarchyNode hn2
							INNER JOIN NodeLocal nl2 ON hn2.NodeId = nl2.NodeId
                            INNER JOIN NodeCte ON NodeCte.ParentNodeId = hn2.NodeId)         

SELECT * FROM NodeCte
)

GO

-- =============================================
-- Author:		Stanislav Dvoychenko
-- Create date: 12-Aug-2010
-- Description:	Returns pipe separated list of Parent node titles
-- for a provided . Currently specific, subject to refactor to be generic
-- =============================================
CREATE FUNCTION eng_GetPipedParentNodeTitles
(
@NodeId int
)
RETURNS nvarchar(2000)
AS
BEGIN

	DECLARE @csv nvarchar(max)
	SET @csv = N'';
	SELECT @csv = @csv + Title + N'|' from eng_GetNodeParents(@NodeId)
	if (Len(@csv) > 0) SET @csv = Left(@csv,Len(@csv)-1)

	-- Return the result of the function
	RETURN @csv

END

```

## See the longest running queries on MSSQL ##
```
DBCC FREEPROCCACHE

Run following query to find longest running query using T-SQL.

SELECT DISTINCT TOP 10
t.TEXT QueryName,
s.execution_count AS ExecutionCount,
s.max_elapsed_time AS MaxElapsedTime,
ISNULL(s.total_elapsed_time / s.execution_count, 0) AS AvgElapsedTime,
s.creation_time AS LogCreatedOn,
ISNULL(s.execution_count / DATEDIFF(s, s.creation_time, GETDATE()), 0) AS FrequencyPerSec
FROM sys.dm_exec_query_stats s
CROSS APPLY sys.dm_exec_sql_text( s.sql_handle ) t
ORDER BY
s.max_elapsed_time DESC
GO
```

Source: http://blog.sqlauthority.com/2009/01/02/sql-server-2008-2005-find-longest-running-query-tsql/

## Performance counters to set ##

http://www.databasejournal.com/features/mssql/article.php/3932406/Top-10-SQL-Server-Counters-for-Monitoring-SQL-Server-Performance.htm

## See files in backup ##

```
RESTORE FILELISTONLY
FROM DISK = N'F:\BKP\Vodafone_MSCRM_fullbackup.bak'
```

## REstore into different DB with a few files ##
```
RESTORE DATABASE MSCRM_Standa 
   FROM DISK = 'F:\BKP\Vodafone_MSCRM_fullbackup.bak'
   WITH MOVE 'Vodafone_MSCRM' TO 'E:\Data\MSCRM_Standa.mdf',
   MOVE 'Vodafone_MSCRM_1' TO 'E:\Data\MSCRM_Standa_1.ndf',
   MOVE 'Vodafone_MSCRM_log' TO 'E:\Data\MSCRM_Standa_2.ldf',
   MOVE 'Vodafone_MSCRM_log_1' TO 'F:\LOG\MSCRM_Standa_3.ldf',
   MOVE 'Vodafone_MSCRM_log_2' TO 'F:\LOG\MSCRM_Standa_4.ldf',
   MOVE 'sysft_ftcat_documentindex' TO 'E:\DATA\MSCRM_Standa_5.ftcat_documentindex'
--WITH REPLACE
GO

RESTORE FILELISTONLY
FROM DISK = N'F:\BKP\Vodafone_MSCRM_fullbackup.bak'
```

## Using ROW\_NUMBER ##

```
update StringMap SET DisplayOrder = om.NUM
	from [dbo].[StringMap] sms join (select 
		ObjectTypeCode,
		ROW_NUMBER() OVER (PARTITION BY ObjectTypeCode ORDER BY ObjectTypeCode, Value ASC) AS 'NUM',  
		Value,
		StringMapId
		from [dbo].[StringMap]
		where AttributeName = 'v2_subcategory') as om on sms.StringMapId = om.StringMapId
```

## Using meta tables ##

See what MS CRM entities are using certain fields:

```
select t.name, t.type_desc, c.name, tp.name, c.collation_name, c.max_length from sys.columns c 
	join sys.tables t on c.object_id = t.object_id 
	join sys.types tp on c.user_type_id = tp.user_type_id
	where c.[name] = 'v2_activitycategoryid' OR c.[name] = 'v2_activitysubcategoryid'  
```

# Oracle #

## Troubleshooting locks ##

```
select     owner||'.'||object_name obj
   ,oracle_username||' ('||s.status||')' oruser
   ,os_user_name osuser
   ,machine computer
   ,l.process unix
   ,'||s.sid||','||s.serial#||' ss
   ,r.name rs
   ,to_char(s.logon_time,'yyyy/mm/dd hh24:mi:ss') time
from       v$locked_object l
   ,dba_objects o
   ,v$session s
   ,v$transaction t
   ,v$rollname r
where l.object_id = o.object_id
  and s.sid=l.session_id
  and s.taddr=t.addr
  and t.xidusn=r.usn
order by osuser, ss, obj
```

## PL-SQL like for ascii character ##

```
select * from cm.element e where e.name like '%' || chr(9) || '%'
```