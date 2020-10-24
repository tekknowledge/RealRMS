IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'error_insert' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.error_insert AS RAISERROR(''Procedure error_insert is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.error_insert
	@UserId			INT
AS

-- SQL HERE

GO

GRANT EXECUTE ON error_insert TO sqlUser