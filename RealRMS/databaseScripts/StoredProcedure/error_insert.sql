IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'error_insert' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.error_insert AS RAISERROR(''Procedure error_insert is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.error_insert
	@msg				VARCHAR(1000) = null,
	@stackTrace			VARCHAR(8000) = null,
	@url				VARCHAR(1000),
	@urlReferrer		VARCHAR(1000) = null,
	@requestJson		VARCHAR(MAX) = null,
	@EmployeeId			INT = null
AS

INSERT INTO errors (msg, stacktrace, url, urlReferer, request, EmployeeId, TimeOccurred)
SELECT @msg, @stackTrace, @url, @urlReferrer, @requestJson, @EmployeeId, CURRENT_TIMESTAMP

GO

GRANT EXECUTE ON error_insert TO sqlUser

--select top 1 * from errors