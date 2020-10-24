IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'user_roles_update' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.user_roles_update AS RAISERROR(''Procedure user_roles_update is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.user_roles_update
	@idUser			INT,
	@roleList		dbo.IntegerList READONLY

AS

DECLARE 
@rolescount INT

SELECT @rolesCount = COUNT(1) FROM @roleList;

DECLARE 
@rolesNotAssigned dbo.IntegerList

INSERT INTO @rolesNotAssigned
SELECT id 
FROM roles
WHERE id NOT IN (SELECT value FROM @roleList)

IF @rolesCount = 0
BEGIN
	DELETE FROM user_roles WHERE idUser = @idUser
END ELSE 
	INSERT INTO user_roles (idUser, idRole)
	SELECT @idUser, value FROM @roleList WHERE value NOT IN ( SELECT idRole FROM user_roles WHERE idUser = @idUser )
	
	DELETE FROM user_roles
	WHERE idUser = @idUser
	AND idRole IN ( SELECT value FROM @rolesNotAssigned )

GO

GRANT EXECUTE ON user_roles_update TO sqlUser