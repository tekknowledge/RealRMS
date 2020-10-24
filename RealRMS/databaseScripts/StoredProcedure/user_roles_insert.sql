IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'user_roles_insert' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.user_roles_insert AS RAISERROR(''Procedure user_roles_insert is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.user_roles_insert
	@idUser			INT,
	@roleList		dbo.IntegerList READONLY
AS

INSERT INTO user_roles (idUser, idRole)
SELECT @idUser, value
FROM @roleList

GO

GRANT EXECUTE ON user_roles_insert TO sqlUser
