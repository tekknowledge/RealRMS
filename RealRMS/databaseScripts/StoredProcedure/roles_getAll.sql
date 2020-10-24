IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'roles_getAll' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.roles_getAll AS RAISERROR(''Procedure roles_getAll is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.roles_getAll
/*
exec roles_getAll
*/
AS

SELECT id as idRole, name
FROM roles

GO

GRANT EXECUTE ON roles_getAll TO sqlUser

GO
