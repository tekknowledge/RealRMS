IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'menuCategory_getAll' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.menuCategory_getAll AS RAISERROR(''Procedure menuCategory_getAll is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE menuCategory_getAll
AS

SELECT id, name, description
FROM menuCategory
ORDER BY name

GO

GRANT EXECUTE ON menuCategory_getAll TO sqlUser