IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'roles_getForUser' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.roles_getForUser AS RAISERROR(''Procedure roles_getForUser is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.roles_getForUser
	@IdUser			INT
AS

SELECT idRole, idUser, roles.name
FROM user_roles
INNER JOIN roles ON user_roles.idRole = roles.id
WHERE idUser = @idUser

GO

GRANT EXECUTE ON roles_getForUser TO sqlUser