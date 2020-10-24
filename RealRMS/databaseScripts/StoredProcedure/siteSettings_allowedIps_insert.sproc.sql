IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'siteSettings_allowedIps_insert' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.siteSettings_allowedIps_insert AS RAISERROR(''Procedure siteSettings_allowedIps_insert is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.siteSettings_allowedIps_insert
	@idSiteSettings			INT,
	@ipAddress				VARCHAR(100)
AS

INSERT INTO siteSettings_allowedIps
SELECT @idSiteSettings, @ipAddress

GO