IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'siteSettings_get' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.siteSettings_get AS RAISERROR(''Procedure siteSettings_get is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.siteSettings_get
	@id			INT
AS

SELECT id, siteUrl, databaseName
FROM siteSettings
WHERE id = @id;

GO

GRANT EXECUTE ON dbo.siteSettings_get TO public